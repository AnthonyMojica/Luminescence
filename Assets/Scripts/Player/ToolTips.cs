using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolTips : MonoBehaviour {

	public GameObject[] objects;
    GameObject DisplayThisToolTip;
    float ShortestDistance;
    float CurrItemDistance;
	// Use this for initialization
	void Start () {

        ShortestDistance = 1000000;
	}
	
	// Update is called once per frame
	void Update () {

        ShortestDistance = 100000;
        objects = GameObject.FindGameObjectsWithTag("PickUp");
        for(int i = 0; i< objects.Length; i++)
        {

            CurrItemDistance = Vector3.Distance(objects[i].transform.position, transform.position);

            if(CurrItemDistance < ShortestDistance)
            {
                DisplayThisToolTip = objects[i];
                ShortestDistance = CurrItemDistance;
            }
        }



        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SendMessage("DoNotDisplayTooltip", SendMessageOptions.DontRequireReceiver);
        }
        if(ShortestDistance < 0.5f)
        DisplayThisToolTip.SendMessage("DisplayTooltip", SendMessageOptions.DontRequireReceiver);
	}

}
