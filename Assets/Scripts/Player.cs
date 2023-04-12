using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int settlements = 5;
    private int cities = 4;
    private int roads = 15;
    private Boolean canPlace = false;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                String pos = (mousePos.x + " " + mousePos.y);
                Debug.Log(pos);
            }

        }
    }
}

