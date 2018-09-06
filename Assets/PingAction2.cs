using UnityEngine;
using System.Collections;

public class PingAction2 : MonoBehaviour {

    GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 10f);

    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -3);

    }
}
