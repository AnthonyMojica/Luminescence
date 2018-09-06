using UnityEngine;
using System.Collections;

public class WindStaffParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //WORLD
        transform.Rotate(new Vector3(0, 600 * Time.deltaTime, 0));
	}
}
