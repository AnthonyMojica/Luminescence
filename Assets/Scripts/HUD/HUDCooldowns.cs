using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDCooldowns : MonoBehaviour
{


    public Image SpellOverlay;
    public ParticleSystem SpellOverlay2;
    public Image BootOverlay;
    public ParticleSystem BootOverlay2;
    public Image AbsorbOverlay;
    public ParticleSystem AbsorbOverlay2;
    float currentScaleSpell;
    float currentScaleBoot;
    float currentScaleAbsorb;
    float CooldownMult;
    PlayerCooldowns theCooldowns;
    CooldownID myID;
    float coolDownTime;
    float CheckCDTimer;

    // Use this for initialization
    void Start()
    {
        CheckCDTimer = 0;
        currentScaleSpell = 0;
        currentScaleBoot = 0;
        currentScaleAbsorb = 0;
        theCooldowns = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCooldowns>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCDTimer -= Time.deltaTime;
        //UPDATE SPELL CD
        if (theCooldowns.spellCooling)
        {
            currentScaleSpell = theCooldowns.spellCooldown / theCooldowns.spellCooldownMax;
            SpellOverlay.fillAmount = currentScaleSpell;
          //  SpellOverlay2.fillAmount = currentScaleSpell;

        }
        else
        {
            SpellOverlay.fillAmount = 0;
            //SpellOverlay2.fillAmount = 0;

        }
        //UPDATE BOOT CD
        if (theCooldowns.dashCooling)
        {
            currentScaleBoot = theCooldowns.dashCooldown / theCooldowns.dashCooldownMax;
            BootOverlay.fillAmount = currentScaleBoot;
           // BootOverlay2.fillAmount = currentScaleBoot;

        }
        else
        {
            BootOverlay.fillAmount = 0;
           // BootOverlay2.fillAmount = 0;
        }

        //UPDATE ABSORB CD

        if (theCooldowns.collectorCooling)
        {
            currentScaleAbsorb = theCooldowns.collectorCooldown / theCooldowns.collectorCooldownMax;
            AbsorbOverlay.fillAmount = currentScaleAbsorb;
            //AbsorbOverlay2.fillAmount = currentScaleAbsorb;
        }
        else
        {
            AbsorbOverlay.fillAmount = 0;
            //AbsorbOverlay2.fillAmount = 0;
        }


        if(CheckCDTimer < 0)
        {
            AbsorbOverlay2.enableEmission = false;
            BootOverlay2.enableEmission = false;
            SpellOverlay2.enableEmission = false;
        }
    }
    public void FlashCooldown(int WhatCD)
    {
        if (WhatCD == 0)
        {
            SpellOverlay2.enableEmission = true;
        }
        if (WhatCD == 1)
        {
            BootOverlay2.enableEmission = true;
        }
        if (WhatCD == 2)
        {
            AbsorbOverlay2.enableEmission = true;
        }
        CheckCDTimer = .1f;
    }

}
