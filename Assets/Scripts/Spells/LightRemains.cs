using UnityEngine;
using System.Collections;

public class LightRemains : MonoBehaviour
{
	public 	GameObject[] allObjects;
    PlayerLight heroLight;
    bool active;
    float deathTimer;

    ParticleSystem particles;
    Light particleLight;
    SpriteRenderer sprite;
    bool once;

   // public GameObject endParticles;
    public GameObject missile;

    GameObject player;

    void Start()
    {
        heroLight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLight>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particleLight = gameObject.GetComponent<Light>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        active = true;
        deathTimer = 0;
        once = true;

        player = GameObject.FindGameObjectWithTag("Player");
	 
		allObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects)
		{
			obj.SendMessage("AddDrop", this.gameObject, SendMessageOptions.DontRequireReceiver);
		}

    }

    void Update()
    {
        if (!active)
        {
            deathTimer += Time.deltaTime;

            particles.emissionRate = 0;
            sprite.enabled = false;

            if (once)
            {
               // Instantiate(endParticles, transform.position, new Quaternion(0, 0, 0, 0));
                once = false;
				allObjects = GameObject.FindObjectsOfType<GameObject>();
				foreach (GameObject obj in allObjects)
				{
					obj.SendMessage("RemoveDrop", this.gameObject, SendMessageOptions.DontRequireReceiver);
				}
            }

            particleLight.range -= Time.deltaTime;

            if (deathTimer > 0.7)
            {
				 
			
                Destroy(gameObject);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
        Instantiate(missile, transform.position, transform.rotation);
        heroLight.GainLight(18);
        active = false;
    }
}
