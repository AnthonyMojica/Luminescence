using UnityEngine;
using System.Collections;

public class OrbExplosion : MonoBehaviour
{
    Light theLight;
    PlayerEquipment heroEquipment;
    float timeAlive;
    public GameObject hpPickup;

    public GameObject debuff;

    GameObject player;

    float maxLife;

    bool once;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();
        Camera.main.SendMessage("ScreenShake");
        if (heroEquipment.equippedEmber == ember.Ice)
        {
            maxLife = 1.1f;
        }
        else
        {
            maxLife = 0.6f;
        }
        once = true;

        float expRadius = 1.3f;

        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();


        if (heroEquipment.equippedAccessory == accessory.OrbOfLight)
        {
            expRadius = 1.3f;
        }
        else if (heroEquipment.equippedAccessory == accessory.Snare)
        {
            expRadius = 2.5f;
        }



        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, Enemies[i].transform.position) < expRadius)
            {
                if (Enemies[i].tag == "Enemy")
                {

                    if (heroEquipment.equippedEmber == ember.None)
                    {
                    }
                    else if (heroEquipment.equippedEmber == ember.Fire)
                    {
                        GameObject tempObj = (GameObject)Instantiate(debuff, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        tempObj.GetComponent<DebuffFire>().target = Enemies[i].gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Ice)
                    {
                        GameObject tempObj = (GameObject)Instantiate(debuff, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        tempObj.GetComponent<DebuffFrost>().target = Enemies[i].gameObject;
                    }
                    else if (heroEquipment.equippedEmber == ember.Wind)
                    {
                        Enemies[i].SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                    }

                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Enemies[i].GetComponent<Health>().LoseHealth(3);
                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Enemies[i].SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                    }

                    if (once)
                    {
                        Instantiate(hpPickup, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        once = false;
                    }
                    Enemies[i].GetComponent<Health>().LoseHealth(5);



                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            timeAlive += Time.deltaTime;
            theLight.range -= Time.deltaTime * 4;
            if (timeAlive >= maxLife)
            {

                if (heroEquipment.equippedAccessory == accessory.Snare)
                {
                    GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    for (int i = 0; i < Enemies.Length; i++)
                    {
                        Enemies[i].SendMessage("UnSnare", SendMessageOptions.DontRequireReceiver);
                    }
                }

                Destroy(gameObject);
            }
        }
    }

}














































