using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class OldGenerateTiles : MonoBehaviour
{
    // Get a reference to all the different hex tile game objects
    public GameObject desert;
    public GameObject hills;
    public GameObject mountains;
    public GameObject forest;
    public GameObject fields;
    public GameObject pasture;
    public GameObject sea;
    public GameObject harbour;
    public GameObject Outline;

    public int gridSize = 3;

    private int m_HexRadius = 7;

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

    // An enum type called water that specifies the number of each terrain tiles to spawn
    enum water
    {
        m_NumberOfSea = 9,
        m_NumberOfHarbour = 9,
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnTiles();
    }

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

    private ArrayList getWaterBucket()
    {
        GameObject[] waterTiles = {sea, harbour};

        ArrayList waterBucket = new ArrayList();

        water[] waterList = (water[])Enum.GetValues(typeof(water));

        for (int i = 0;i < 2;i++)
        {
            for (int j = 0; j < (int)waterList[i]; j++)
            {
                waterBucket.Add(waterTiles[i]);
            }
        }
        return waterBucket;
    }

    private void SpawnTiles()
    {
        // Get and store the arraylists generated from these two method calls
        ArrayList terrainBucket = getTerrainBucket();
        ArrayList waterBucket = getWaterBucket();

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
                // Check if the tiles are being generated at the edge. If it is, then pull from the waterBucket instead
                if (Math.Abs(xOffset) == tilesInRow || (Math.Abs(zOffset) == gridSize))
                {
                    // Access the arraylist at a random index and instantiate the object, then remove it from the arraylist
                    // The position of the tile is calculated using the formula: x position = Sqrt(3) * radius * x offset, z position = 1.5 * radius * z offset
                    int rand = UnityEngine.Random.Range(0, waterBucket.Count);
                    Instantiate((GameObject)waterBucket[rand], new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));
                    waterBucket.RemoveAt(rand);
                }

                else
                {
                    int rand = UnityEngine.Random.Range(0, terrainBucket.Count);
                    Instantiate((GameObject)terrainBucket[rand], new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));

                    // A terrain tile is generated with an outline around it
                    Instantiate(Outline, new Vector3(Mathf.Sqrt(3) * m_HexRadius * xOffset, 1, 1.5f * m_HexRadius * zOffset), Quaternion.Euler(0f, 30, 0f));
                    terrainBucket.RemoveAt(rand);
                }
            }
            reverse_i -= 0.5f;
            zOffset--;
        }
    }
}