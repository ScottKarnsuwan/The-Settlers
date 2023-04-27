using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataPassOn : MonoBehaviour
{
    //This is data passing on to the main game
    [SerializeField]
    //Count how many players
    public static int playerCount;
    public void DataOutput(int val)
    {
        //A case for each drop down option
        switch (val)
        {
            case 0: Debug.Log("Single player");
                playerCount = 1;
                break;
            case 1: Debug.Log("Two players");
                playerCount = 2;
                break;
            case 2: Debug.Log("Three players");
                playerCount = 3;
                break;
            case 3: Debug.Log("Four players");
                playerCount = 4;
                break;
            case 4: Debug.Log("Five players");
                playerCount = 5;
                break;
            case 5: Debug.Log("Six players");
                playerCount = 6;
                break;
        }


    }
}
