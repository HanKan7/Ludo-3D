using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
     public List<Transform> childNodeList = new List<Transform>();
     Transform[] childObjectsTransform;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        FillNodes();


        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentNode = childNodeList[i].position;

            if(i > 0)
            {
                Vector3 prevNode = childNodeList[i - 1].position;

                Gizmos.DrawLine(prevNode, currentNode);
            }
        }

    }

    void FillNodes()
    {
        childNodeList.Clear();
        childObjectsTransform = GetComponentsInChildren<Transform>();

        for (int i = 1; i < childObjectsTransform.Length; i++)
        {
            childNodeList.Add(childObjectsTransform[i]);
        }

    }
}
