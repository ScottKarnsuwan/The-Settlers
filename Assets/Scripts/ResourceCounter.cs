using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    public TextMeshProUGUI brickCountDisplay;
    public TextMeshProUGUI lumberCountDisplay;
    public TextMeshProUGUI oreCountDisplay;
    public TextMeshProUGUI grainCountDisplay;
    public TextMeshProUGUI woolCountDisplay;

    private int brickCount;
    private int lumberCount;
    private int oreCount;
    private int grainCount;
    private int woolCount;

    // Update is called once per frame
    void Update()
    {
        brickCount = FindObjectOfType<ResourceProduction>().brickCount;
        lumberCount = FindObjectOfType<ResourceProduction>().lumberCount;
        oreCount = FindObjectOfType<ResourceProduction>().oreCount;
        grainCount = FindObjectOfType<ResourceProduction>().grainCount;
        woolCount = FindObjectOfType<ResourceProduction>().woolCount;

        brickCountDisplay.text = brickCount.ToString();
        lumberCountDisplay.text = lumberCount.ToString();
        oreCountDisplay.text = oreCount.ToString();
        grainCountDisplay.text = grainCount.ToString();
        woolCountDisplay.text = woolCount.ToString();
    }
}