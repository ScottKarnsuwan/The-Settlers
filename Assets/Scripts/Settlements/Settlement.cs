using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Settlement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Get a reference to the settlement mesh
    public GameObject settlementMesh;

    // Get a reference to the 'ghost settlement'
    public GameObject highlight;

    // An arraylist to contain all of the settlements that have been instantiated into the scene
    private ArrayList settlementsList = new ArrayList();

    private int brickCount;
    private int lumberCount;
    private int oreCount;
    private int grainCount;
    private int woolCount;

    // OnEnable is called whenever this script is activated
    void OnEnable()
    {
        // Get the settlementsList from the SettlementManager script
        settlementsList = FindObjectOfType<SettlementManager>().settlementsList;

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
        // Make the 'ghost settlement' visible
        highlight.SetActive(true);
    }

    // This is activated when the mouse no longer hovers over the settlement
    public void OnPointerExit(PointerEventData eventData)
    {
        // Make the 'ghost settlement' invisible again
        highlight.SetActive(false);
    }

    // This is activated when the mouse clicks the settlement
    public void OnPointerClick(PointerEventData eventData)
    {
        highlight.SetActive(false);

        // Make the settlement visible
        settlementMesh.SetActive(true);

        // Consume resources and tell the ResourceCounter script to update
        brickCount--;
        lumberCount--;
        grainCount--;
        woolCount--;

        FindObjectOfType<ResourceCounter>().UpdateResourceCount(brickCount, lumberCount, oreCount, grainCount, woolCount);

        // Remove this settlement from the settlementsList
        // This is to prevent the same settlement from being built twice
        settlementsList.Remove(gameObject);

        // Switch from the settlement building screen back to the build and trade phase screen
        FindObjectOfType<ScreenManager>().DisableSettlementBuilding();

        // Disable this script attached to this settlement
        enabled = false;
    }
}