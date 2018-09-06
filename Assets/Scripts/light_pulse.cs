using UnityEngine;
using System.Collections;

public class light_pulse : MonoBehaviour {

    public Light lt;
    //public float lIntensity;
    public float MinIntensity, MaxIntensity;
    //public float lRange;
    public float MinRange, MaxRange;

    bool grow;
    float timer = 0;
	// Use this for initialization
	void Start () {
        
        lt = GetComponent<Light>();
        timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (lt.intensity < MinIntensity)
            grow = true;

        if (lt.intensity >= MaxIntensity)
            grow = false;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            lt.intensity += Random.Range(-.25f, .25f);
            //timer = .1f;
        }
        if (timer <= 0)
        {
            lt.range += Random.Range(-.25f, .25f);
            timer = .05f;
        }


        if (lt.range < MinRange)
            lt.range = MinRange;

        if (lt.range > MaxRange)
            lt.range = MaxRange;

        if (grow)
            lt.intensity += .5f * Time.deltaTime;
        else
            lt.intensity -= .5f * Time.deltaTime;
	}
}
