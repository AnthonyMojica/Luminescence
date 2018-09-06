using UnityEngine;
using System.Collections;

public class SmalllightExplosionScript : MonoBehaviour {


    //ParticleSystem theParts;
    Light thelight;
    float timer;
	// Use this for initialization
	void Start () 
    {
        //theParts = gameObject.GetComponent<ParticleSystem>();
        thelight = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        thelight.range -= 2 * Time.deltaTime;
        Destroy(gameObject, 1);
	}
}
