using UnityEngine;
using System.Collections;

public class TrailLightRemains : MonoBehaviour
{

    PlayerLight heroLight;
    bool active;

    float deathTimer;
    ParticleSystem particles;
    Light particleLight;
    SpriteRenderer sprite;
    bool once;
    public GameObject missile;
    public GameObject trailParticles;

    GameObject player;

    void Start()
    {
        heroLight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLight>();
        player = GameObject.FindGameObjectWithTag("Player");
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particleLight = gameObject.GetComponent<Light>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        active = true;
        deathTimer = 0;
        once = true;
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
                Instantiate(trailParticles, transform.position, new Quaternion(0, 0, 0, 0));
                once = false;
            }

            particleLight.range -= Time.deltaTime;

            if (deathTimer > 0.7)
            {
                Destroy(gameObject);
            }


        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 0.8f)
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
        //The dash currently spawns 11 of these as of 4/22/15
        Instantiate(missile, transform.position, transform.rotation);
        heroLight.GainLight(1);
        active = false;
    }
}
