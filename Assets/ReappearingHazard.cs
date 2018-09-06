using UnityEngine;
using System.Collections;

public enum timeOffsets { zero, one, two, three };

public class ReappearingHazard : MonoBehaviour {


    public float timeActive;
    public float timeSleeping;

    public timeOffsets timeOffset;
    PlayerEquipment heroEquipment;

    float timer;

    bool active;

    bool fadingIn;

    public bool doingDamage;
    bool fadingOut;

    SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        active = false;
        fadingIn = false;
        fadingOut = false;
        doingDamage = false;

        if (timeOffset == timeOffsets.zero)
        {
            timer = 0;
        }
        else if (timeOffset == timeOffsets.one)
        {
            timer = -3;
        }
        else if (timeOffset == timeOffsets.two)
        {
            timer = -6;
        }
        else if (timeOffset == timeOffsets.three)
        {
            timer = -9;
        }
	}
	
	// Update is called once per frame
    void Update()
    {

        if (heroEquipment.paused == false)
        {
            timer += Time.deltaTime;
            if (active == false)
            {

                if (timer >= timeSleeping)
                {
                    active = true;
                    fadingIn = true;
                    timer = 0;
                }
            }
            else
            {
                if (timer >= timeActive)
                {
                    active = false;
                    fadingOut = true;
                    doingDamage = false;
                    timer = 0;
                }
            }

            if (fadingIn)
            {
                float alpha = sprite.color.a + (Time.deltaTime * 0.5f);
                if (alpha > 1.0f)
                {
                    fadingIn = false;
                    alpha = 1f;
                    doingDamage = true;

                }
                sprite.color = new Color(1f, 1f, 1f, alpha);

            }
            if (fadingOut)
            {
                float alpha = sprite.color.a - (Time.deltaTime * 0.5f);
                if (alpha < 0.0f)
                {
                    fadingOut = false;
                    alpha = 0.0f;
                }
                sprite.color = new Color(1f, 1f, 1f, alpha);

            }


        }

    }
}
