using UnityEngine;
using System.Collections;

public class HealthRemains : MonoBehaviour
{

    public GameObject missile;
    SpriteRenderer sprite;
    Light particleLight;
    ParticleSystem particles;

    bool active;

    Health heroHp;
    float deathTimer;

    GameObject player;
    Options theOptions;

    // Use this for initialization
    void Start()
    {
        heroHp = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particleLight = gameObject.GetComponent<Light>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        active = true;
        deathTimer = 0;
        theOptions = GameObject.Find("TheOptions").GetComponent<Options>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            deathTimer += Time.deltaTime;

            particles.emissionRate = 0;
            sprite.enabled = false;
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

    //void OnTriggerEnter(Collider other)
    //{
    //    if (active)
    //    {
    //        if (other.tag == "Player")
    //        {
    //            PickUp();
    //        }
    //    }

    //}

    void PickUp()
    {
        Instantiate(missile, transform.position, transform.rotation);


        if(theOptions.easyMode == true)
        {
            heroHp.GainHealth(3.5f);
        }
        else
        {
            heroHp.GainHealth(2.5f); // CHANGE THIS LATER
        }
        active = false;
    }
}
