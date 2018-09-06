using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	 
	public Vector3 scrolling= 	new Vector3(0.0f,0.1f,0.0f);
	public GameObject[] creds;
	public GameObject[] Buttons= new GameObject[3];
	public AudioClip clip;
	public bool playing=false;

	// Use this for initialization
	void Start () 
    {

        creds = GameObject.FindGameObjectsWithTag("Chest");
    	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < 2; i++) {
			
		
			creds[i].transform.position = new Vector3 (creds[i].transform.position.x, -130, creds[i].transform.position.z);
			if ((Input.GetButtonDown ("CMenuAccept") || Input.GetButtonDown ("KBMenuAccept")) || (Input.GetButtonDown ("CPause") || Input.GetButtonDown ("KBPause")) || (Input.GetKeyDown (KeyCode.Mouse0))) {
				Application.LoadLevel ("MainMenu");

			}
			creds[i].transform.Translate (scrolling);
			scrolling.y += 60.0f * Time.deltaTime;
	 
			if (creds[i].transform.position.y >= Screen.height * 4.5f) {
				scrolling.y = -130.0f;
                GameObject.Find("TheOptions").GetComponent<Options>().WatchCredits();

			}
		 
	 

 
		}
	}
 


	}

