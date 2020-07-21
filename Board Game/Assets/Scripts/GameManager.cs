using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int whoseTurnItIs = 1; // red = 1, yellow = 2 , blue = 3 , green = 4
 
    Dice dice;

    private void Start()
    {
       // players = FindObjectsOfType<PlayerRoute>();
        dice = GetComponent<Dice>();
    }

    private bool IsValidPlayer(PlayerRoute player)
    {
        //If he's home and dice is not 6


        //Check if it's not exceeding goal

        bool valid = true;

      //  if (valid)
        //    player.BoxCollider = true;
        return true;
    }
    public bool ValidRoll()
    {
        if (dice.gameHasStarted)
        {
            PlayerRoute[] players = FindObjectsOfType<PlayerRoute>();

            foreach (PlayerRoute player in players)
            {
                if (player.myTurnNumber == whoseTurnItIs)
                {
                    if (player.nodeNumber != 0)
                        return true;
                }
            }
        }
        return false;
    }

}
