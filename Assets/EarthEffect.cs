using UnityEngine;
using System.Collections;

public class EarthEffect : MonoBehaviour {

 

	// Use this for initialization
 
    GameObject[] enemies;
    public float distance;
    public float range=1.0f;
    public float damage = 7.0f;
	void Start () 
    {
         
        Camera.main.SendMessage("ScreenShake");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            distance = (transform.position - enemies[i].transform.position).magnitude;
            if(distance<range)
            {
                enemies[i].GetComponent<Health>().LoseHealth(damage);
                           }

        }
	}
	
	// Update is called once per frame
	void Update () {

 
	}
}
