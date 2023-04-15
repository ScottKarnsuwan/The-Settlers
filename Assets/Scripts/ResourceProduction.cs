using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProduction : MonoBehaviour
{
    // An arraylist containing all of the terrain tiles that have been instantiated into the scene
    private ArrayList terrainTilesList = new ArrayList();
    private ArrayList terrainTilesIndex = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        terrainTilesList = FindObjectOfType<GenerateNumberTiles>().terrainTilesList;
        terrainTilesIndex = FindObjectOfType<GenerateNumberTiles>().GetTerrainTilesIndex();
    }

    void OnEnable()
    {

        foreach (GameObject terrainTile in terrainTilesList)
        {

        }
    }
}
