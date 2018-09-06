using UnityEngine;
using System.Collections;

public class TutorialEmptyCheck : MonoBehaviour {

    public GameObject Door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
    void OnTriggerStay(Collider Other)
    {
        if (Other.tag == "Player")
        {
            GameObject[] EnemyCount2 = GameObject.FindGameObjectsWithTag("Enemy");
            if (EnemyCount2.Length == 0)
            {
                Destroy(Door);
            }

        }
    }
}
