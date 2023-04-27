using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCycle : MonoBehaviour
{
      public Sprite[] images; // an array of Sprites that will hold the different images

    void Start()
    {
        int randomIndex = Random.Range(0, images.Length); // choose a random index from the array
        GetComponent<Image>().sprite = images[randomIndex]; // set the Image component's sprite to the random image
    }

}
