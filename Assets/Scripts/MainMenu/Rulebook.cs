using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Rulebook : MonoBehaviour
{ 
    //Array for pics
    public Sprite[] rules;
    //Index of first image
    public int index = 0;
    void Start()
    {
        // Set the first image in the array as the starting image
        GetComponent<Image>().sprite = rules[index];
        
    }
    void Update()
    {
       int temp = index;
        GetComponent<Image>().sprite = rules[temp];
        Debug.Log(index);
    }

    public void Increment()
    {
        // increment the index of the current image
        index++;
        
        //If we've reached the end of the array, wrap around to the beginning
        if (index >= rules.Length)
        {
            index= 0;
        }
        //Update the image component to display the new image
    }
    public void Decrement()
    {
        //Decrement
        index--;
        //Wraps around but in reverse
        if (index < 0)
        {
            index = rules.Length-1;
        }
    }

}
