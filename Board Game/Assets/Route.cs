using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childNodeList = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillNode();

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

    void FillNode()
    {
        childNodeList.Clear();
        childObjects = GetComponentsInChildren<Transform>();

        foreach(Transform child in childObjects)
        {
            if(child != transform)
            {
                childNodeList.Add(child);
            }
        }

    }
}
