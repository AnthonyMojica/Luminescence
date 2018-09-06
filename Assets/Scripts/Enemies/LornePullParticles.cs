using UnityEngine;
using System.Collections;

public class LornePullParticles : MonoBehaviour {

    GameObject Lorne;
    ParticleSystem theParts;
    Vector2 waypoint;
    float distanceToShadow;
    bool doOnce;
    //public AudioSource playSounds;
   // public AudioClip GetHit;
    PlayerEquipment heroEquipment;


    public GameObject theExplosion;
    // Use this for initialization
    void Start()
    {
        //playSounds = gameObject.GetComponent<AudioSource>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Lorne = GameObject.FindGameObjectWithTag("Invincible");
        theParts = gameObject.GetComponent<ParticleSystem>();
        distanceToShadow = 1;
        doOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            if (Lorne != null)
            {
                distanceToShadow = Vector3.Distance(transform.position, Lorne.transform.position);
                waypoint = Lorne.transform.position - gameObject.transform.position;
                waypoint.Normalize();
                waypoint *= 8 * Time.deltaTime;
                transform.position = new Vector3(waypoint.x + transform.position.x, waypoint.y + transform.position.y, -1);
            }
            else
                Destroy(gameObject);
            if (distanceToShadow < .1f && doOnce)
            {
              //  playSounds.PlayOneShot(GetHit);
                doOnce = false;
                Instantiate(theExplosion, Lorne.transform.position, Lorne.transform.rotation);
                theParts.emissionRate = 0;
                Destroy(gameObject, 2);
            }
        }

    }
}
