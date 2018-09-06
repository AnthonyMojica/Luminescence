using UnityEngine;
using System.Collections;

public class DethrosSummoner : MonoBehaviour
{
    public GameObject SumLivingDead;
    public GameObject SumWriath;
    public GameObject SumParts;
    //public float maxTime;
    //float currTime;
    //PlayerEquipment equipment;

    void Start()
    {
        SumParts.SetActive(false);
        //currTime = maxTime;
        //equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    void Update()
    {
  
    }

    void SpawnLivingDead()
    {
        Instantiate(SumLivingDead, transform.position, transform.rotation);
    }
    void SpawnWraith()
    {
        Instantiate(SumWriath, transform.position, transform.rotation);
    }

    void TurnOnSummoning()
    {
        SumParts.SetActive(true);
    }
    void TurnOffSummoning()
    {
        SumParts.SetActive(false);
    }
}

