using UnityEngine;
using System.Collections;

public class ConsumeLight : MonoBehaviour {

    GameObject shadow;
    ParticleSystem theParts;
    Vector2 waypoint;
    float distanceToShadow;
    bool doOnce;
    public AudioSource playSounds;
    public AudioClip GetHit;
    PlayerEquipment heroEquipment;
    

    public GameObject theExplosion;
	// Use this for initialization
	void Start () 
    {
        playSounds = gameObject.GetComponent<AudioSource>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < Enemies.Length; i++ )
        {
            if (Enemies[i].name == "ShadowSpawn" || Enemies[i].name == "ShadowSpawn(Clone)")
            {   
                shadow = Enemies[i];
                break;
            }
        }
        theParts = gameObject.GetComponent<ParticleSystem>();
        distanceToShadow = 1;
        doOnce = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (heroEquipment.paused == false)
        {


            if (shadow != null)
            {
                distanceToShadow = Vector3.Distance(transform.position, shadow.transform.position);
                waypoint = shadow.transform.position - gameObject.transform.position;
                waypoint.Normalize();
                waypoint *= 4 * Time.deltaTime;
                transform.position = new Vector3(waypoint.x + transform.position.x, waypoint.y + transform.position.y, -1);
            }
            else
                Destroy(gameObject);
            if (distanceToShadow < .1f && doOnce)
            {
                playSounds.PlayOneShot(GetHit);
                doOnce = false;
                Instantiate(theExplosion, shadow.transform.position, shadow.transform.rotation);
                theParts.emissionRate = 0;
                Destroy(gameObject, 2);
            }
        }

	}
}
