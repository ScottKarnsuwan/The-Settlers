using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    public Button rollButton;
    private Rigidbody m_Rigidbody;

    // When this script is activated reset the die's position and rotation
    // Make the Roll button interactable and disable gravity for the die
    void OnEnable()
    {
        rollButton.interactable = true;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = false;
    }

    // Update is called once per frame
    // Rotate the die with a random speed to make the result more random
    void Update()
    {
        int rand = UnityEngine.Random.Range(5, 15);
        transform.Rotate(new Vector3(15 * rand, 30 * rand, 45 * rand) * Time.deltaTime);
        m_Rigidbody.AddTorque(100, 100, 100);
    }
}
