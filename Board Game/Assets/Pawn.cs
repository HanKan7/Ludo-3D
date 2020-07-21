using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    int routePos;

    public Route route;

    public int steps;

    bool isMoving;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMoving == false)
        {
            steps = Random.Range(1, 7);
            Debug.Log(steps);

            if (routePos + steps < route.childNodeList.Count)
            {
                StartCoroutine(Move());
            }

            else
            {
                Debug.Log("Rolled Number is too high");
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving) yield break;

        isMoving = true;

        while(steps > 0)
        {
            Vector3 nextPos = route.childNodeList[routePos + 1].position;
            while(MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePos++;
        }

        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }
}
