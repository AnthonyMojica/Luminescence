using UnityEngine;
using System.Collections;

public class WhirlSpin : MonoBehaviour {

    PlayerEquipment heroEquipment;

    GameObject player;
    ParticleSystem particles;
    float timer;
    bool once;

    public GameObject debuff;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        timer = 0;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        particles = gameObject.GetComponent<ParticleSystem>();
        once = true;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;
        transform.Rotate(new Vector3(0, 0, -600 * Time.deltaTime));

        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            if (once)
            {


                player.GetComponent<Animator>().enabled = true;
                player.GetComponent<SpriteRenderer>().enabled = true;
                Destroy(gameObject, 2);
                particles.emissionRate = 0;

                once = false;
            }
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (heroEquipment.equippedEmber == ember.None)
            {
            }
            else if (heroEquipment.equippedEmber == ember.Fire)
            {
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFire>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Ice)
            {
                GameObject tempObj = (GameObject)Instantiate(debuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
            }
            else if (heroEquipment.equippedEmber == ember.Wind)
            {
            }

            else if (heroEquipment.equippedEmber == ember.Earth)
            {
                other.GetComponent<Health>().LoseHealth(0.6f);
            }
            else if (heroEquipment.equippedEmber == ember.Death)
            {
                other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
            }
            else if (heroEquipment.equippedEmber == ember.Life)
            {
            }

            other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
            other.GetComponent<Health>().LoseHealth(1);
        }
    }
}
