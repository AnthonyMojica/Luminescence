using UnityEngine;
using System.Collections;

public class PlayerCooldowns : MonoBehaviour
{
    public float dashCooldown;
    public float dashCooldownMax;
    public bool dashCooling = false;
    public float spellCooldown;
    public float spellCooldownMax;
    public bool spellCooling = false;
    /*public*/ float meleeCooldown;
    public float meleeCooldownMax;
    public bool meleeCooling = false;
    public float collectorCooldown;
    public float collectorCooldownMax;
    public bool collectorCooling = false;
    Animator anim;
    PlayerEquipment equipment;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        dashCooldown = dashCooldownMax;
        spellCooldown = spellCooldownMax;
        meleeCooldown = meleeCooldownMax;
        collectorCooldown = collectorCooldownMax;
        equipment = GetComponent<PlayerEquipment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!equipment.paused)
        {
            if (dashCooling)
            {
                dashCooldown -= Time.deltaTime;
                if (dashCooldown <= 0.0f)
                {
                    anim.CrossFade("Idle", 0.01f);
                    dashCooldown = dashCooldownMax;
                    dashCooling = false;
                }
            }
            if (spellCooling)
            {
                spellCooldown -= Time.deltaTime;
                if (spellCooldown <= 0.0f)
                {
                    spellCooldown = spellCooldownMax;
                    spellCooling = false;
                }
            }
            if (meleeCooling)
            {
                meleeCooldown -= Time.deltaTime;
                if (meleeCooldown <= 0.0f)
                {
                    meleeCooldown = meleeCooldownMax;
                    meleeCooling = false;
                }
            }
            if (collectorCooling)
            {
                collectorCooldown -= Time.deltaTime;
                if (collectorCooldown <= 0.0f)
                {
                    collectorCooldown = collectorCooldownMax;
                    collectorCooling = false;
                }
            }
        }
    }
}
