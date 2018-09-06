using UnityEngine;
using System.Collections;

public class EarthEmber : MonoBehaviour {

	// Use this for initialization
    public float timer;
    GameObject[] enemies;
    public float distance;
    public float range=1.0f;
    public float damage = 7.0f;
	void Start () 
    {
        timer = 1.0f;
        Camera.main.SendMessage("ScreenShake");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            distance = (transform.position - enemies[i].transform.position).magnitude;
            if(distance<range)
            {
                enemies[i].GetComponent<Health>().LoseHealth(damage);
                enemies[i].SendMessage("GetWrecked",SendMessageOptions.DontRequireReceiver);
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
         timer -= Time.deltaTime;
        if (timer <= 0.0f)
            Destroy(this.gameObject);

	}
}


 
