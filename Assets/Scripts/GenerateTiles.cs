using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateTiles : MonoBehaviour
{
    // Get a reference to all the different hex tile game objects
    public GameObject desert;
    public GameObject forest;
    public GameObject fields;
    public GameObject pasture;
    public GameObject hills;
    public GameObject mountains;
    public GameObject sea;
    public GameObject harbour;
    public GameObject Outline;

    public int gridSize = 3;

    private int m_HexRadius = 7;

    /*
    // Old implementation of the bucket system
    // An enum type called terrain that specifies the number of each terrain tiles to spawn
    enum terrain
    {
        m_NumberOfDesert = 1,
        m_NumberOfHills = 3,
        m_NumberOfMountains = 3,
        m_NumberOfForest = 4,
        m_NumberOfFields = 4,
        m_NumberOfPasture = 4,
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        SpawnTiles();
    }

    /*
    // Old implementation of the bucket system
    // Generate an arraylist called terrainBucket that stores the correct number of each terrain tile
    private ArrayList getTerrainBucket()
    {
        // An array of game objects containing each terrain tile
        GameObject[] terrainTiles = {desert, hills, mountains, forest, fields, pasture};

        ArrayList terrainBucket = new ArrayList();

        // The .GetValues method takes an enum and returns an array containing all its values
        terrain[] terrainList = (terrain[])Enum.GetValues(typeof(terrain));

        // A nested loop that adds the terrain tiles to the terrainBucket arraylist
        for (int i = 0; i < 6; i++)
        {
            for(int j = 0; j < (int)terrainList[i]; j++)
            {
                terrainBucket.Add(terrainTiles[i]);
            }
        }
        return terrainBucket;
    }
    */

    // Generate an arraylist called terrainBucket that stores the correct number of each terrain tile
    private ArrayList getTerrainBucket()
    {
        GameObject[] terrainTiles = {forest, fields, pasture, hills, mountains};

        // Calculate the number of terrain tiles needed minus the desert tile
        double numberOfTerrainTiles = 3*Math.Pow(gridSize, 2) - 3 * gridSize;

        // Initialize the terrainBucket array list and add the desert tile first
        ArrayList terrainBucket = new ArrayList();
        terrainBucket.Add(desert);

        // A for loop to add terrain tiles into the bucket
        for (int i = 0; i < numberOfTerrainTiles; i++)
        {
            terrainBucket.Add(terrainTiles[i % 5]);
        }
        return terrainBucket;

    }

    private void SpawnTiles()
    {
        // Get and store the arraylists generated from these two method calls
        ArrayList terrainBucket = getTerrainBucket();
        GameObject[] waterTiles = {sea, harbour};

        // These variables are used as the index position to access waterTiles to get the sea and harbour tiles in the correct place
        int waterTilesIndexBottomLeft = 1;
        int waterTilesIndexTopRight = 0;

        // i and reverse_i determine the number of tiles that will be generated in a row
        // They are also responsible for the offset between odd and even rows
        float i = gridSize * 0.5f;
        float reverse_i = i + gridSize;

        float rows = i + gridSize;
        int zOffset = gridSize;

        // for loop that runs equal to the height of the grid size
        for (; i <= rows; i += 0.5f)
        {
            float tilesInRow = Mathf.Min(i, reverse_i);

            // for loop that runs equal to the number of tiles to generate per row
            for (float xOffset = -tilesInRow; xOffset <= tilesInRow; xOffset++)
            {
                // Check if the tiles are being generated at the bottom or leftmost side. If it is, get the index position from waterTilesIndexBottomLeft
                if (zOffset == -gridSize || xOffset == -tilesInRow)
                {
                    Instantiate(waterTiles[waterTilesIndexBottomLeft % 2], new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));
                    waterTilesIndexBottomLeft++;
                }

                // Check if the tiles are being generated at the top or rightmost side. If it is, get the index position from waterTilesIndexTopRight
                else if (zOffset == gridSize || xOffset == tilesInRow)
                {
                    // The position of the tile is calculated using the formula: x position = Sqrt(3) * radius * x offset, z position = 1.5 * radius * z offset
                    Instantiate(waterTiles[waterTilesIndexTopRight % 2], new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));
                    waterTilesIndexTopRight++;
                }

                else
                {
                    // A random number is used to access the terrainBucket arraylist.
                    // The object is instantiated and then removed from the arraylist
                    int rand = UnityEngine.Random.Range(0, terrainBucket.Count);
                    Instantiate((GameObject)terrainBucket[rand], new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));
                    terrainBucket.RemoveAt(rand);

                    // A terrain tile is generated with an outline around it
                    Instantiate(Outline, new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));
                }
            }
            reverse_i -= 0.5f;
            zOffset --;
        }
    }
}