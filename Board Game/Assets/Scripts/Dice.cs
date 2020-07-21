using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int diceNumber;
    public bool gameHasStarted = false;
    public bool haveToThrowDiceAgain = false;
    public bool diceReady = true;
    public bool TeamRedHasPlayed = false, TeamYellowHasPlayed = false, TeamBlueHasPlayed = false, TeamGreenHasPlayed = false;
    // public bool isExpired = 
    GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && haveToThrowDiceAgain == false)
        {
            //diceReady = false;
            gameHasStarted = true;
            haveToThrowDiceAgain = true;
            diceNumber = Random.Range(1, 7);

            Debug.Log("Rolled Number is " + diceNumber);
            //Debug.Log("Is valid roll " + gm.ValidRoll());

            if (diceNumber != 6 && !gm.ValidRoll())
            {
                if (gm.whoseTurnItIs == 3)
                {
                    gm.whoseTurnItIs = 4;
                    haveToThrowDiceAgain = false;
                }

                else
                {
                    gm.whoseTurnItIs = (gm.whoseTurnItIs + 1) % 4;
                    haveToThrowDiceAgain = false;
                }
                    
            }
        }
    }

}

