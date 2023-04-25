using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// More will be added to this class as the game develops
public class ScreenManager : MonoBehaviour
{
    // Get a reference to the different screens to switch between
    public GameObject buildAndTradePhaseScreen;
    public GameObject roadBuildingScreen;
    public GameObject settlementBuildingScreen;
    public GameObject cityBuildingScreen;

    public Button roadsButton;
    public Button settlementsButton;
    public Button citiesButton;

    private int brickCount;
    private int lumberCount;
    private int oreCount;
    private int grainCount;
    private int woolCount;

    private ArrayList builtSettlementsList = new ArrayList();

    // Update is called once per frame
    void Update()
    {
        brickCount = FindObjectOfType<ResourceCounter>().brickCount;
        lumberCount = FindObjectOfType<ResourceCounter>().lumberCount;
        oreCount = FindObjectOfType<ResourceCounter>().oreCount;
        grainCount = FindObjectOfType<ResourceCounter>().grainCount;
        woolCount = FindObjectOfType<ResourceCounter>().woolCount;

        builtSettlementsList = FindObjectOfType<SettlementManager>().builtSettlementsList;

        // Only activate the Roads button if there are at least 1 brick and 1 lumber
        if (brickCount < 1 || lumberCount < 1)
        {
            roadsButton.interactable = false;
        }
        else
        {
            roadsButton.interactable= true;
        }

        // Only activate the Settlements button if there are at least 1 brick, 1 lumber, 1 grain and 1 wool
        if (brickCount < 1 || lumberCount < 1 || grainCount < 1 || woolCount < 1)
        {
            settlementsButton.interactable = false;
        }
        else
        {
            settlementsButton.interactable = true;
        }

        // Only activate the Cities button if there are at least 3 ores, 2 grain and 1 built settlement
        if (oreCount < 3 || grainCount < 2 || builtSettlementsList.Count ==0)
        {
            citiesButton.interactable = false;
        }
        else
        {
            citiesButton.interactable = true;
        }
    }

    // This is called by the Road script to switch from the road building screen to the build and trade phase screen
    public void DisableRoadBuilding()
    {
        roadBuildingScreen.SetActive(false);
        buildAndTradePhaseScreen.SetActive(true);
        FindObjectOfType<RoadManager>().DisableRoads();
    }

    // This is called by the Settlement to switch from the settlement building screen to the build and trade phase screen
    public void DisableSettlementBuilding()
    {
        settlementBuildingScreen.SetActive(false);
        buildAndTradePhaseScreen.SetActive(true);
        FindObjectOfType<SettlementManager>().DisableSettlements();
    }

    public void DisableSettlementUpgrading()
    {
        cityBuildingScreen.SetActive(false);
        buildAndTradePhaseScreen.SetActive(true);
        FindObjectOfType<SettlementManager>().DisableUpgrades();
    }
}