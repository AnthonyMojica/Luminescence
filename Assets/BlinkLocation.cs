using UnityEngine;
using System.Collections;

public class BlinkLocation : MonoBehaviour {

    float timer;
    RoomGeneration generator;
    GameObject dungeon;

    GameObject player;
    public GameObject explosion;

    bool tpFailed;
	// Use this for initialization
	void Start () {
        tpFailed = false;
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player");

        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        if (dungeon != null)
            generator = dungeon.GetComponent<RoomGeneration>();

        if (Application.loadedLevelName == "Tutorial")
        {

            if (transform.position.x < 0.5f || transform.position.x > 26.5f || transform.position.y > -0.5f || transform.position.y < -21.32f)
            {
                tpFailed = true;
            }
        }
        else
        {

            if (transform.position.x < 0f || transform.position.x > (generator.finalRoomInfoArray[generator.currentRoom].width - 1) || transform.position.y > 0f || transform.position.y < -(generator.finalRoomInfoArray[generator.currentRoom].height - 1))
            {
                tpFailed = true;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0.05f)
        {
            Instantiate(explosion, player.transform.position, transform.rotation);
            if (tpFailed)
            {
                TpFailed();
            }
            else
            {


                player.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Door")
        {
            tpFailed = true;
        }
    }

    void TpFailed()
    {
        Vector3 temp = transform.position - player.transform.position;
        player.GetComponent<CharacterController>().Move(temp);
        Destroy(gameObject);

    }
}
