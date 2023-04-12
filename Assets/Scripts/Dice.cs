using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    public Boolean roll = true;

    void Start()
    //Initialization
    {rb = GetComponent<Rigidbody>();}

    void Update()
    {
        // If space is pressed it will roll the dice
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // Dice attributes when it rolls
            DiceNumber.diceNumber = 0;
            diceVelocity = rb.velocity;
            float X = UnityEngine.Random.Range(0, 500);
            float Y = UnityEngine.Random.Range(0, 500);
            float Z = UnityEngine.Random.Range(0, 500);
            transform.position = new Vector3(34.8f, 18.5f, -18);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(X, Y, Z);
        }     
    }
}
        
    
