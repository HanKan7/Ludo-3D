using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoute : MonoBehaviour
{
    public RouteManager routeManager;
    Dice dice;
    GameManager gameManager;

    public float distanceFactor;
    public bool isMoving;
    public int myTurnNumber;
    public int nodeNumber = 0;

    public bool isClicked = false;
    public bool movedToFirstNode = false;
    public bool willGetAnotherChance = false;
    public bool isSafe = false;
    //public bool canPlay = false;
   
    public Vector3 heightNode = new Vector3(0, 0.75f, 0);
    private Vector3 homePos;
    private Vector3 startNode = Vector3.zero;

    Animator anim;


    private void Start()
    {
        dice = FindObjectOfType<Dice>();
        gameManager = FindObjectOfType<GameManager>();
        homePos = transform.position;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isMoving == false && gameManager.whoseTurnItIs == myTurnNumber && dice.haveToThrowDiceAgain == true)
        {
            if (movedToFirstNode == true) anim.Play("SphereBoing");
            else if(dice.diceNumber == 6) anim.Play("SphereBoing");
            else if (dice.diceNumber != 6) anim.Play("Idle");
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SafeSpot")
        {
            isSafe = true;
            return;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SafeSpot")
        {
            isSafe = true;
            return;
        }

        if (other.gameObject.tag != transform.tag)  //some other ball has hit 
        {

            PlayerRoute reference = other.gameObject.GetComponent<PlayerRoute>();
            Debug.Log("Collided with " + reference.gameObject.transform.name + "ball");
            if (reference.isSafe == true) return;
            //if (reference.isSafe == false && isMoving == false && gameManager.whoseTurnItIs - 1 == myTurnNumber)
            //{
            //    //Debug.Log("Collided with " + reference.gameObject.transform.name + "ball");
            //    reference.StartCoroutine(Move(reference.homePos, 1));
            //}
            int number = gameManager.whoseTurnItIs;
            Debug.Log("  Object name  " + transform.name + "  Number is  " + number + "  MyturnNumber is   " + myTurnNumber);
            if (isSafe == false && reference.isMoving == false && (number - 1) != myTurnNumber)         // should kill
            {
                if(myTurnNumber != 4)   //green shoudnt hit red
                {
                    Debug.Log("Killed  " + transform.name + "ball");
                    movedToFirstNode = false;
                    StartCoroutine(Move(homePos, 1));
                    nodeNumber = 0;
                    startNode = Vector3.zero;
                }

            }

            else     //
            {
                Debug.Log("My name is " + transform.name);
                if (isSafe == false && reference.isMoving == false && (number + 3) >= myTurnNumber) // blue kills green. Green gets called
                {
                    Debug.Log("Killedd " + transform.name + "ball");
                    StartCoroutine(Move(homePos, 1));
                    nodeNumber = 0;
                    startNode = Vector3.zero;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "SafeSpot")
        {
            isSafe = false;
        }
    }

    private void OnMouseDown()
    {
        if (isMoving == false && gameManager.whoseTurnItIs == myTurnNumber && dice.haveToThrowDiceAgain == true)
        {
            MoveOneNodeToOther(dice.diceNumber);
            ChangeTurn();
        } 

    }

    void MoveOneNodeToOther(int moveCount)
    {
        if(movedToFirstNode == false)   //moves to the first node
        {
            if (moveCount == 6)
            {
                startNode = routeManager.childNodeList[nodeNumber].position + heightNode;
                movedToFirstNode = true;
                willGetAnotherChance = true;
                StartCoroutine(Move(startNode, 1));
                return;
            }
            else
            {
                //gameManager.whoseTurnItIs++;      //goes to next player if its not 6
                return;
            }

        }
        StartCoroutine(Move(startNode , moveCount));
          
    }

    IEnumerator Move(Vector3 goalPos , int moveCount)
    {
        //Debug.Log("Moving " + transform.name);
        if (moveCount == 0)
        {
            isMoving = false;
            yield break;
        }
        isMoving = true;
        
        for (int i = 0; i < 1000; i++) //moves to next node
        {
            if (transform.position == goalPos) break;
           // Debug.Log("FOR startNode = " + startNode + "corcount = " + corCOunt);
            transform.position = Vector3.MoveTowards(transform.position, goalPos, distanceFactor * Time.deltaTime);
            yield return new WaitForSeconds(0.00001f);
        }
        nodeNumber++;
        startNode = routeManager.childNodeList[nodeNumber].position + heightNode;
        //Debug.Log("NEW startNode = " + startNode);
        moveCount--;
        StartCoroutine(Move(startNode, moveCount));
    }

    void ChangeTurn()
    {
        
        if (movedToFirstNode == true && willGetAnotherChance == true)
        {
            willGetAnotherChance = false;   //wont increase the whoseTurnItIs
            dice.haveToThrowDiceAgain = false;
            return;
        }

        if (gameManager.whoseTurnItIs == 4) // green to red turn
        {
            gameManager.whoseTurnItIs = 1;
            dice.haveToThrowDiceAgain = false;
            return;
        }
        gameManager.whoseTurnItIs++;    //Next Turn
        dice.haveToThrowDiceAgain = false;
        return;
    }
}
