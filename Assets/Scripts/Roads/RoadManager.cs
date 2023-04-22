using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    // Get a reference to the road game object
    public GameObject road;

    // An arraylist containing all of the roads that will be generated in the scene
    public ArrayList roadsList = new ArrayList();

    private int m_hexRadius;

    // An arraylist to contain all of the terrain tiles that have been instantiated into the scene
    private ArrayList m_TerrainTilesList = new ArrayList();

    // Spawn a road on all the edges of every terrain tile
    public void SpawnRoads()
    {
        // Get and store the hexagon radius by passing the value from the GenerateTiles script
        m_hexRadius = FindObjectOfType<GenerateTiles>().hexRadius;

        // Get all of the terrain tiles in the scene
        m_TerrainTilesList = FindObjectOfType<GenerateNumberTiles>().terrainTilesList;

        Vector3 selectedTerrainTilePosition;
        GameObject selectedRoad;

        // An array of Vector3 variables to store the road positions
        Vector3[] selectedRoadPositions = new Vector3[6];

        // An array of quaternion variables to store the road rotations
        Quaternion[] selectedRoadRotation = new Quaternion[6];

        // Left edge
        selectedRoadRotation[0] = Quaternion.identity;

        // Right edge
        selectedRoadRotation[1] = Quaternion.identity;

        // Top left edge
        selectedRoadRotation[2] = Quaternion.Euler(0, 60, 0);

        // Top right edge
        selectedRoadRotation[3] = Quaternion.Euler(0, -60, 0);

        // Bottom left edge
        selectedRoadRotation[4] = Quaternion.Euler(0, -60, 0);

        // Bottom right edge
        selectedRoadRotation[5] = Quaternion.Euler(0, 60, 0);


        // An array of bool variables to check whether the roads have spawned in the same position
        bool[] samePositions = new bool[6];

        // A variable to keep track of the number of roads that have been instantiated
        int roadCounter = 0;

        // Loop through every terrain tile
        foreach (GameObject terrainTile in m_TerrainTilesList)
        {
            // Get the position of the selected terrain tile
            selectedTerrainTilePosition = terrainTile.transform.position;

            // Get the position of the roads to be instantiated
            // Left edge
            selectedRoadPositions[0] = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 2, 1.0f, selectedTerrainTilePosition.z);

            // Right edge
            selectedRoadPositions[1] = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 2, 1.0f, selectedTerrainTilePosition.z);

            // Top left edge
            selectedRoadPositions[2] = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z + 0.75f * m_hexRadius);

            // Top right edge
            selectedRoadPositions[3] = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z + 0.75f * m_hexRadius);

            // Bottom left edge
            selectedRoadPositions[4] = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z - 0.75f * m_hexRadius);

            // Bottom right edge
            selectedRoadPositions[5] = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z - 0.75f * m_hexRadius);

            // A loop that iterates through roadsList to check if there is already an instantiated road in the same position
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                for (int i = 0; i < 6; i++)
                {
                    if (Math.Abs(Vector3.Distance(selectedRoadPositions[i], roadPosition)) < 0.1)
                    {
                        samePositions[i] = true;
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                // If a road doesn't currently exists there, spawn a road
                if (samePositions[i])
                {
                    samePositions[i] = false;
                }

                else
                {
                    selectedRoad = Instantiate((GameObject)road, selectedRoadPositions[i], selectedRoadRotation[i]);

                    // Change the name of the road to include the order it generated in
                    selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                    roadCounter++;

                    // Make the road invisible
                    selectedRoad.GetComponentInChildren<Renderer>().enabled = false;

                    roadsList.Add(selectedRoad);
                    samePositions[i] = false;
                }
            }
        }
    }


    // Enable the roads to be interactive
    public void EnableRoads()
    {
        // roadsList = FindObjectOfType<RoadManager>().roadsList;
        foreach (GameObject road in roadsList)
        {
            road.GetComponent<Road>().enabled = true;
        }
    }

    // Disable interactivity
    public void DisableRoads()
    {
        // roadsList = FindObjectOfType<RoadManager>().roadsList;
        foreach (GameObject road in roadsList)
        {
            road.GetComponent<Road>().enabled = false;
        }
    }
}