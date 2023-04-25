using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
    // Get a reference to the settlement game object
    public GameObject settlement;

    // An arraylist containing all of the settlements that will be generated in the scene
    public ArrayList settlementsList = new ArrayList();

    public ArrayList builtSettlementsList = new ArrayList();

    private int m_hexRadius;

    // An arraylist to contain all of the terrain tiles that have been instantiated into the scene
    private ArrayList m_TerrainTilesList = new ArrayList();

    // Spawn a settlement on all the vertices of every terrain tile
    public void SpawnSettlements()
    {
        // Get and store the hexagon radius by passing the value from the GenerateTiles script
        m_hexRadius = FindObjectOfType<GenerateTiles>().hexRadius;

        // Get all of the terrain tiles in the scene
        m_TerrainTilesList = FindObjectOfType<GenerateNumberTiles>().terrainTilesList;

        Vector3 selectedTerrainTilePosition;
        GameObject selectedSettlement;

        // An array of Vector3 variables to store the settlement positions
        Vector3[] selectedSettlementPositions = new Vector3[6];

        // An array of bool variables to check whether the settlements have spawned in the same position
        bool[] samePositions = new bool[6];

        // A variable to keep track of the number of settlements that have been instantiated
        int settlementCounter = 0;

        // Loop through every terrain tile
        foreach (GameObject terrainTile in m_TerrainTilesList)
        {
            // Get the position of the selected terrain tile
            selectedTerrainTilePosition = terrainTile.transform.position;

            // Get the position of the settlements to be instantiated
            // Top vertex
            selectedSettlementPositions[0] = new Vector3(selectedTerrainTilePosition.x, 2.0f, selectedTerrainTilePosition.z + m_hexRadius);

            // Bottom vertex
            selectedSettlementPositions[1] = new Vector3(selectedTerrainTilePosition.x, 2.0f, selectedTerrainTilePosition.z - m_hexRadius);

            // Top left vertex
            selectedSettlementPositions[2] = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 2, 2.0f, selectedTerrainTilePosition.z + (m_hexRadius / 2f));

            // Top right vertex
            selectedSettlementPositions[3] = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 2, 2.0f, selectedTerrainTilePosition.z + (m_hexRadius / 2f));

            // Bottom left vertex
            selectedSettlementPositions[4] = new Vector3(selectedTerrainTilePosition.x - (Mathf.Sqrt(3) * m_hexRadius) / 2, 2.0f, selectedTerrainTilePosition.z - (m_hexRadius / 2f));

            // Bottom right vertex
            selectedSettlementPositions[5] = new Vector3(selectedTerrainTilePosition.x + (Mathf.Sqrt(3) * m_hexRadius) / 2, 2.0f, selectedTerrainTilePosition.z - m_hexRadius / 2f);

            // A loop that iterates through settlementsList to check if there is already an instantiated settlement in the same position
            foreach (GameObject settlement in settlementsList)
            {
                Vector3 settlementPosition = settlement.transform.position;
                for (int i = 0; i < 6; i++)
                {
                    if (Math.Abs(Vector3.Distance(selectedSettlementPositions[i], settlementPosition)) < 0.1)
                    {
                        samePositions[i] = true;
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                // If a settlement doesn't currently exists there, spawn a settlement
                if (samePositions[i])
                {
                    samePositions[i] = false;
                }

                else
                {
                    selectedSettlement = Instantiate((GameObject)settlement, selectedSettlementPositions[i], Quaternion.identity);

                    // Change the name of the settlement to include the order it generated in
                    selectedSettlement.name = selectedSettlement.name.Substring(0, selectedSettlement.name.Length - 7) + settlementCounter;
                    settlementCounter++;

                    // Make the settlement invisible
                    Transform settlementMesh = selectedSettlement.transform.GetChild(0);
                    settlementMesh.gameObject.SetActive(false);

                    settlementsList.Add(selectedSettlement);
                    samePositions[i] = false;
                }
            }
        }
    }

    // Enable the settlements to be interactive
    public void EnableSettlements()
    {
        foreach (GameObject settlement in settlementsList)
        {
            settlement.GetComponent<Settlement>().enabled = true;
        }
    }

    // Disable interactivity
    public void DisableSettlements()
    {
        foreach (GameObject settlement in settlementsList)
        {
            settlement.GetComponent<Settlement>().enabled = false;
        }
    }

    // Enable the built settlements to be upgradable
    public void EnableUpgrades()
    {
        foreach (GameObject settlement in builtSettlementsList)
        {
            settlement.GetComponent<UpgradeSettlement>().enabled = true;
        }
    }

    // Disable upgradability
    public void DisableUpgrades()
    {
        foreach (GameObject settlement in builtSettlementsList)
        {
            settlement.GetComponent<UpgradeSettlement>().enabled = false;
        }
    }


}