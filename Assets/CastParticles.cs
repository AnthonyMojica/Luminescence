using UnityEngine;
using System.Collections;

public class CastParticles : MonoBehaviour {

    GameObject player;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;
	}
}
