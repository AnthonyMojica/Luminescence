using UnityEngine;
using System.Collections;

public class PlayerDashing : MonoBehaviour
{

    public float dashSpeed;
    public float dashDuration;
    public float earthtrailtime;

    float timeRemaining;

    CharacterController controller;
    Vector2 MoveDirect;
    PlayerEquipment heroEquipment;
    PlayerLight heroLight;

    PlayerCooldowns heroCooldowns;
    float trailBlazerDropTimer;

    Animator anim;

    public GameObject lightRemains;

    [Header("Trailblazer")]
    public GameObject FireTrail;
    public GameObject LightTrail;
    public GameObject WindTrail;
    public GameObject IceTrail;
    public GameObject LifeTrail;
    public GameObject EarthTrail;
    public GameObject DeathTrail;

    [Header("Decoy")]

    public GameObject lightDecoy;
    public GameObject fireDecoy;
    public GameObject iceDecoy;
    public GameObject windDecoy;
    public GameObject earthDecoy;
    public GameObject lifeDecoy;
    public GameObject deathDecoy;

    [Header("Whirlwind")]

    public GameObject lightWhirl;
    public GameObject fireWhirl;
    public GameObject iceWhirl;
    public GameObject windWhirl;
    public GameObject earthWhirl;
    public GameObject lifeWhirl;
    public GameObject deathWhirl;


    [Header("Charge")]

    public GameObject FireCharge;
    public GameObject LightCharge;
    public GameObject WindCharge;
    public GameObject IceCharge;
    public GameObject LifeCharge;
    public GameObject EarthCharge;
    public GameObject DeathCharge;

    [Header("Blink")]
    public GameObject FireBlink;
    public GameObject LightBlink;
    public GameObject WindBlink;
    public GameObject IceBlink;
    public GameObject LifeBlink;
    public GameObject EarthBlink;
    public GameObject DeathBlink;

    [Header("Audio")]

    public AudioClip charge;
    public AudioClip whirl;
    public AudioClip decoy;
    public AudioClip blink;
    public AudioClip trail;

    public HUDCooldowns UICD;


    AudioSource aPlayer;

    Vector3 oldPos;

    Quaternion lockRot;

    bool once;

    Options theoptions;

    PlayerMovement move;

    void Start()
    {
        earthtrailtime = 0;
        anim = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        heroEquipment = gameObject.GetComponent<PlayerEquipment>();
        heroLight = gameObject.GetComponent<PlayerLight>();
        heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
        trailBlazerDropTimer = 0.0f;
        UICD = GameObject.Find("Health bar").GetComponent<HUDCooldowns>();
        aPlayer = gameObject.GetComponent<AudioSource>();
        once = false;
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();
        move = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (heroEquipment.paused == false && !move.transitioning)
        {

            //If the player is dashing, perform the dash
            if (timeRemaining > 0)
            {
                if (timeRemaining < 0.08f)
                {
                    if (once && (heroEquipment.equippedBoot == boot.Charge || heroEquipment.equippedBoot == boot.Blink || heroEquipment.equippedBoot == boot.Whirlwind))
                    {
                        Instantiate(lightRemains, oldPos, new Quaternion(0, 0, 0, 0));
                        once = false;
                    }
                }
                timeRemaining -= Time.deltaTime;

                if (heroEquipment.equippedBoot != boot.Blink && heroEquipment.equippedBoot != boot.Decoy)
                {
                    //Set the dash direction to the direction the player is currently facing

                    //Check if the player is touching the joysticks
                    MoveDirect.x = Input.GetAxis("CLSHorizontal");
                    MoveDirect.y = Input.GetAxis("CLSVertical");

                    //If the player wasn't touching the joysticks, check for WASD input
                    if (MoveDirect.x == 0.0f && MoveDirect.y == 0.0f)
                    {
                        MoveDirect.x = Input.GetAxis("KBHorizontal");
                        MoveDirect.y = Input.GetAxis("KBVertical");
                    }


                    //If the player wasn't using the joysticks OR the WASD keys, dash in the direction they are facing
                    if (MoveDirect.x == 0.0f && MoveDirect.y == 0.0f)
                    {
                        transform.rotation = lockRot;
                        MoveDirect = transform.TransformDirection(Vector3.up);

                        //Factor in speed and time
                        //Increase the dashspeed to make up for the player not moving on their own during the dash
                        MoveDirect *= (dashSpeed + 3.1f) * Time.deltaTime;
                    }
                    else
                    {
                        //Factor in speed and time
                        MoveDirect *= dashSpeed * Time.deltaTime;
                    }


                    controller.Move(MoveDirect);


                    if (heroEquipment.equippedBoot == boot.Trailblazer)
                    {
                        trailBlazerDropTimer += Time.deltaTime;
                        if (trailBlazerDropTimer > 0.03f)
                        {
                            //No ember equipped
                            if (heroEquipment.equippedEmber == ember.None)
                            {
                                Instantiate(LightTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Ice ember equipped
                            else if (heroEquipment.equippedEmber == ember.Ice)
                            {
                                Instantiate(IceTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Fire ember equipped
                            else if (heroEquipment.equippedEmber == ember.Fire)
                            {
                                Instantiate(FireTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Wind ember equipped
                            else if (heroEquipment.equippedEmber == ember.Wind)
                            {
                                Instantiate(WindTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Earth ember equipped
                            else if (heroEquipment.equippedEmber == ember.Earth)
                            {
                                Instantiate(EarthTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Life ember equipped
                            else if (heroEquipment.equippedEmber == ember.Life)
                            {
                                Instantiate(LifeTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            //Death ember equipped
                            else if (heroEquipment.equippedEmber == ember.Death)
                            {
                                Instantiate(DeathTrail, transform.position, new Quaternion(0, 0, 0, 0));
                            }
                            else if (heroEquipment.equippedEmber == ember.Earth)
                            {
                                earthtrailtime += Time.deltaTime;

                                if (earthtrailtime >= 3.0f)
                                {
                                    Instantiate(EarthTrail, transform.position, new Quaternion(0, 0, 0, 0));
                                    earthtrailtime = 0.0f;
                                }
                            }
                            trailBlazerDropTimer = 0.0f;

                        }
                    }
                }
            }
        }
    }
    void Dash()
    {
        //If the player isnt already dashing, begin dashing
        if (!heroCooldowns.dashCooling)
        {
            if (heroLight.currentLight < 10)
            {
                return;
            }

            theoptions.AddToDash();

            //Make the player spend light
            heroLight.LoseLight(10);
            heroEquipment.EmberLoseDurability();
            heroCooldowns.dashCooling = true;
            timeRemaining = dashDuration;
            if (heroEquipment.equippedBoot != boot.Whirlwind && heroEquipment.equippedBoot!=boot.Decoy)
            {
                //Change the animation state into dashing
                anim.CrossFade("PlayerDashing", 0.01f);
            }

            //Set up the local variables
            trailBlazerDropTimer = 0.1f;

            lockRot = transform.rotation;

            if (heroEquipment.equippedBoot == boot.Trailblazer)
            {
                aPlayer.PlayOneShot(trail);

            }

            if (heroEquipment.equippedBoot == boot.Charge)
            {
                aPlayer.PlayOneShot(charge);
                oldPos = transform.position;
                once = true;
                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(LightCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(IceCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(FireCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(WindCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    Instantiate(LifeCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    Instantiate(DeathCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Instantiate(EarthCharge, transform.position, new Quaternion(0, 0, 0, 0));
                }
            }

            if (heroEquipment.equippedBoot == boot.Blink)
            {
                aPlayer.PlayOneShot(blink);
                oldPos = transform.position;
                once = true;
                Vector3 temp = (transform.TransformDirection(Vector3.up.normalized) * 3.0f) + transform.position;

                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(LightBlink, temp, new Quaternion(0, 0, 0, 0));

                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(IceBlink, temp, new Quaternion(0, 0, 0, 0));

                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(FireBlink, temp, new Quaternion(0, 0, 0, 0));
                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(WindBlink, temp, new Quaternion(0, 0, 0, 0));

                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    Instantiate(LifeBlink, temp, new Quaternion(0, 0, 0, 0));

                }
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    Instantiate(DeathBlink, temp, new Quaternion(0, 0, 0, 0));
                }
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Instantiate(EarthBlink, temp, new Quaternion(0, 0, 0, 0));
                }




            }
            if (heroEquipment.equippedBoot == boot.Whirlwind)
            {
                aPlayer.PlayOneShot(whirl);
                oldPos = transform.position;
                once = true;
                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightWhirl, transform.position, transform.rotation);

                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(iceWhirl, transform.position, transform.rotation);

                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(fireWhirl, transform.position, transform.rotation);

                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(windWhirl, transform.position, transform.rotation);

                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    Instantiate(lifeWhirl, transform.position, transform.rotation);

                }
                //Death ember equipped
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    Instantiate(deathWhirl, transform.position, transform.rotation);

                }
                //Earth ember equipped
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Instantiate(earthWhirl, transform.position, transform.rotation);

                }


            }
            if (heroEquipment.equippedBoot == boot.Decoy)
            {

                aPlayer.PlayOneShot(decoy);
                oldPos = transform.position;

                Vector3 temp = (transform.TransformDirection(Vector3.up.normalized));

                controller.Move(temp);

                //No ember equipped
                if (heroEquipment.equippedEmber == ember.None)
                {
                    Instantiate(lightDecoy, oldPos, transform.rotation);

                }
                //Ice ember equipped
                else if (heroEquipment.equippedEmber == ember.Ice)
                {
                    Instantiate(iceDecoy, oldPos, transform.rotation);
                }
                //Fire ember equipped
                else if (heroEquipment.equippedEmber == ember.Fire)
                {
                    Instantiate(fireDecoy, oldPos, transform.rotation);
                }
                //Wind ember equipped
                else if (heroEquipment.equippedEmber == ember.Wind)
                {
                    Instantiate(windDecoy, oldPos, transform.rotation);
                }
                //Life ember equipped
                else if (heroEquipment.equippedEmber == ember.Life)
                {
                    Instantiate(lifeDecoy, oldPos, transform.rotation);
                }
                //Death ember equipped
                else if (heroEquipment.equippedEmber == ember.Death)
                {
                    Instantiate(deathDecoy, oldPos, transform.rotation);
                }
                //Earth ember equipped
                else if (heroEquipment.equippedEmber == ember.Earth)
                {
                    Instantiate(earthDecoy, oldPos, transform.rotation);
                }

            }
        }
        else
        {
            UICD.FlashCooldown(1);
        }
    }

}
