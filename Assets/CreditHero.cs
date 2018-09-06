using UnityEngine;
using System.Collections;

public class CreditHero : MonoBehaviour {
	Vector3 scrolling= 	new Vector3(0.0f,0.01f,0.0f);
	public float posy;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, -10.0f, transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () {
		posy = transform.localPosition.y;
		 transform.Translate (scrolling*Time.deltaTime);
		scrolling.y += Time.deltaTime;
		if (transform.localPosition.y > Screen.height/100) 
		{
			transform.position = new Vector3 (transform.position.x, -10.0f, transform.position.z);
			scrolling = new Vector3 (0.0f, 0.01f, 0.0f);
		}

	
	}
}
