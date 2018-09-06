using UnityEngine;
using System.Collections;

public class FairyPushOrb : MonoBehaviour
{


    public float speed;
    public float damage;
    public float range;
    float timer;
    float distanceTraveled;

    public GameObject explosion;
    public GameObject hazard;
    public GameObject eqp;
    PlayerEquipment heroEquipment;

    void Start()
    {
        timer = 0;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        distanceTraveled = 0;
        range = Random.Range(5, 8);
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (heroEquipment.paused == false)
        {
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
        if (timer < 0)
        {
            timer = .05f;
            Instantiate(explosion, transform.position, transform.rotation);
            GameObject tmp = (GameObject)Instantiate(hazard, transform.position, transform.rotation);
            Destroy(tmp, 2);
        }
        Destroy(gameObject, .2f);
    }
}
