using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiText : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
        Camera.main.orthographic = true; // set the camera to orthographic mode
        Camera.main.orthographicSize = canvas.GetComponent<RectTransform>().rect.height / 2; // set the size of the camera to match the canvas height
    }

    void LateUpdate()
    {
        transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y, -10f); // set the position of the camera to match the canvas position and move it back in z direction
    }
}

