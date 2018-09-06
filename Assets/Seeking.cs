using UnityEngine;
using System.Collections;

public class Seeking : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float damage;
    public float range;
    public GameObject explosion;
    PlayerLight theLight;
    public GameObject the_remains;
    public GameObject Lorne;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        theLight = player.GetComponent<PlayerLight>();
        
    }






    void FixedUpdate()
    {
		if (player == null)
		{
        player = GameObject.FindGameObjectWithTag("Player");
			if(player!=null)
			theLight = player.GetComponent<PlayerLight>();
			
		}

        transform.up = (player.transform.position - transform.position).normalized;
        transform.position += transform.up * speed * Time.deltaTime;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode();
        }
        else if (other.tag == "Player")
        {
          int temp=  (int)theLight.currentLight / 18;
            theLight.LoseLight(theLight.currentLight);
            for (int i = 0; i < temp; i++)
            {
            GameObject temps=    (GameObject)Instantiate(the_remains, transform.position + new Vector3(Random.value * Random.Range(0, 4), Random.value * Random.Range(0, 4), transform.position.z),transform.rotation); 
                if(temps.transform.position.x<-5||temps.transform.position.x>10||temps.transform.position.y<-10||temps.transform.position.y>10)
                {
                    int forchoosing = Random.Range(0, 4);
                    switch(forchoosing)
                    {
                        case 0:
                            temps.transform.position= new Vector3(0,0, temps.transform.position.z);
                            break;
                        case 1:
                            temps.transform.position = new Vector3(3, 0, temps.transform.position.z);

                            break;
                        case 2:
                            temps.transform.position = new Vector3(1, 3, temps.transform.position.z);

                            break;
                        case 3:
                            temps.transform.position = new Vector3(4, 8, temps.transform.position.z);

                            break;

                    }
                }
            }
            Explode();
        }
    }
    void Explode()
    {
        Camera.main.SendMessage("ScreenShake", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
