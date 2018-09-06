using UnityEngine;
using System.Collections;

public class DebuffFrost : MonoBehaviour {

    float timer;
    public GameObject target;

    ParticleSystem particles;
    Light theLight;

    bool once;

    PlayerEquipment eqp;


    void Start()
    {
        timer = 0;
        particles = gameObject.GetComponent<ParticleSystem>();
        theLight = gameObject.GetComponent<Light>();
        once = true;
        eqp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
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
                if (timer == 0)
                {
                    target.SendMessage("Slow", SendMessageOptions.DontRequireReceiver);
                }
                timer += Time.deltaTime;

                if (timer > 3.0f)
                {
                    if (once)
                    {
                        target.SendMessage("Unslow", SendMessageOptions.DontRequireReceiver);
                        once = false;
                    }
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
