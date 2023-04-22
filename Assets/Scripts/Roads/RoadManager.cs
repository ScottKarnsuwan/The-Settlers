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
        Vector3 selectedRoadPosition;

        // A variable to keep track of the number of roads that have been instantiated
        int roadCounter = 0;

        // A variable to check whether the roads have spawned in the same position
        bool samePosition = false;

        // Loop through every terrain tile
        foreach (GameObject terrainTile in m_TerrainTilesList)
        {
            // Get the position of the selected terrain tile
            selectedTerrainTilePosition = terrainTile.transform.position;

            // Get the position of the road to be instantiated
            // The road will be instantiated on the left edge of the selected terrain tile
            selectedRoadPosition = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 2, 1.0f, selectedTerrainTilePosition.z);

            // A loop that iterates through roadsList to check if there is already an instantiated road in the same position
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                if (Math.Abs(Vector3.Distance(selectedRoadPosition, roadPosition)) < 0.1)
                {
                    samePosition = true;
                }
            }

            if (samePosition)
            {
                samePosition = false;
            }

            // If a road doesn't currently exists there, spawn a road
            else
            {
                selectedRoad = Instantiate((GameObject)road, selectedRoadPosition, Quaternion.identity);

                // Change the name of the road to include the order it generated in
                selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                roadCounter++;

                // Make the road invisible
                selectedRoad.GetComponentInChildren<Renderer>().enabled = false;

                roadsList.Add(selectedRoad);
                samePosition = false;
            }

            // Do the same thing again but for the right edge of the selected terrain tile
            selectedRoadPosition = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 2, 1.0f, selectedTerrainTilePosition.z);
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                if (Math.Abs(Vector3.Distance(selectedRoadPosition, roadPosition)) < 0.1)
                {
                    samePosition = true;
                }
            }

            if (samePosition)
            {
                samePosition = false;
            }

            else
            {
                selectedRoad = Instantiate((GameObject)road, selectedRoadPosition, Quaternion.identity);
                selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                roadCounter++;
                selectedRoad.GetComponentInChildren<Renderer>().enabled = false;
                roadsList.Add(selectedRoad);
                samePosition = false;
            }

            // Check the top left edge
            selectedRoadPosition = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z + 0.75f * m_hexRadius);
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                if (Math.Abs(Vector3.Distance(selectedRoadPosition, roadPosition)) < 0.1)
                {
                    samePosition = true;
                }
            }

            if (samePosition)
            {
                samePosition = false;
            }

            else
            {
                selectedRoad = Instantiate((GameObject)road, selectedRoadPosition, Quaternion.Euler(0, 60, 0));
                selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                roadCounter++;
                selectedRoad.GetComponentInChildren<Renderer>().enabled = false;
                roadsList.Add(selectedRoad);
                samePosition = false;
            }

            // Check the top right edge
            selectedRoadPosition = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z + 0.75f * m_hexRadius);
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                if (Math.Abs(Vector3.Distance(selectedRoadPosition, roadPosition)) < 0.1)
                {
                    samePosition = true;
                }
            }

            if (samePosition)
            {
                samePosition = false;
            }

            else
            {
                selectedRoad = Instantiate((GameObject)road, selectedRoadPosition, Quaternion.Euler(0, -60, 0));
                selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                roadCounter++;
                selectedRoad.GetComponentInChildren<Renderer>().enabled = false;
                roadsList.Add(selectedRoad);
                samePosition = false;
            }

            // Check the bottom left edge
            selectedRoadPosition = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z - 0.75f * m_hexRadius);
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                if (Math.Abs(Vector3.Distance(selectedRoadPosition, roadPosition)) < 0.1)
                {
                    samePosition = true;
                }
            }

            if (samePosition)
            {
                samePosition = false;
            }

            else
            {
                selectedRoad = Instantiate((GameObject)road, selectedRoadPosition, Quaternion.Euler(0, -60, 0));
                selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                roadCounter++;
                selectedRoad.GetComponentInChildren<Renderer>().enabled = false;
                roadsList.Add(selectedRoad);
                samePosition = false;
            }

            // Check the bottom right edge
            selectedRoadPosition = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 4, 1.0f, selectedTerrainTilePosition.z - 0.75f * m_hexRadius);
            foreach (GameObject road in roadsList)
            {
                Vector3 roadPosition = road.transform.position;
                if (Math.Abs(Vector3.Distance(selectedRoadPosition, roadPosition)) < 0.1)
                {
                    samePosition = true;
                }
            }

            if (samePosition)
            {
                samePosition = false;
            }

            else
            {
                selectedRoad = Instantiate((GameObject)road, selectedRoadPosition, Quaternion.Euler(0, 60, 0));
                selectedRoad.name = selectedRoad.name.Substring(0, selectedRoad.name.Length - 7) + roadCounter;
                roadCounter++;
                selectedRoad.GetComponentInChildren<Renderer>().enabled = false;
                roadsList.Add(selectedRoad);
                samePosition = false;
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