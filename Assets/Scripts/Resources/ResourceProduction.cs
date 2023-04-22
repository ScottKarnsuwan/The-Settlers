using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceProduction : MonoBehaviour
{
    // Get a reference to the 2 dice
    public GameObject dice1;
    public GameObject dice2;

    // Get a reference to the BuildAndTradePhaseScreen and DiceScreen
    public GameObject BuildAndTradePhaseScreen;
    public GameObject diceScreen;

    // Create variables to keep track of the resource count
    // They are public which means that they can be referenced by other scripts
    // However, [HideInInspector] makes it so that you cannot change their values from within the inspector
    private int brickCount;
    private int lumberCount;
    private int oreCount;
    private int grainCount;
    private int woolCount;

    // Create 2 variables of type Roll, where Roll is the name of the script attatched to the 2 dice objects
    private Roll m_Dice1;
    private Roll m_Dice2;

    private int m_RollResult;

    // This arraylist will store all of the number tiles that match the dice roll result
    private ArrayList m_SelectedNumberTileList = new ArrayList();

    // A variable to store the duration of how long the number tile will stay green for before going back to its original color
    private WaitForSeconds m_ColorWait;

    // An arraylist to contain all of the terrain tiles that have been instantiated into the scene
    private ArrayList m_TerrainTilesList = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        // Get all of the terrain tiles in the scene
        m_TerrainTilesList = FindObjectOfType<GenerateNumberTiles>().terrainTilesList;

        // Set the color duration to 2 seconds
        m_ColorWait = new WaitForSeconds(2);
    }

    // This script is enabled when the Roll button is clicked
    void OnEnable()
    {
        brickCount = FindObjectOfType<ResourceCounter>().brickCount;
        lumberCount = FindObjectOfType<ResourceCounter>().lumberCount;
        oreCount = FindObjectOfType<ResourceCounter>().oreCount;
        grainCount = FindObjectOfType<ResourceCounter>().grainCount;
        woolCount = FindObjectOfType<ResourceCounter>().woolCount;

        // Assign a reference to the Roll script attatched to the dice1 and dice2 objects
        m_Dice1 = dice1.GetComponent<Roll>();
        m_Dice2 = dice2.GetComponent<Roll>();

        // Start the GetRollResult coroutine
        StartCoroutine(GetRollResult());
    }

    // A corutine is similar to a regular function
    // However, the only difference is that it will stop its execution when it encounters 'yield'
    // When this happens, execution exits the function and waits until whatever is to the right of 'yield' to be completed before executing the rest of the function
    // Essentially, this puts a pause in the middle of the function
    private IEnumerator GetRollResult()
    {
        // This while loop will never exit until both dice have rolled a number
        while (m_Dice1.highestFaceNumber == 0 || m_Dice2.highestFaceNumber == 0)
        {
            // This tells the coroutine to resume execution in the next frame
            yield return null;
        }

        // Once the dice have finished rolling, add them together
        m_RollResult = m_Dice1.highestFaceNumber + m_Dice2.highestFaceNumber;

        // Switch back to the main screen
        diceScreen.SetActive(false);
        BuildAndTradePhaseScreen.SetActive(true);

        // Start the GiveResources coroutine
        yield return StartCoroutine(GiveResources());
    }

    private IEnumerator GiveResources()
    {
        // Loop through every terrain tile and find which ones have a number tile that matches the roll result
        foreach (GameObject terrainTile in m_TerrainTilesList)
        {
            if (terrainTile.transform.Find("Number Tile "+ m_RollResult))
            {
                // Incremeent the appropriate resource count
                if (terrainTile.name == "Hills")
                {
                    brickCount += 1;
                }
                else if (terrainTile.name == "Forest")
                {
                    lumberCount += 1;
                }
                else if (terrainTile.name == "Mountains")
                {
                    oreCount += 1;
                }
                else if (terrainTile.name == "Fields")
                {
                    grainCount += 1;
                }
                else if (terrainTile.name == "Pasture")
                {
                    woolCount += 1;
                }

                // Store the matching number tile into an arraylist
                Transform numberTile = terrainTile.transform.Find("Number Tile " + m_RollResult);
                m_SelectedNumberTileList.Add(numberTile.GetChild(0));
            }
        }

        // Call the UpdateResourceCount from the ResourceCounter script
        FindObjectOfType<ResourceCounter>().UpdateResourceCount(brickCount, lumberCount, oreCount, grainCount, woolCount);

        // Disable this script
        enabled = false;

        // Loop through the matching number tiles and turn them green
        foreach (Transform selectedNumberTile in m_SelectedNumberTileList)
        {
            selectedNumberTile.GetComponent<Renderer>().material.color = Color.green;
        }

        // Wait 2 seconds
        yield return m_ColorWait;

        // Turn the number tiles back to their original color
        foreach (Transform selectedNumberTile in m_SelectedNumberTileList)
        {
            selectedNumberTile.GetComponent<Renderer>().material.color = Color.white;
        }

        m_SelectedNumberTileList.Clear();
    }
}