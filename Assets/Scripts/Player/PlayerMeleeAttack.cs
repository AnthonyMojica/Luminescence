using UnityEngine;
using System.Collections;

public class PlayerMeleeAttack : MonoBehaviour
{
    GameObject player;
    PlayerStats playerStats;
    public float attackDamage = 5.0f;
    bool attacking = false;
    float hasRotated = 0.0f;
    float toRotate = 120.0f;
    float rotationDelta = 0.0f;
    public float speed = 3.0f;

    AudioSource audioPlayer;
    
    PlayerEquipment heroEqp;
    Animator anim;

    public GameObject fireDebuff;
    public GameObject frostDebuff;

    public AudioClip meleeSound;
    public AudioClip hitSound;

    Options theoptions;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        player = transform.parent.gameObject;
            playerStats = player.GetComponent<PlayerStats>();
        rotationDelta = player.transform.rotation.z;
        heroEqp = player.GetComponent<PlayerEquipment>();
        audioPlayer = GetComponent<AudioSource>();
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();
    }

    void Update()
    {
        if (heroEqp.paused == false)
        {
            if (attacking)
            {
                hasRotated += 120.0f * Time.deltaTime * speed;
                transform.Rotate(Vector3.forward, 120.0f * Time.deltaTime * speed - rotationDelta);
                rotationDelta = player.transform.rotation.z - rotationDelta;
                if (hasRotated >= toRotate)
                {
                    hasRotated = 0.0f;
                    attacking = false;
                    gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
                    transform.rotation = player.transform.rotation;
                    transform.Rotate(new Vector3(0, 0, 30));
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (attacking && other.gameObject != player && other.gameObject.GetComponent<Health>() != null)
        {
            other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
            audioPlayer.PlayOneShot(hitSound);

            if (heroEqp.equippedEmber == ember.None)
            {
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);

            }
            else if (heroEqp.equippedEmber == ember.Fire)
            {
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);

                GameObject tempObj = (GameObject)Instantiate(fireDebuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFire>().target = other.gameObject;
            }
            else if (heroEqp.equippedEmber == ember.Ice)
            {
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);

                GameObject tempObj = (GameObject)Instantiate(frostDebuff, other.transform.position, other.transform.rotation);
                tempObj.GetComponent<DebuffFrost>().target = other.gameObject;
            }
            else if (heroEqp.equippedEmber == ember.Wind)
            {
                other.SendMessage("GetWrecked", SendMessageOptions.DontRequireReceiver);
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);
            }
            else if (heroEqp.equippedEmber == ember.Life)
            {
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);

            }
            else if (heroEqp.equippedEmber == ember.Death)
            {
                other.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier);

            }
            else if (heroEqp.equippedEmber == ember.Earth)
            {
                other.gameObject.GetComponent<Health>().LoseHealth(attackDamage + playerStats.meleeModifier + 3);

            }
        }
    }

    void Melee()
    {
        if (attacking == false)
        {
            theoptions.AddToMelee();
            audioPlayer.PlayOneShot(meleeSound);
            attacking = true;
            anim.CrossFade("Attacking", 0.01f);
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }

    }
}
