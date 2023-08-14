using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberManager : MonoBehaviour
{
    public GameObject robberTile;

    // An arraylist to contain all of the terrain tiles that have been instantiated into the scene
    private ArrayList m_TerrainTilesList = new ArrayList();

    public void DiscardHalf()
    {
        int brickCount = FindObjectOfType<ResourceCounter>().brickCount;
        int lumberCount = FindObjectOfType<ResourceCounter>().lumberCount;
        int oreCount = FindObjectOfType<ResourceCounter>().oreCount;
        int grainCount = FindObjectOfType<ResourceCounter>().grainCount;
        int woolCount = FindObjectOfType<ResourceCounter>().woolCount;

        int resourceCount = brickCount + lumberCount + oreCount + grainCount + woolCount;
        int[] resourceArrayInt = { brickCount, lumberCount, oreCount, grainCount, woolCount };
        string[] resourceArraystring = { "brick", "lumber", "ore", "grain", "wool" };

        // Create a list of total resources
        List<string> resourceList = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < resourceArrayInt[i]; j++)
            {
                    resourceList.Add(resourceArraystring[i]);
            }
        }

        // Remove half from the list randomly
        for (int i = 0; i < resourceCount / 2; i++)
        {
            int rand = UnityEngine.Random.Range(0, resourceList.Count);
            resourceList.RemoveAt(rand);
        }

        // Count the number of each resources left after halving
        brickCount = resourceList.FindAll(item => item == "brick").Count;
        lumberCount = resourceList.FindAll(item => item == "lumber").Count;
        oreCount = resourceList.FindAll(item => item == "ore").Count;
        grainCount = resourceList.FindAll(item => item == "grain").Count;
        woolCount = resourceList.FindAll(item => item == "wool").Count;

        // Call the UpdateResourceCount from the ResourceCounter script
        FindObjectOfType<ResourceCounter>().UpdateResourceCount(brickCount, lumberCount, oreCount, grainCount, woolCount);
    }

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