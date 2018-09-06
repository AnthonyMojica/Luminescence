using UnityEngine;
using System.Collections;

public class SpellLightMine : MonoBehaviour
{

    public GameObject explosion;

    bool active;
    float timer;
    float betterTimer;

    float targetTimer;
    PlayerEquipment heroEquipment;

    GameObject target;

    void Start()
    {
        active = false;
        timer = 0;
        betterTimer = 0;
        targetTimer = 0.25f;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        target = null;
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {

            if (target != null)
            {
                Vector2 moveTo = (target.transform.position - transform.position).normalized;
                moveTo = moveTo * Time.deltaTime * 1;

                transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, transform.position.z);
            }
            else
            {

                targetTimer += Time.deltaTime;

                if (targetTimer >= 0.25f)
                {
                    GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

                    for (int i = 0; i < Enemies.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, Enemies[i].transform.position) < 2.5f)
                        {
                            if (Enemies[i].tag == "Enemy")
                            {
                                target = Enemies[i];
                                break;
                            }
                        }
                    }

                    targetTimer = 0;
                }
            }

            betterTimer += Time.deltaTime;
            if (betterTimer >= 3.0f)
            {
                active = true;
            }
            if (active)
            {
                timer += Time.deltaTime;
                if (timer >= 0.25f)
                {
                    Explode();
                }
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            active = true;
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
