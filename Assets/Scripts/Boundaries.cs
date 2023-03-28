using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour

{
    Vector3 diceVelocity;
    void FixedUpdate()
    {
        //Calling the other class to get the dice's velocity
        diceVelocity = Dice.diceVelocity;
    }

    
    void OnTriggerStay(Collider col)
    {
        //Check the side it landed on
        if(diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0)
        {
            switch(col.gameObject.name)
            {
                case "Side1":
                    DiceNumber.diceNumber = 5;
                    break;
                case "Side2":
                    DiceNumber.diceNumber = 4;
                    break;
                case "Side3":
                    DiceNumber.diceNumber = 6;
                    break;
                case "Side4":
                    DiceNumber.diceNumber = 2;
                    break;
                case "Side5":
                    DiceNumber.diceNumber = 1;
                    break;
                case "Side6":
                    DiceNumber.diceNumber = 3;
                    break;
            }
        }
    }
}
