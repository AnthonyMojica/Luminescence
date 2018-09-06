using UnityEngine;
using System.Collections;

public class CutsceneEnemies : MonoBehaviour {

    Vector3 Direction;
    Vector3 StartPOS;
    Vector3 EndPOS;

    int RandX, RandY;
    float speed;
    float timer;
    bool halt;
	// Use this for initialization
	void Start () 
    {
        halt = false;
        timer = 2;
        if (gameObject.tag == "Player")
        {
            
            StartPOS = new Vector3(16, 0, -3);
            EndPOS = new Vector3(2, 0, -3);
            transform.position = StartPOS;
            Direction = EndPOS - StartPOS;
            speed = .25f;
        }
        else
        {
            RandX = Random.Range(15, 32);
            RandY = Random.Range(-11, 11);
            StartPOS = new Vector3(RandX, RandY, -3);
            transform.position = StartPOS;

            RandX = -22;
            RandY = Random.Range(-11, 11);
            EndPOS = new Vector3(RandX, RandY, -3);

            Direction = EndPOS - StartPOS;
            speed = Random.Range(1, 3);
            speed /= 10;
        }


        
	}
	
	// Update is called once per frame
	void Update () 
    {


        if (gameObject.tag == "Player")
        {

            if (transform.position.x < 2 && halt == false)
                halt = true;

            if (timer < 0)
                halt = false;
            if(halt)
            {
                timer -= Time.deltaTime;
            }
            else
                transform.position += Direction * speed * Time.deltaTime;

            
        }
        else
        {
            transform.position += Direction * speed * Time.deltaTime;
        }
       
        
	}
}
