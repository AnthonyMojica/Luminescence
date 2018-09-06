using UnityEngine;
using System.Collections;

public class BoltExp : MonoBehaviour
{

    public GameObject lightRemains;
    // Use this for initialization
    public bool dropLight;
    void Start()
    {
        if (dropLight)
        {
            Instantiate(lightRemains, transform.position, new Quaternion(0, 0, 0, 0));
            
        }
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
