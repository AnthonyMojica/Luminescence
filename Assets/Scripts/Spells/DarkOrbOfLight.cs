using UnityEngine;
using System.Collections;

public class DarkOrbOfLight : MonoBehaviour {


    public float speed;
    public float damage;
    public float range;

    float distanceTraveled;

    public GameObject explosion;
    public GameObject hazard;
    public GameObject eqp;
    PlayerEquipment heroEquipment;

    void Start()
    {
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        distanceTraveled = 0;
        range = Random.Range(5, 8);
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            if(name.Contains("Morrius"))
            {
                Health MorHealth = GameObject.Find("Morrius(Clone)").GetComponent<Health>();
                if (MorHealth != null)
                {
                    if (MorHealth.healthPercent <= .5f)
                    {
                        speed = 4;
                    }
                    if (MorHealth.healthPercent <= .25f)
                    {
                        speed = 6;
                    }
                }
            }
            transform.position += transform.up * speed * Time.deltaTime;
            distanceTraveled += speed * Time.deltaTime;

            if (distanceTraveled >= range)
            {
                Explode();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Explode();
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Health>().LoseHealth(damage);
            Explode();
        }
    }

    void Explode()
    {
        GameObject exp = (GameObject)Instantiate(explosion, transform.position, transform.rotation);

        if (this.name.Contains("Morrius"))
        {
            exp.GetComponent<DarkOrbExplosion>().expRadius = 2.4f;
        }
        else
        {
            exp.GetComponent<DarkOrbExplosion>().expRadius = 1.2f;
        }


        if(hazard != null)
         Instantiate(hazard, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
