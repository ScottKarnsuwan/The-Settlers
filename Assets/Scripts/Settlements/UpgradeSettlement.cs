using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeSettlement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Get a reference to city and the 'ghost city'
    public GameObject city;
    public GameObject cityHighlight;

    public AudioSource villagerNoise;

    private ArrayList builtSettlementsList = new ArrayList();

    private int brickCount;
    private int lumberCount;
    private int oreCount;
    private int grainCount;
    private int woolCount;

    // OnEnable is called whenever this script is activated
    void OnEnable()
    {
        // Get the builtSettlementsList
        builtSettlementsList = FindObjectOfType<SettlementManager>().builtSettlementsList;

        // Get the current resource count from the ResourceCounter script
        brickCount = FindObjectOfType<ResourceCounter>().brickCount;
        lumberCount = FindObjectOfType<ResourceCounter>().lumberCount;
        oreCount = FindObjectOfType<ResourceCounter>().oreCount;
        grainCount = FindObjectOfType<ResourceCounter>().grainCount;
        woolCount = FindObjectOfType<ResourceCounter>().woolCount;
    }

    // This is activated when the mouse hovers over the settlement
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Make the 'ghost city' visible
        cityHighlight.SetActive(true);

        villagerNoise.Play();
    }

    // This is activated when the mouse no longer hovers over the settlement
    public void OnPointerExit(PointerEventData eventData)
    {
        // Make the 'ghost city' invisible again
        cityHighlight.SetActive(false);
    }

    // This is activated when the mouse clicks the settlement
    public void OnPointerClick(PointerEventData eventData)
    {
        cityHighlight.SetActive(false);

        // Consume resources and tell the ResourceCounter script to update
        oreCount -= 3;
        grainCount -= 2;
        FindObjectOfType<ResourceCounter>().UpdateResourceCount(brickCount, lumberCount, oreCount, grainCount, woolCount);

        // Build the city
        Instantiate(city, gameObject.transform.position, Quaternion.identity);

        // Remove and destroy the settlement
        builtSettlementsList.Remove(gameObject);
        Destroy(gameObject);

        // Switch from the city building screen back to the build and trade phase screen
        FindObjectOfType<ScreenManager>().DisableSettlementUpgrading();
    }
}