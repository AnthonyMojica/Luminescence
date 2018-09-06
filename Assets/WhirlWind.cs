using UnityEngine;
using System.Collections;

public class WhirlWind : MonoBehaviour
{


    //If you're looking for the whirlwind behavior, see "WhirlSpin.cs"

    float timer;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -300 * Time.deltaTime));
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {


            GetComponentInChildren<ParticleSystem>().emissionRate = 0;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }


    }
}
