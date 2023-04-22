using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Road : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Get a reference to the 'ghost road'
    public GameObject highlight;

    // An arraylist to contain all of the roads that have been instantiated into the scene
    private ArrayList roadsList = new ArrayList();

    private int brickCount;
    private int lumberCount;
    private int oreCount;
    private int grainCount;
    private int woolCount;

    // OnEnable is called whenever this script is activated
    void OnEnable()
    {
        // Get the roadsList from the RoadManager script
        roadsList = FindObjectOfType<RoadManager>().roadsList;

        // Get the current resource count from the ResourceCounter script
        brickCount = FindObjectOfType<ResourceCounter>().brickCount;
        lumberCount = FindObjectOfType<ResourceCounter>().lumberCount;
        oreCount = FindObjectOfType<ResourceCounter>().oreCount;
        grainCount = FindObjectOfType<ResourceCounter>().grainCount;
        woolCount = FindObjectOfType<ResourceCounter>().woolCount;

    }

    // This is activated when the mouse hovers over the road
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Make the 'ghost road' visible
        highlight.SetActive(true);
    }

    // This is activated when the mouse no longer hovers over the road
    public void OnPointerExit(PointerEventData eventData)
    {
        // Make the 'ghost road' invisible again
        highlight.SetActive(false);
    }

    // This is activated when the mouse clicks the road
    public void OnPointerClick(PointerEventData eventData)
    {
        // Make the road visible
        GetComponentInChildren<Renderer>().enabled = true;

        // Consume resources and tell the ResourceCounter script to update
        brickCount--;
        lumberCount--;
        FindObjectOfType<ResourceCounter>().UpdateResourceCount(brickCount, lumberCount, oreCount, grainCount, woolCount);

        // Remove this road from the roadsList
        // This is to prevent the same road from being built twice
        int roadNumber = int.Parse(gameObject.name.Substring(4));
        roadsList.RemoveAt(roadNumber);

        // Switch from the road building screen back to the build and trade phase screen
        FindObjectOfType<ScreenManager>().DisableRoadBuilding();

        // Disable this script attached to this road
        enabled = false;
    }
}