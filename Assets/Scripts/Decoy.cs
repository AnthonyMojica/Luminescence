using UnityEngine;
using System.Collections;

public class Decoy : MonoBehaviour
{


    public GameObject lightRemains;
    float timer;
    public GameObject explosion;
    GameObject player;
    PlayerStats theStats;
    PlayerEquipment equip;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        theStats = player.GetComponent<PlayerStats>();
        equip = player.GetComponent<PlayerEquipment>();
        timer = 0;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            obj.SendMessage("Decoy", SendMessageOptions.DontRequireReceiver);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!equip.paused)
        {
            timer += Time.deltaTime;
            if (timer >= 3.0f)
            {
                GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects)
                {
                    obj.SendMessage("UnDecoy", SendMessageOptions.DontRequireReceiver);

                    //if (obj.tag == "Enemy")
                    //{
                    //    if (Vector3.Distance(transform.position, obj.transform.position) < 5.0f)
                    //    {
                    //        obj.GetComponent<Health>().LoseHealth(damage + theStats.spellModifier);
                    //        //obj.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                    //    }
                    //}
                }

                Explode();

            }
        }

    }
    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(lightRemains, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
