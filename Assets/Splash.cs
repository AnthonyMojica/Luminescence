using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    public float timer;
	// Use this for initialization
	void Start () {
        timer = 3.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer<=0)
        {

            Application.LoadLevel("MainMenu");
        }
	
	}
}
