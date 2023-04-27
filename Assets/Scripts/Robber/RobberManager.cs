using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberManager : MonoBehaviour
{
    public GameObject robberTile;

    // An arraylist to contain all of the terrain tiles that have been instantiated into the scene
    private ArrayList m_TerrainTilesList = new ArrayList();

    // Enable the robber to be moved onto other terrain tiles
    public void EnableRobber()
    {
        // Get all of the terrain tiles in the scene
        m_TerrainTilesList = FindObjectOfType<GenerateNumberTiles>().terrainTilesList;

        // Find the robber tile
        foreach (GameObject tile in m_TerrainTilesList)
        {
            if (tile.transform.Find("Robber"))
            {
                robberTile = tile;
            }
        }

        // Enable only the terrain tiles without the robber
        foreach (GameObject tile in m_TerrainTilesList)
        {
            if (!tile.transform.Find("Robber"))
            {
                tile.GetComponent<MoveRobber>().enabled = true;
            }
        }
    }

    // Disable robber movement
    public void DisableRobber()
    {
        foreach (GameObject tile in m_TerrainTilesList)
        {
            tile.GetComponent<MoveRobber>().enabled = false;
        }
    }
}
