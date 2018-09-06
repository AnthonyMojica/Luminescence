using UnityEngine;
using System.Collections;

public class SpellLightBolt : MonoBehaviour
{

    public float damage;

    public GameObject expPlacer;
    public GameObject expPlacer2;
    public GameObject debuff;
    public GameObject hpPickup;

    public GameObject poke;
    public GameObject burns;
    PlayerStats theStats;
    PlayerEquipment heroEquipment;
    //ParticleSystem particles;

    GameObject kaboom;
    GameObject kaboom2;
    GameObject player;

    float timer;
    bool once;

    bool once2;

    ParticleSystem particles;

    public AudioClip boltSound;

    bool shootAgain;
    void Start()
    {
        transform.Rotate(270, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        theStats = player.GetComponent<PlayerStats>();
        heroEquipment = player.GetComponent<PlayerEquipment>();
        particles = gameObject.GetComponent<ParticleSystem>();
        kaboom = (GameObject)Instantiate(expPlacer, transform.position, transform.rotation);
        kaboom.GetComponent<ExpPlacer>().returnsLight = false;
        timer = 0;
        once = true;
        shootAgain = true;
        once2 = true;
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            timer += Time.deltaTime;
            if (timer >= 0)
            {
                if (shootAgain == false)
                {
                    if (once2)
                    {
                        kaboom2 = (GameObject)Instantiate(expPlacer2, transform.position, transform.rotation);
                        kaboom2.GetComponent<ExpPlacer>().returnsLight = true;

                        particles.enableEmission = true;
                        once2 = false;
                    }
                }
                transform.localScale = transform.localScale + new Vector3(0, 0, Time.deltaTime * 20.0f);
                transform.position = player.transform.position;
                if (timer >= 0.15f)
                {
                    Explode();
                }
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        theStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        if (other.tag == "Wall")
        {
            Explode();
        }
        else if (other.tag == "Enemy")
        {
            if (once)
            {
                Instantiate(hpPickup, other.transform.position, other.transform.rotation);
                once = false;
            }
            Instantiate(burns, new Vector3(other.transform.position.x, other.transform.position.y, -0.5f), new Quaternion(0, 0, 0, 0));
            if (heroEquipment.equippedEmber == ember.None)
            {
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
                Instantiate(poke, other.transform.position, other.transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
                Instantiate(poke, other.transform.position, other.transform.rotation);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFire>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
                Instantiate(poke, other.transform.position, other.transform.rotation);
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                Camera.main.SendMessage("ScreenShake");
                other.GetComponent<Health>().LoseHealth(damage + 3 + theStats.spellModifier);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(poke, other.transform.position, other.transform.rotation);
                other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
            }
        }
    }

    void Explode()
    {
        if (shootAgain)
        {
            kaboom.SendMessage("Detonate", SendMessageOptions.DontRequireReceiver);
            transform.localScale = new Vector3(0, 0, 0);

            particles.startLifetime = timer;
            ParticleSystem.Particle[] firedParticles = new ParticleSystem.Particle[particles.particleCount];
            particles.GetParticles(firedParticles);

            for (int i = 0; i < firedParticles.Length; i++)
            {
                var pooy = firedParticles[i];
                pooy.color = Color.black;
                firedParticles[i] = pooy;
            }

            particles.SetParticles(firedParticles, firedParticles.Length);

            particles.enableEmission = false;


            timer = -0.2f;
            shootAgain = false;

            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(boltSound);


        }
        else
        {
            if (once2 == false)
            {


                kaboom2.SendMessage("Detonate", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
    }
}
