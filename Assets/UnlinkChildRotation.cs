using UnityEngine;
using System.Collections;

public class UnlinkChildRotation : MonoBehaviour {

    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
