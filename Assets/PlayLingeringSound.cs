using UnityEngine;
using System.Collections;

public class PlayLingeringSound : MonoBehaviour {


    public AudioSource thing;
    public AudioClip DeathSound;
	// Use this for initialization
	void Start () {
        thing.PlayOneShot(DeathSound);
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
