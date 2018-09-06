using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null && other != this && other.GetComponent<AISkeletonArcher>() != null)
        {
            AISkeletonArcher ai = other.GetComponent<AISkeletonArcher>();
            if (ai)
            {
                ai.usedWaypoints.pushBack(gameObject);
            }
            //other.GetComponent<AISkeletonArcher>().usedWaypoints.pushBack(this.gameObject);
        }
    }
}
