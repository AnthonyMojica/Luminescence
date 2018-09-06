using UnityEngine;
using System.Collections;

public class PlayerHazard : MonoBehaviour
{

    public bool isInHazard;
    bool isInCloud;

    public Texture HazardCookie;
    Light heroLight;
    GameObject hero;
    Health heroHP;
    PlayerMovement heroMovement;

    PlayerEquipment heroEqp;

    public AudioSource aPlayer;



    float timer;

    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        heroMovement = hero.GetComponent<PlayerMovement>();
        heroHP = hero.GetComponent<Health>();
        heroEqp = hero.GetComponent<PlayerEquipment>();
        heroLight = hero.GetComponentInChildren<Light>();
        timer = 0.5f;

        isInCloud = false;
    }

    void Update()
    {
        if (heroEqp.paused == false)
        {


            if (isInHazard)
            {
                heroLight.cookie = HazardCookie;
                heroMovement.isinHazard = true;
                heroMovement.halfSpeed = 0.8f;
                heroMovement.fullSpeed = 1.6f;
                heroHP.LoseHealth(3.0f * Time.deltaTime);
                if (aPlayer.isPlaying == false)
                {
                    aPlayer.Play();
                }

            }
            else
            {
                heroLight.cookie = null;
                heroMovement.isinHazard = false;
                heroMovement.halfSpeed = 1.6f;
                heroMovement.fullSpeed = 3.1f;
                if (aPlayer && aPlayer.isPlaying == true)
                {
                    aPlayer.Stop();
                    
                }
            }

            if (isInCloud)
            {
                heroLight.cookie = HazardCookie;
                heroHP.LoseHealth(3.0f * Time.deltaTime);
            }
            else if (isInHazard == false)
            {
                heroLight.cookie = null;
            }

            timer += Time.deltaTime;

            if (timer >= 0.05f)
            {
                isInHazard = false;
                isInCloud = false;

                GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
                for (int i = 0; i < hazards.Length; i++)
                {
                    if (Vector3.Distance(transform.position, hazards[i].transform.position) < 0.8f)
                    {
                        isInHazard = true;
                        break;
                    }
                }

                if (isInHazard == false)
                {
                    GameObject[] reapHazards = GameObject.FindGameObjectsWithTag("ReapHazard");
                    for (int i = 0; i < reapHazards.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, reapHazards[i].transform.position) < 0.95f)
                        {
                            if (reapHazards[i].GetComponent<ReappearingHazard>().doingDamage == true)
                            {
                                isInHazard = true;
                                break;
                            }
                        }
                    }

                }

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (Vector3.Distance(transform.position, enemies[i].transform.position) < 0.95f)
                    {
                        if (enemies[i].name.Contains("Cloud"))
                        {
                            isInCloud = true;
                            break;
                        }

                    }
                }

                timer = 0;
            }
        }
    }

}
