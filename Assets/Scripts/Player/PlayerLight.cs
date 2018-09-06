using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour
{


    public float maxLight;
    public float minLight;
    public float currentLight;
    GameObject player;
    Light playerLight;
    float rangeMod = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponentInChildren<Light>();
    }

    void Update()
    {
        playerLight.range = ((currentLight * 4 )/ maxLight) + 1.4f;
        if (currentLight < 20)
            currentLight += Time.deltaTime * 5;
        
    }

    public void GainLight(float Amount)
    {
        currentLight += Amount;
        if (currentLight > maxLight)
            currentLight = maxLight;
        if (rangeMod < 4f)
        {
            rangeMod += Amount * .015f;
        }
    }

    public void LoseLight(float Amount)
    {
        currentLight -= Amount;
        if (currentLight <= 0)
        {
            currentLight = 0;
        }
        if (rangeMod > .4f)
        {
            rangeMod -= Amount * .025f;
        }
    }
}
