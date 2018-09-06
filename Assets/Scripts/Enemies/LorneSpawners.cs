using UnityEngine;
using System.Collections;

public class LorneSpawners : MonoBehaviour {

    GameObject ParentObject;
    public GameObject SumFairies;
    public GameObject SumParts;

    bool phase;
    //public float maxTime;
    //float currTime;
    //PlayerEquipment equipment;

    void Start()
    {
        phase = false;
        SumParts.SetActive(false);
        //currTime = maxTime;
        //equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    void Update()
    {
        ParentObject = transform.parent.gameObject;
        if(phase)
            transform.RotateAround(ParentObject.transform.position, new Vector3(0, 0, 1), Time.deltaTime * -45f);        
        else
        transform.RotateAround(ParentObject.transform.position,new Vector3(0,0,1), Time.deltaTime * 180f);
    }

    void SpawnFairy()
    {
        Vector3 SpawnPOS = new Vector3(transform.position.x, transform.position.y, -1);

        Instantiate(SumFairies, SpawnPOS, new Quaternion(0,0,0,0));
        GameObject tmp = (GameObject)Instantiate(SumParts, SpawnPOS, transform.rotation);
        Destroy(tmp, 2);
    }

    void ReverseRotate()
    {
        phase = true;
    }
    void SpinNormal()
    {
        phase = false;
    }
}
