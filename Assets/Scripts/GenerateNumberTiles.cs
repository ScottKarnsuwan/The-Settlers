using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class GenerateNumberTiles : MonoBehaviour
{
    // Get a reference to all the different number tile game objects
    public GameObject numberTile2;
    public GameObject numberTile3;
    public GameObject numberTile4;
    public GameObject numberTile5;
    public GameObject numberTile6;
    public GameObject numberTile8;
    public GameObject numberTile9;
    public GameObject numberTile10;
    public GameObject numberTile11;
    public GameObject numberTile12;

    private int m_hexRadius;
    private GameObject m_SelectedTerrainTile;


    // An arraylist containing all of the terrain tiles that have been instantiated into the scene
    public ArrayList terrainTilesList = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        // Get and store the hexagon radius by passing the value from the GenerateTiles script
        m_hexRadius = FindObjectOfType<GenerateTiles>().hexRadius;

        // Call the SpawnTiles() method from the GenerateTiles script and store the return value in terrainTilesList
        // This also instantiates the tiles into the scene
        terrainTilesList = FindObjectOfType<GenerateTiles>().SpawnTiles();

        SpawnNumberTiles(terrainTilesList);
    }

    // Generate an arraylist called numberBuckeet that stores the correct number of each number tile
    private ArrayList GetNumberTilesBucket()
    {
        GameObject[] numberTiles = { numberTile6, numberTile5, numberTile9, numberTile4, numberTile10, numberTile3, numberTile8, numberTile11, numberTile2, numberTile12 };

        // Initialize the numberBucket array list
        ArrayList numberBucket = new ArrayList();

        // A for loop to add number tiles into the bucket for every terrain tile in the scene minus the desert tile
        for (int i = 0; i < terrainTilesList.Count - 1; i++)
        {
            numberBucket.Add(numberTiles[i % 10]);
        }
        return numberBucket;
    }

    // Generate an arraylist called terrainTilesIndex that just populates the arraylist with (0,1,2,3...) whilst omitting the index of the desert tile
    public ArrayList GetTerrainTilesIndex()
    {
        ArrayList terrainTilesIndex = new ArrayList();

        for (int i = 0; i < terrainTilesList.Count; i++)
        {
            m_SelectedTerrainTile = (GameObject)terrainTilesList[i];
            if (m_SelectedTerrainTile.name != "Desert(Clone)")
            {
                terrainTilesIndex.Add(i);
            }
        }

        return terrainTilesIndex;
    }

    private void SpawnNumberTiles(ArrayList terrainTilesList)
    {
        // Get and store the arraylist generated from the getNumberTilesBucket() method
        ArrayList numberBucket = GetNumberTilesBucket();

        // An arraylist to store the indexes of each terrain tile
        ArrayList terrainTilesIndex = GetTerrainTilesIndex();

        // An arraylist to store number tiles that have been instantiated
        // This will only used to keep track of numberTile6 and numberTile8 (red number tiles) to make sure they don't spawn on adjacent hexes
        ArrayList numberTilesList = new ArrayList();

        // A random terrain tile will be selected using rand
        // The selected tile will be stored in selectedTerrainTile
        // Its position will be stored in selectedTerrainTilePosition
        int rand;
        Vector3 selectedTerrainTilePosition;

        // A variable to check whether numberTile6 or numberTile8 have spawned next to eachother
        bool isAdjacent = false;

        GameObject selectedNumberTile;

        // Initialize all instances of numberTile6 and numberTile8 from the numberBucket first
        while (numberBucket.Contains(numberTile6) || numberBucket.Contains(numberTile8))
        {
            // This uses the ternary operator: '?' to determine which of the 2 number tiles is present in numberBucket
            // whichNumberTile is then assigned to it
            object whichNumberTile = numberBucket.Contains(numberTile6) ? numberTile6 : numberTile8;

            // Select a random terrain tile and store it and its position
            rand = UnityEngine.Random.Range(0, terrainTilesIndex.Count);
            m_SelectedTerrainTile = (GameObject)terrainTilesList[(int)terrainTilesIndex[rand]];
            selectedTerrainTilePosition = m_SelectedTerrainTile.transform.position;

            // A loop that iterates through each game object in the numberTilesList to check if any of them are within Sqrt(3) * radius of the new number tile to be instantiated
            foreach (GameObject numberTile in numberTilesList)
            {
                Vector3 numberTilePosition = new Vector3(numberTile.transform.position.x, selectedTerrainTilePosition.y, numberTile.transform.position.z);
                if (Math.Abs(Vector3.Distance(selectedTerrainTilePosition, numberTilePosition)) <= Mathf.Sqrt(3) * m_hexRadius)
                {
                    isAdjacent = true;
                }
            }

            // If the new number tile is adjacent to an existing number tile, move on to the next iteration of the while loop and skip instantiation
            if (isAdjacent)
            {
                isAdjacent = false;
                continue;
            }

            // Instantiate the number tile above the selected terrain tile and make the number tile a child of it
            numberTilesList.Add(Instantiate((GameObject)numberBucket[numberBucket.IndexOf(whichNumberTile)], new Vector3(selectedTerrainTilePosition.x, 6.0f, selectedTerrainTilePosition.z), Quaternion.Euler(0f, 0f, 0f)));
            selectedNumberTile = (GameObject)numberTilesList[numberTilesList.Count - 1];
            selectedNumberTile.transform.parent = m_SelectedTerrainTile.transform;
            
            // Remove the instantiated number tile from the bucket 
            numberBucket.Remove(whichNumberTile);

            // Remove the index of the selected terrain tile from the terrainTilesIndex arraylist so that it cannot be selected again
            terrainTilesIndex.RemoveAt(rand);
        }

        // A for loop to instantiate the rest of the number tiles randomly
        for (int i = 0; i < numberBucket.Count; i++)
        {
            rand = UnityEngine.Random.Range(0, terrainTilesIndex.Count);
            m_SelectedTerrainTile = (GameObject)terrainTilesList[(int)terrainTilesIndex[rand]];
            selectedTerrainTilePosition = m_SelectedTerrainTile.transform.position;
            selectedNumberTile = Instantiate((GameObject)numberBucket[i], new Vector3(selectedTerrainTilePosition.x, 6.0f, selectedTerrainTilePosition.z), Quaternion.Euler(0f, 0f, 0f));
            selectedNumberTile.transform.parent = m_SelectedTerrainTile.transform;
            terrainTilesIndex.RemoveAt(rand);
        }
    }
}