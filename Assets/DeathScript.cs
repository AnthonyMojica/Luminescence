using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {


    public float deathinfectrange;
    public float deathtimer;
	// Use this for initialization
	void Start () {
        deathinfectrange = 2.0f;
        deathtimer = 10.0f;
	
	}
	
	// Update is called once per frame
	void Update () {
        deathtimer -= Time.deltaTime;
        if (deathtimer > 0.0f)
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                Vector3 dist = transform.position - obj.transform.position;
                if (obj.tag == "Enemy" && dist.magnitude < deathinfectrange)
                    obj.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
            }
        }
      
	
	}
}
