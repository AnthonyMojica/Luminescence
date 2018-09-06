using UnityEngine;
using System.Collections;

public class DethrosSpellAttack : MonoBehaviour
{
    GameObject player;
    Health playerHealth;
    float speed = 7.5f;
    float homingTimer = 1.0f;
    bool hasBeenClose = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        Vector3 look = (player.transform.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(look);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 100.0f);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (homingTimer > 0.0f || hasBeenClose)
        {
            homingTimer -= Time.deltaTime;
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Vector3 look = (player.transform.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1.0f);
        }
        if (distanceToPlayer <= 1.5f)
        {
            hasBeenClose = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHealth.LoseHealth(1.0f);
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
            Destroy(gameObject);
    }
}
