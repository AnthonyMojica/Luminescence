using UnityEngine;
using System.Collections;

public class TrailProjectile : MonoBehaviour
{

    public float MaxTimeActive;
    float timeAlive;

    ParticleSystem particles;
    Light particleLight;

    public GameObject TrailLightRemains;

    PlayerEquipment eqp;
    float damage;
    bool once;
    public bool isWindEmber;
    PlayerStats theStats;

    public GameObject debuff;



    void Start()
    {
        theStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        damage = 1.0f;
        //Orient the thing to look correct
        if (isWindEmber)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            particles = gameObject.GetComponentInChildren<ParticleSystem>();
        }
        else
        {
            transform.Rotate(new Vector3(270, 0, 0));
            particles = gameObject.GetComponent<ParticleSystem>();
        }
        particleLight = gameObject.GetComponent<Light>();
        once = true;

        eqp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }
    void Update()
    {
        if (eqp.paused == false)
        {
            timeAlive += Time.deltaTime;

            //Turn off the particles early to make deletion look more smooth
            if (timeAlive >= (MaxTimeActive - 0.7))
            {
                particles.emissionRate = 0;

                if (once)
                {
                    Instantiate(TrailLightRemains, transform.position, new Quaternion(0, 0, 0, 0));
                    once = false;
                }

                particleLight.range -= Time.deltaTime;
            }

            if (timeAlive > MaxTimeActive)
            {
                //Spawn the trail light remains and destory the gameobject
                Destroy(gameObject);
            } 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        eqp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();


        //If the trail is still in its damage phase
        if (timeAlive < (MaxTimeActive - 0.7))
        {
            if (other.tag == "Enemy")
            {


                if (eqp.equippedEmber == ember.None)
                {
                }
                else if (eqp.equippedEmber == ember.Fire)
                {
                    GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                    tempObj.GetComponent<DebuffFire>().target = other.gameObject;
                }
                else if (eqp.equippedEmber == ember.Ice)
                {
                    GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                    tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
                }
                else if (eqp.equippedEmber == ember.Wind)
                {
                    other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                }

                else if (eqp.equippedEmber == ember.Earth)
                {
                    other.GetComponent<Health>().LoseHealth(0.3f);
                }
                else if (eqp.equippedEmber == ember.Death)
                {
                    other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                }
                else if (eqp.equippedEmber == ember.Life)
                {
                }

                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier / 3.0f);
                timeAlive = (MaxTimeActive - 0.7f);
            }
        }
    }
}
