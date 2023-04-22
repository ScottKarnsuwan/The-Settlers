using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    // Get a reference to the text display of each resource
    public TextMeshProUGUI brickCountDisplay;
    public TextMeshProUGUI lumberCountDisplay;
    public TextMeshProUGUI oreCountDisplay;
    public TextMeshProUGUI grainCountDisplay;
    public TextMeshProUGUI woolCountDisplay;

    public int brickCount;
    public int lumberCount;
    public int oreCount;
    public int grainCount;
    public int woolCount;

    // Start is called before the first frame update
    void Start()
    {
        brickCount = 0;
        lumberCount = 0;
        oreCount = 0;
        grainCount = 0;
        woolCount = 0;
    }

    // Update is called once per frame
    // Display the resource ammount to the screen
    void Update()
    {
        brickCountDisplay.text = brickCount.ToString();
        lumberCountDisplay.text = lumberCount.ToString();
        oreCountDisplay.text = oreCount.ToString();
        grainCountDisplay.text = grainCount.ToString();
        woolCountDisplay.text = woolCount.ToString();
    }

    // Get the updated resource count from various scripts calling this method
    public void UpdateResourceCount(int updatedBrickCount, int updatedLumberCount, int updatedOreCount, int updatedGrainCount, int updatedWoolCount)
    {
        brickCount = updatedBrickCount;
        lumberCount = updatedLumberCount;
        oreCount = updatedOreCount;
        grainCount = updatedGrainCount;
        woolCount = updatedWoolCount;
    }
}