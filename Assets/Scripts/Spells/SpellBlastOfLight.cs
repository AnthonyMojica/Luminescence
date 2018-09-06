using UnityEngine;
using System.Collections;

public class SpellBlastOfLight : MonoBehaviour
{


    Light theLight;
    PlayerEquipment heroEquipment;
    float timeAlive;

    public GameObject debuff;
    public GameObject remains;
    public GameObject hpPickup;
    GameObject player;
    public GameObject burns;
    public float dmg;

    float maxLife;
    bool once;
    PlayerStats theStats;

    // Use this for initialization
    void Start()
    {
        dmg = 10;
        player = GameObject.FindGameObjectWithTag("Player");
        timeAlive = 0;
        theLight = gameObject.GetComponent<Light>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        theStats = player.GetComponent<PlayerStats>();
        Instantiate(burns, new Vector3(transform.position.x, transform.position.y, -0.5f), new Quaternion(0, 0, 0, 0));


        if (heroEquipment.equippedEmber == ember.Ice)
        {
            maxLife = 1.5f;
        }
        else
        {
            maxLife = 1.0f;
        }
        once = true;

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, Enemies[i].transform.position) < 2.6f)
            {
                if (Enemies[i].tag == "Enemy")
                {
                    Instantiate(burns, new Vector3(Enemies[i].transform.position.x, Enemies[i].transform.position.y, -0.5f), new Quaternion(0, 0, 0, 0));

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
                        //Handles itself
                    }
                    else if (heroEquipment.equippedEmber == ember.Life)
                    {
                        //Handles itself
                    }
                    else if (heroEquipment.equippedEmber == ember.Death)
                    {
                        Enemies[i].SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                    }
                    else if (heroEquipment.equippedEmber == ember.Earth)
                    {
                        Enemies[i].GetComponent<Health>().LoseHealth(3);
                    }

                    if (once)
                    {
                        Instantiate(hpPickup, Enemies[i].transform.position, Enemies[i].transform.rotation);
                        once = false;
                    }
                    Enemies[i].SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                    Enemies[i].GetComponent<Health>().LoseHealth(dmg+theStats.spellModifier);



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
                Instantiate(remains, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {


    //        if (heroEquipment.equippedEmber == ember.None)
    //        {

    //        }
    //        else if (heroEquipment.equippedEmber == ember.Fire)
    //        {

    //            GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
    //            tempObj.GetComponent<DebuffFire>().target = other.gameObject;
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Ice)
    //        {

    //            GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
    //            tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Wind)
    //        {
    //            //Handles itself
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Life)
    //        {
    //            //Handles itself
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Death)
    //        {
    //            other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
    //        }
    //        else if (heroEquipment.equippedEmber == ember.Earth)
    //        {
    //            other.GetComponent<Health>().LoseHealth(3);
    //        }

    //        if (once)
    //        {
    //            Instantiate(hpPickup, other.transform.position, other.transform.rotation);
    //            once = false;
    //        }
    //        other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
    //        other.GetComponent<Health>().LoseHealth(10);
    //    }

    //}
}

