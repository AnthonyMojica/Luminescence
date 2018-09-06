using UnityEngine;
using System.Collections;

public class ThereCanOnlyBeOneChest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject doomedChest = GameObject.FindGameObjectWithTag("Chest");
        if (gameObject != doomedChest)
            Destroy(doomedChest);
	}
}
