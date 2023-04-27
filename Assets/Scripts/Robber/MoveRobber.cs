using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveRobber : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject hexagonHighlight;
    public AudioSource endermanAudio;

    private Transform robber;
    private GameObject robberTile;

    // OnEnable is called whenever this script is activated
    void OnEnable()
    {
        robberTile = FindObjectOfType<RobberManager>().robberTile;
    }

    // This is activated when the mouse hovers over the terrain tile
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Make the highlight visible
        hexagonHighlight.SetActive(true);
    }

    // This is activated when the mouse no longer hovers over the terrain tile
    public void OnPointerExit(PointerEventData eventData)
    {
        // Make the highlight invisible again
        hexagonHighlight.SetActive(false);
    }

    // This is activated when the mouse clicks the terrain tile
    public void OnPointerClick(PointerEventData eventData)
    {
        endermanAudio.Play();

        hexagonHighlight.SetActive(false);

        robber = robberTile.transform.GetChild(robberTile.transform.childCount - 1);

        // Reposition the robber to be above this tile
        robber.position = new Vector3(gameObject.transform.position.x, 10.0f, gameObject.transform.position.z);

        // Make the robber a child of this tile
        robber.transform.parent = gameObject.transform;

        // Switch from the robber screen to the build and trade phase screen
        FindObjectOfType<ScreenManager>().DisableRobberScrreen();
    }
}