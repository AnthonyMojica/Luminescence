using UnityEngine;
using System.Collections;

public class EnemyLight : MonoBehaviour {

    Light thelight;

    int phase;

	// Use this for initialization
	void Start () {
        thelight = gameObject.GetComponent<Light>();
        phase = 0;
        thelight.range = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (phase == 0)
        {
            thelight.range = thelight.range + Time.deltaTime;
            if (thelight.range >= 3.5f)
            {
                phase = 1;
            }
        }
        else
        {
            thelight.range = thelight.range - Time.deltaTime;
            if (thelight.range <= 1.5f)
            {
                phase = 0;
            }
        }
	}
}
