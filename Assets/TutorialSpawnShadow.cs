using UnityEngine;
using System.Collections;

public class TutorialSpawnShadow : MonoBehaviour {

    public GameObject Shadow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   
    void OnTriggerEnter(Collider Other)
    {
        if(Other.tag == "Player")
        { 
        Instantiate(Shadow, new Vector3(19, -3, -1), new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
        }
    }
}
