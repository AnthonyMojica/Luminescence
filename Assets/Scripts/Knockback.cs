using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    public float knockbackDistance;
    public float knockbackSpeed;

    bool isGettingKnockedBack = false;
    Vector3 origin;
    Vector2 dir;
    GameObject player;
    CharacterController controller;
    float timerMax;
    float timerCurr;

    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCds;

    bool once;
    bool wasMeleeing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        heroCds = player.GetComponent<PlayerCooldowns>();
        controller = GetComponent<CharacterController>();
        timerMax = knockbackDistance / knockbackSpeed;
        timerCurr = timerMax;
        once = true;
        wasMeleeing = false;
    }

    void Update()
    {
        if (heroEquipment.paused == false)
        {
            //Knockback extra far
            //                       wind ember equipped                                    blast of light                      melee                           whirlwind boot
            if (heroEquipment.equippedEmber == ember.Wind && (heroEquipment.equippedAccessory == accessory.BlastOfLight || heroCds.meleeCooling || (heroEquipment.equippedBoot == boot.Whirlwind && heroCds.dashCooling)))
            {
                if (once)
                {
                    wasMeleeing = heroCds.meleeCooling;
                    knockbackDistance *= 2;
                    once = false;
                }
            }

            if (!isGettingKnockedBack)
            {
                origin = transform.position;
                dir = (transform.position - player.transform.position);
            }
            else if (isGettingKnockedBack)
            {
                timerCurr -= Time.deltaTime;
                controller.Move(dir * Time.deltaTime * knockbackSpeed);

                if (Vector3.Distance(transform.position, origin) >= knockbackDistance || timerCurr <= 0.0f)
                {
                    //Set the distance back to what it was originally
                    if (heroEquipment.equippedAccessory == accessory.BlastOfLight || wasMeleeing && heroEquipment.equippedEmber == ember.Wind)
                    {
                        knockbackDistance *= 0.5f;
                        wasMeleeing = false;
                    }
                    isGettingKnockedBack = false;
                    timerCurr = timerMax;
                }
            }
        }
    }

    void GetWrecked()
    {
        isGettingKnockedBack = true;
        once = true;
        wasMeleeing = false;
    }

}
