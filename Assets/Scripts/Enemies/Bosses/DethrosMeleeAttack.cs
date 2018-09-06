using UnityEngine;
using System.Collections;

public class DethrosMeleeAttack : MonoBehaviour
{
    GameObject dethros;
    public float attackDamage = 5.0f;
    public bool attacking = false;
    public bool stun = false;
    float hasRotated = 0.0f;
    float toRotate = 180.0f;
    float rotationDelta = 0.0f;
    public float speed = 3.0f;
    GameObject player;
    Health pHealth;
    PlayerEquipment pEquip;
    PlayerMovement pMove;

    void Start()
    {
        dethros = transform.parent.gameObject;
        rotationDelta = dethros.transform.rotation.z;
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<Health>();
        pEquip = player.GetComponent<PlayerEquipment>();
        pMove = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (pEquip.paused == false)
        {
            if (attacking)
            {
                hasRotated += 180.0f * Time.deltaTime * speed;
                transform.Rotate(Vector3.forward, 180.0f * Time.deltaTime * speed - rotationDelta);
                rotationDelta = dethros.transform.rotation.z - rotationDelta;
                if (hasRotated >= toRotate)
                {
                    hasRotated = 0.0f;
                    attacking = false;
                    gameObject.SetActive(false);
                    transform.rotation = dethros.transform.rotation;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            pMove.KnockBack(transform.position);
            pHealth.LoseHealth(attackDamage);
        }
    }
}
