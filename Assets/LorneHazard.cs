using UnityEngine;
using System.Collections;

public class LorneHazard : MonoBehaviour
{

    public float DamagePerSecond;
    public Texture HazardCookie;

    GameObject hero;
    Health heroHP;
    Light heroLight;
    PlayerMovement heroMovement;
    PlayerDashing heroDash;
    GameObject tocheck;
    AudioSource audioPlayer;
    public float timer = 5.0f;
    public float countertodestroythefriggingbug = 0;
    public GameObject[] floors;
    bool active = false;
    public GameObject Safety;
    int[] Safezones;
    public float timertosafety = 4.0f;

    void Start()
    {
        this.gameObject.SetActive(false);
        tocheck = this.gameObject;
        hero = GameObject.FindGameObjectWithTag("Player");
        heroHP = hero.GetComponent<Health>();
        heroLight = hero.GetComponentInChildren<Light>();
        heroMovement = hero.GetComponent<PlayerMovement>();
        heroDash = hero.GetComponent<PlayerDashing>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
        Safezones= new int[6];

       floors = GameObject.FindGameObjectsWithTag("Floor");
       if (floors != null)
       { }
       //{
       //    int tempforchoosingfloors = 0;
       //    for (; tempforchoosingfloors < 6; )
       //    {
       //        int temp = Random.Range(0, floors.Length);
       //        bool tempbool = true;
       //        for (int i = 0; i < Safezones.Length; i++)
       //        {
       //            if (Safezones[i] == temp)
       //                tempbool = false;

       //        }
       //        if (tempbool)
       //        {
       //            Safezones[tempforchoosingfloors] = temp;
       //            ++tempforchoosingfloors;
       //            Instantiate(Safety, floors[temp].gameObject.transform.position, floors[temp].gameObject.transform.rotation);

       //        }


       //    }
       //}
 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hero)
        {
            heroMovement.isinHazard = true;
            // Slow the player
            heroMovement.halfSpeed = 0.8f;
            heroMovement.fullSpeed = 1.6f;
            //heroDash.dashSpeed = 2;
            //Dont slow the player anymore
            heroLight.cookie = HazardCookie;

            //Play the hazard sound attached to the player
            ++countertodestroythefriggingbug;
            audioPlayer.Play();


            // other.GetComponentInChildren<AudioSource>().enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == hero && heroMovement.isinHazard == true)
        {
            if (countertodestroythefriggingbug != 0)
                heroHP.LoseHealth(DamagePerSecond * Time.deltaTime / countertodestroythefriggingbug);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == hero)
        {
            --countertodestroythefriggingbug;
            if (countertodestroythefriggingbug == 0)
            {
                heroMovement.isinHazard = false;
                // Return the players speeds to normal
                heroMovement.halfSpeed = 1.6f;
                heroMovement.fullSpeed = 3.1f;
                //heroDash.dashSpeed = 4;
                heroLight.cookie = null;

                //Stop playing audio
                audioPlayer.Stop();
            }


        }
    }

    void Update()
    {
        if (active)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                Destroy(this.gameObject);
        }
        else
        {
            timertosafety -= Time.deltaTime;
            if(timertosafety<=0)
            {
                this.gameObject.SetActive(true);
                active = true;
            }
        }
    }
}
