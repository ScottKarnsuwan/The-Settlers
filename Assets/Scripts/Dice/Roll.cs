using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    // Get a reference to the Roll and Back buttons
    public Button rollButton;
    public Button backButton;

    [HideInInspector] public int highestFaceNumber;

    private Rigidbody m_Rigidbody;
    private float m_StillTimer;
    private float m_HighestFacePosition;
    private string m_HighestFaceString;

    // OnEnable is called whenever this script is activated
    // In this case it's called when the Roll button is clicked
    void OnEnable()
    {   
        // Disable the Roll and Back button
        rollButton.interactable = false;
        backButton.interactable = false;

        // Reset the result of the previous roll to 0
        highestFaceNumber = 0;

        // Enable the ResourceProduction script
        FindObjectOfType<ResourceProduction>().enabled = true;

        // Apply gravity and a random torque to the die
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.useGravity = true;
        float X = UnityEngine.Random.Range(-500, 500);
        float Y = UnityEngine.Random.Range(-500, 500);
        float Z = UnityEngine.Random.Range(-500, 500);
        m_Rigidbody.AddTorque(X, Y, Z);
    }

    // Update is called once per frame
    // This checks whether the die has been still for 0.5 seconds or more
    // If it has then disable this script, which will call the OnDisable() method
    void Update()
    {
        if (gameObject.transform.hasChanged)
        {
            m_StillTimer = 0f;
            gameObject.transform.hasChanged = false;
        }
        else
        {
            m_StillTimer += Time.deltaTime;
        }

        if (m_StillTimer >= 0.4f)
        {
            enabled = false;
        }
    }

    // This is called once the die stops moving
    // It checks each face of the die to find whichever is highest
    void OnDisable()
    {
        m_HighestFacePosition = -Mathf.Infinity;
        foreach (Transform child in gameObject.transform)
        {
            if (child.transform.position.y > m_HighestFacePosition)
            {
                m_HighestFacePosition = child.transform.position.y;
                m_HighestFaceString = child.name;
            }
        }
        highestFaceNumber = int.Parse(m_HighestFaceString);
        m_Rigidbody.isKinematic = true;
        backButton.interactable = true;
    }
}