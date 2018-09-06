using UnityEngine;
using System.Collections;

public class TrailBlazerExplosion : MonoBehaviour {


    GameObject player;

    Vector3 origin;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -0.7f);

        Vector3 temp = Vector3.Normalize(transform.position - origin);
       // transform.rotation = Quaternion.LookRotation(temp);

        Vector3 temp2 = transform.eulerAngles;
        temp2.x = 0;
        temp2.y = 0;
        temp2.z = temp.z * 10000;
        transform.eulerAngles = temp2;
        
	}
}
