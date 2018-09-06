using UnityEngine;
using System.Collections;

public class SpellSnare : MonoBehaviour
{

    public float speed;
    public float damage;
    public float range;
    public float growthRate;
    float distanceTraveled;

    public GameObject explosion;
    public GameObject lightRemains;
    PlayerEquipment heroEquipment;
    PlayerStats theStats;
    public GameObject burns;
    PlayerSpellCasting pSpells;

    void Start()
    {
        theStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        damage = 5.0f;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        pSpells = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpellCasting>();
        distanceTraveled = 0;
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            transform.localScale += new Vector3(growthRate * Time.deltaTime, growthRate * Time.deltaTime, growthRate * Time.deltaTime);
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
    }

    void Explode()
    {

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, Enemies[i].transform.position) < 2.5f)
            {
                Enemies[i].SendMessage("Snare", SendMessageOptions.DontRequireReceiver);
                Enemies[i].GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
        }

        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(burns, new Vector3(transform.position.x, transform.position.y, -0.5f), new Quaternion(0, 0, 0, 0));

        if (pSpells.chained == false)
        {
            Instantiate(gameObject, transform.position, transform.rotation);
            pSpells.chained = true;
        }
        else
        {
            Instantiate(lightRemains, transform.position, transform.rotation);

        }

        Destroy(gameObject);
    }
}



