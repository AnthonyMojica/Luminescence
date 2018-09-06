using UnityEngine;
using System.Collections;

public class PlayerSpellCasting : MonoBehaviour
{
    [Header("Orb of Light")]
    public GameObject lightOrb;
    public GameObject fireOrb;
    public GameObject frostOrb;
    public GameObject windOrb;
    public GameObject lifeOrb;
    public GameObject deathOrb;
    public GameObject earthOrb;
    public AudioClip orbAudio;

    [Header("Singularity")]
    public GameObject lightSing;
    public GameObject fireSing;
    public GameObject frostSing;
    public GameObject windSing;
    public GameObject lifeSing;
    public GameObject deathSing;
    public GameObject earthSing;

    [Header("Blast of Light")]
    public GameObject lightBlast;
    public GameObject fireBlast;
    public GameObject frostBlast;
    public GameObject windBlast;
    public GameObject lifeBlast;
    public GameObject deathBlast;
    public GameObject earthBlast;

    [Header("Bolt of Light")]
    public GameObject lightBolt;
    public GameObject fireBolt;
    public GameObject frostBolt;
    public GameObject windBolt;
    public GameObject lifeBolt;
    public GameObject deathBolt;
    public GameObject earthBolt;
    public AudioClip boltAudio;

    [Header("Light Mine")]
    public GameObject lightMine;
    public GameObject fireMine;
    public GameObject frostMine;
    public GameObject windMine;
    public GameObject lifeMine;
    public GameObject deathMine;
    public GameObject earthMine;

    [Header("Chain Lightning")]
    public GameObject lightChain;
    public GameObject fireChain;
    public GameObject frostChain;
    public GameObject windChain;
    public GameObject lifeChain;
    public GameObject deathChain;
    public GameObject earthChain;
    public bool chained;

    [Header("Ensnare")]
    public GameObject lightSnare;
    public GameObject fireSnare;
    public GameObject frostSnare;
    public GameObject windSnare;
    public GameObject lifeSnare;
    public GameObject deathSnare;
    public GameObject earthSnare;


    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;
    PlayerLight heroLight;
    Animator anim;
    public HUDCooldowns UICD;

    Light playersLight;

    bool shoot;
    public GameObject castParticles;
    float shootTimer;

    AudioSource audioPlayer;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        heroLight = gameObject.GetComponent<PlayerLight>();
        UICD = GameObject.Find("Health bar").GetComponent<HUDCooldowns>();
        playersLight = gameObject.GetComponentInChildren<Light>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
        chained = false;
        shoot = false;
        shootTimer = 0;
    }

    void Update()
    {
        if (shoot == true)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > 0.1f)
            {
                shootTimer = 0;
                shoot = false;
                FireTheSpell();
            }
        }
    }

    void FireTheSpell()
    {
        playersLight.intensity = 8;
        Camera.main.SendMessage("ScreenShake");
        //If the orb of light is equipped, fire the orb of light ability
        if (heroEquipment.equippedAccessory == accessory.OrbOfLight)
        {
            GameObject myOrb = null;

            Vector3 orbRot1 = transform.rotation.eulerAngles;
            orbRot1 = new Vector3(orbRot1.x, orbRot1.y, orbRot1.z - 20);
            Quaternion rot1 = transform.rotation;
            rot1.eulerAngles = orbRot1;

            Vector3 orbRot2 = transform.rotation.eulerAngles;
            orbRot1 = new Vector3(orbRot2.x, orbRot2.y, orbRot2.z + 20);
            Quaternion rot2 = transform.rotation;
            rot2.eulerAngles = orbRot1;

            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightOrb, transform.position, rot1);
                Instantiate(lightOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(lightOrb, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireOrb, transform.position, rot1);
                Instantiate(fireOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(fireOrb, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windOrb, transform.position, rot1);
                Instantiate(windOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(windOrb, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostOrb, transform.position, rot1);
                Instantiate(frostOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(frostOrb, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeOrb, transform.position, rot1);
                Instantiate(lifeOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(lifeOrb, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathOrb, transform.position, rot1);
                Instantiate(deathOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(deathOrb, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthOrb, transform.position, rot1);
                Instantiate(earthOrb, transform.position, rot2);
                myOrb = (GameObject)Instantiate(earthOrb, transform.position, transform.rotation);

            }
            if (myOrb != null)
            {
                myOrb.GetComponent<SpellOrbOfLight>().dropLight = true;
                
            }
        }
        else if (heroEquipment.equippedAccessory == accessory.Singularity)
        {
            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightSing, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireSing, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windSing, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostSing, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeSing, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathSing, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthSing, transform.position, transform.rotation);
            }
        }
        else if (heroEquipment.equippedAccessory == accessory.BlastOfLight)
        {
            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightBlast, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireBlast, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windBlast, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostBlast, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeBlast, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathBlast, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthBlast, transform.position, transform.rotation);

            }
        }
        else if (heroEquipment.equippedAccessory == accessory.BoltOfLight)
        {
            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightBolt, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireBolt, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windBolt, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostBolt, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeBolt, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathBolt, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthBolt, transform.position, transform.rotation);

            }
        }
        else if (heroEquipment.equippedAccessory == accessory.LightMine)
        {
            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightMine, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireMine, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windMine, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostMine, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeMine, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathMine, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthMine, transform.position, transform.rotation);

            }

        }

        else if (heroEquipment.equippedAccessory == accessory.Snare)
        {
            chained = false;
            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightSnare, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireSnare, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windSnare, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostSnare, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeSnare, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathSnare, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthSnare, transform.position, transform.rotation);

            }
        }
        else if (heroEquipment.equippedAccessory == accessory.ChainLightning)
        {
            chained = false;
            if (heroEquipment.equippedEmber == ember.None)
            {
                Instantiate(lightChain, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                Instantiate(fireChain, transform.position, transform.rotation);
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
                Instantiate(windChain, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                Instantiate(frostChain, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
                Instantiate(lifeChain, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                Instantiate(deathChain, transform.position, transform.rotation);

            }
            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                Instantiate(earthChain, transform.position, transform.rotation);

            }
        }
    }

    void CastSpell()
    {
        if (heroEquipment.paused == false)
        {
            //If the spell cast is not on cooldown
            if (!heroCooldowns.spellCooling)
            {
                if (heroLight.currentLight < 20)
                {
                    Camera.main.SendMessage("ScreenShake");
                    return;
                }
                heroEquipment.EmberLoseDurability();
                heroLight.LoseLight(20);
                playersLight.intensity = 5.5f;
                ///Instantiate(castParticles, transform.position, transform.rotation);
                heroCooldowns.spellCooling = true;
                anim.CrossFade("Spellcasting", 0.01f);
                shoot = true;

                if (heroEquipment.equippedAccessory == accessory.BoltOfLight)
                {
                    audioPlayer.PlayOneShot(boltAudio);

                }
                else if (heroEquipment.equippedAccessory == accessory.OrbOfLight)
                {
                    audioPlayer.PlayOneShot(orbAudio);
                    
                }

            }
            else
            {
                UICD.FlashCooldown(0);
            }
        }

    }
}
