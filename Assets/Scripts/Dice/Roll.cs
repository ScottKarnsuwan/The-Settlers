using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    public Button rollButton;
    public Button backButton;

    private Rigidbody m_Rigidbody;
    private float m_StillTimer;
    private string m_HighestFace;
    private float m_HighestFacePosition;

    // When this script is activated, disable the Roll and Back button
    // Apply gravity and a random torque to the die
    void OnEnable()
    {
        rollButton.interactable = false;
        backButton.interactable = false;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.useGravity = true;
        float X = UnityEngine.Random.Range(-500, 500);
        float Y = UnityEngine.Random.Range(-500, 500);
        float Z = UnityEngine.Random.Range(-500, 500);
        m_Rigidbody.AddTorque(X, Y, Z);
    }

    // Update is called once per frame
    // This checks whether the die has been still for 0.5 seconds
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

        if (m_StillTimer >= 0.5f)
        {
            this.enabled = false;
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
                m_HighestFace = child.name;
            }
        }
        m_Rigidbody.isKinematic = true;
        // FindObjectOfType<ResourceProduction>().enabled = true;
    }
}