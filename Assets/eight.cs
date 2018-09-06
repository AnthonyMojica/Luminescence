using UnityEngine;
using System.Collections;

public class eight : MonoBehaviour {

	public float kek;
	public float keks;
	public float kekekeke;
	bool goingright=true;
	// Use this for initialization
	void Start () {
		keks = 1.0f;
		kekekeke = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		kek = (Mathf.Cos (keks+=Time.deltaTime*5));
	 
		if (kekekeke <= 4 && goingright)
			transform.position = new Vector3 (kekekeke += 0.1f, kek, transform.position.z);
		else if (kekekeke > -4.0f) {
			goingright = false;
			transform.position = new Vector3 (kekekeke -= 0.1f, kek, transform.position.z);
		} else
			goingright = true;
	
	
	}
}
