using UnityEngine;
using System.Collections;

public class DebuffFire : MonoBehaviour
{

    public float dps;
    float timer;
    public GameObject target;

    Health targetHealth;
    ParticleSystem particles;
    Light theLight;
    PlayerEquipment eqp;

    void Start()
    {
        eqp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        timer = 0;
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        targetHealth = target.GetComponent<Health>();
        particles = gameObject.GetComponent<ParticleSystem>();
        theLight = gameObject.GetComponent<Light>();

    }

    void Update()
    {
        if (eqp.paused == false)
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = target.transform.position + new Vector3(0, 0, -0.5f);
                timer += Time.deltaTime;



                if (timer < 3.0f)
                {
                    targetHealth.LoseHealth(dps * Time.deltaTime);
                }
                else
                {
                    particles.emissionRate = 0;
                    theLight.range -= Time.deltaTime;
                }

                if (timer > 4.0f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
