using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// More will be added to this class as the game develops
public class ScreenManager : MonoBehaviour
{
    // Get a reference to the different screens to switch between
    public GameObject roadBuildingScreen;
    public GameObject buildAndTradePhaseScreen;

    public Button roadsButton;

    private int brickCount;
    private int lumberCount;

    // Update is called once per frame
    void Update()
    {
        // Only activate the roads button if you have at least 1 brick and 1 lumber
        brickCount = FindObjectOfType<ResourceCounter>().brickCount;
        lumberCount = FindObjectOfType<ResourceCounter>().lumberCount;

        if (brickCount < 1 || lumberCount < 1)
        {
            roadsButton.interactable = false;
        }

        else
        {
            roadsButton.interactable= true;
        }
    }

    // This is called by the Road script to switch from the road building screen to the build and trade phase screen
    public void DisableRoadBuilding()
    {
        roadBuildingScreen.SetActive(false);
        buildAndTradePhaseScreen.SetActive(true);
        FindObjectOfType<RoadManager>().DisableRoads();
    }
}