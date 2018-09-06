using UnityEngine;
using System.Collections;

public class SkeletonProjectile : MonoBehaviour
{
    GameObject player;
    Health playerHealth;
    PlayerEquipment eq;
    public float speed;
    public float damage;
    bool isActive = true;
    float timer = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Decoy");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        eq = player.GetComponent<PlayerEquipment>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && isActive)
        {
            playerHealth.LoseHealth(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            isActive = false;
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            Destroy(gameObject);
        if (isActive && !eq.paused)
            transform.position += transform.up * speed * Time.deltaTime;
    }

    void Update()
    {
        if (!eq.paused)
            timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            Destroy(gameObject);
        }
    }
}
