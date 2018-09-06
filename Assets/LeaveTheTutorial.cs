using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaveTheTutorial : MonoBehaviour
{

    public Text LeaveText;
    bool start;

    

    // Use this for initialization
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            LeaveText.color += new Color(0, 0, 0, 1 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {

            start = true;
            //GameObject.FindObjectOfType<Options>().tutEquip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
            LevelManager.Load("Game");
        }
    }

}
