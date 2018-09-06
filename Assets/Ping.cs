using UnityEngine;
using System.Collections;

public class Ping : MonoBehaviour
{

    public GameObject ping;
    public GameObject ping2;
    PlayerCooldowns heroCooldowns;
    GameObject[] Shadow;
    public HUDCooldowns UICD;
    void Start()
    {
        UICD = GameObject.Find("Health bar").GetComponent<HUDCooldowns>();
        heroCooldowns = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCooldowns>();
    }

    void CollectLight()
    {
        if (!heroCooldowns.collectorCooling)
        {

            Instantiate(ping, transform.position, transform.rotation);
            Instantiate(ping2, transform.position, transform.rotation);
            heroCooldowns.collectorCooling = true;

            Shadow = GameObject.FindGameObjectsWithTag("Enemy");
            if (Shadow != null)
            {
                for (int i = 0; i < Shadow.Length; i++)
                {
                   // if (Shadow[i].name == "ShadowSpawn" || Shadow[i].name == "ShadowSpawn(Clone)")
                        Shadow[i].SendMessage("BlindedByTheLight", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else
        {
            UICD.FlashCooldown(2);
        }
    }
}
