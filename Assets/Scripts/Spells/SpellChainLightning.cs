using UnityEngine;
using System.Collections;

public class SpellChainLightning : MonoBehaviour
{
    public float damage;

    public GameObject debuff;
    public GameObject hpPickup;
    public GameObject lightPickup;

    public GameObject theChains;

    int enemiesLeft;

    DisplayEnmiesRemaining ugh;

    PlayerEquipment heroEquipment;
    //ParticleSystem particles;
    PlayerStats theStats;
    GameObject kaboom;
    GameObject player;

    GameObject target;

    ParticleSystem particles;

    PlayerSpellCasting pSpells;

    public GameObject cantHit;

    bool once;

    bool dood;

    public GameObject burns;

    float timer;

    void Start()
    {
        theStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        transform.Rotate(270, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        particles = gameObject.GetComponent<ParticleSystem>();
        target = null;
        pSpells = player.GetComponent<PlayerSpellCasting>();
        timer = 0;
        ugh = GameObject.Find("EnemiesRemaining").GetComponent<DisplayEnmiesRemaining>();
        once = true;
        cantHit = null;
        dood = true;

        //Attempt to fix fuzzy balls of lightning
        Destroy(gameObject, 1);
    }

    void FixedUpdate()
    {
        if (heroEquipment.paused == false)
        {
            //Keep firing from the player
            // transform.position = player.transform.position;

            if (target == null)
            {
                transform.localScale = transform.localScale + new Vector3(0, 0, Time.deltaTime * 20.0f);
                timer += Time.deltaTime;
            }
            else
            {
                if (once)
                {
                    //This code makes the particles that were already fired invisible
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

                    timer = -.2f;
                    once = false;
                }
                timer += Time.deltaTime;

            }
            if (timer >= 0.15f)
            {
                Explode();
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        pSpells = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpellCasting>();
        theStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        if (target == null)
        {
            if (other.gameObject == cantHit)
            {
                return;
            }
            if (other.tag == "Wall")
            {
                Explode();
            }
            else if (other.tag == "Enemy")
            {
                Instantiate(burns, new Vector3(other.transform.position.x, other.transform.position.y, -0.5f), new Quaternion(0, 0, 0, 0));
                target = other.gameObject;
                if (heroEquipment.equippedEmber == ember.None)
                {
                    other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                }
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                    GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                    tempObj.GetComponent<DebuffFire>().target = other.gameObject;
                }
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                    GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                    tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
                }
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                    other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                }
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                }
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Camera.main.SendMessage("ScreenShake");
                    other.GetComponent<Health>().LoseHealth(damage + 3+theStats.spellModifier);
                }
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                    other.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
                }


                if (pSpells.chained == false)
                {
                    if (ugh.count > 0)
                    {
                        GameObject[] possibleDoodsToShoot = GameObject.FindGameObjectsWithTag("Enemy");
                        for (int i = 0; i < possibleDoodsToShoot.Length; i++)
                        {
                            if (Vector3.Distance(other.transform.position, possibleDoodsToShoot[i].transform.position) < 3)
                            {
                                Vector3 temp = possibleDoodsToShoot[i].transform.position - other.transform.position;

                                float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg + 270;

                                Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

                                GameObject theNewChains = (GameObject)Instantiate(theChains, other.transform.position, rot);
                                theNewChains.GetComponent<SpellChainLightning>().cantHit = other.gameObject;
                                pSpells.chained = true;
                            }
                        }
                    }
                }
                else
                {
                    if (dood)
                    {
                        Instantiate(hpPickup, other.transform.position, other.transform.rotation);
                        Instantiate(lightPickup, other.transform.position, new Quaternion(0, 0, 0, 0));
                        dood = false;
                    }

                }
            }


        }
    }

    void Explode()
    {
        pSpells = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpellCasting>();

        if (target == null && pSpells.chained == false)
        {
            Instantiate(lightPickup, transform.position, new Quaternion(0, 0, 0, 0));

        }
        Destroy(gameObject);
    }
}
