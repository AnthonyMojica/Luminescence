using UnityEngine;
using System.Collections;

public class SpellSingularity : MonoBehaviour
{

    PlayerEquipment heroEquipment;

    float lifeTimer;

    // Update is called once per frame

    Light theLight;
    public GameObject theExplosion;
    //GameObject player;
    Vector3 dir;

    ParticleSystem particles;
    void Start()
    {
        lifeTimer = 0;
        theLight = gameObject.GetComponent<Light>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        dir = transform.up;
        particles = gameObject.GetComponent<ParticleSystem>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (heroEquipment.paused == false)
        {
            transform.position += dir * 1.5f * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 150f * Time.deltaTime));

            lifeTimer += Time.deltaTime;
            if (lifeTimer >= 1.5)
            {
                theLight.range -= Time.deltaTime * 2;
                particles.emissionRate = 0;
            }

            if (lifeTimer >= 2.1)
            {
                Instantiate(theExplosion, transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyid itsID = other.GetComponent<EnemyID>().theID;

            if (itsID == enemyid.shadowCloud)
            {
                other.GetComponent<AIShadowCloud>().target = gameObject;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyid itsID = other.GetComponent<EnemyID>().theID;

            if (itsID != enemyid.shadowCloud)
            {
                Vector2 moveTo = (transform.position - other.transform.position).normalized;
                CharacterController theController = other.GetComponent<CharacterController>();

                theController.Move(moveTo * Time.deltaTime * 2);
            }
        }
    }
}
