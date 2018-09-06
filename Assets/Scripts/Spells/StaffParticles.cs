using UnityEngine;
using System.Collections;

public class StaffParticles : MonoBehaviour {

    PlayerEquipment playerEquipemnt;
    float checkTimer;


    public GameObject fireParts;
    public GameObject iceParts;
    public GameObject lightParts;
    public GameObject windParts;
    public GameObject lifeParts;
    public GameObject deathParts;
    public GameObject earthParts;


	// Use this for initialization
	void Start () {
        playerEquipemnt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        checkTimer = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= 1.0f)
        {
            if (playerEquipemnt.equippedEmber == ember.None)
            {
                lightParts.SetActive(true);
                fireParts.SetActive(false);
                iceParts.SetActive(false);
                windParts.SetActive(false);
                lifeParts.SetActive(false);
                deathParts.SetActive(false);
                earthParts.SetActive(false);
            }
            else if (playerEquipemnt.equippedEmber == ember.Fire)
            {
                lightParts.SetActive(false);
                fireParts.SetActive(true);
                iceParts.SetActive(false);
                windParts.SetActive(false);
                lifeParts.SetActive(false);
                deathParts.SetActive(false);
                earthParts.SetActive(false);
            }
            else if (playerEquipemnt.equippedEmber == ember.Ice)
            {
                lightParts.SetActive(false);
                fireParts.SetActive(false);
                iceParts.SetActive(true);
                windParts.SetActive(false);
                lifeParts.SetActive(false);
                deathParts.SetActive(false);
                earthParts.SetActive(false);
            }
            else if (playerEquipemnt.equippedEmber == ember.Wind)
            {
                lightParts.SetActive(false);
                fireParts.SetActive(false);
                iceParts.SetActive(false);
                windParts.SetActive(true);
                lifeParts.SetActive(false);
                deathParts.SetActive(false);
                earthParts.SetActive(false);
            }
            else if (playerEquipemnt.equippedEmber == ember.Life)
            {
                lightParts.SetActive(false);
                fireParts.SetActive(false);
                iceParts.SetActive(false);
                windParts.SetActive(false);
                lifeParts.SetActive(true);
                deathParts.SetActive(false);
                earthParts.SetActive(false);
            }
            else if (playerEquipemnt.equippedEmber == ember.Death)
            {

                lightParts.SetActive(false);
                fireParts.SetActive(false);
                iceParts.SetActive(false);
                windParts.SetActive(false);
                lifeParts.SetActive(false);
                deathParts.SetActive(true);
                earthParts.SetActive(false);
            
            }
            else if (playerEquipemnt.equippedEmber == ember.Earth)
            {

                lightParts.SetActive(false);
                fireParts.SetActive(false);
                iceParts.SetActive(false);
                windParts.SetActive(false);
                lifeParts.SetActive(false);
                deathParts.SetActive(false);
                earthParts.SetActive(true);

            }
            checkTimer = 0;
        }
	}
}
