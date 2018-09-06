using UnityEngine;
using System.Collections;

public class TrailPickupParticles : MonoBehaviour
{

    float lifeTimer;
    // Use this for initialization
    void Start()
    {
        lifeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
