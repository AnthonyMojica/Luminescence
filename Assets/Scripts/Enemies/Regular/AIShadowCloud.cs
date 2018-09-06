using UnityEngine;
using System.Collections;

public class AIShadowCloud : MonoBehaviour
{
    PlayerEquipment heroEquipment;


    public GameObject target;
    public GameObject player;
    public float moveSpeed;
    public bool isInfected = false;
    public float infectRange;
    public float infecttimer;
    public bool isReinforced = false;


    Health heroHP;
    Light heroLight;


    PlayerMovement hMove;
    float SnareTimer;
//    float SnareTimer;
//    bool isSnared;

    void Start()
    {
//        isSnared = false;
        infecttimer = 3.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        hMove = player.GetComponent<PlayerMovement>();
        target = player;
        heroHP = target.GetComponent<Health>();
        heroLight = target.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroEquipment.paused == false && !hMove.transitioning)
        {
            SnareTimer -= Time.deltaTime;
            if(isInfected)
            Infect();
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player");

            }

            Vector2 moveTo = (target.transform.position - transform.position).normalized;
            moveTo = moveTo * Time.deltaTime * moveSpeed;

            transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -1.2f);
            if (SnareTimer < 0)
            {
                Unsnare();
                SnareTimer = 100000;
            }
            Turn();
        }

    }
    void Turn()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        angle -= 270.0f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 7);
    }
    public void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = 2;
    }
    void Reinforce()
    {
        if (!isReinforced)
        {
            moveSpeed *= 1.5f;
            isReinforced = true;
        }

    }

    void UnReinforce()
    {
        if (isReinforced)
        {
            moveSpeed /= 1.5f;
            isReinforced = false;
        }

    }

    void OnDestroy()
    {
        if (heroLight != null)
        {
            heroLight.cookie = null;
        }
    }

    void Decoy()
    {
        player = GameObject.FindGameObjectWithTag("Decoy");
        target = player;
    }
    void UnDecoy()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;

    }

    void Snare()
    {
        moveSpeed = 0;
        SnareTimer = 3;
    }
    void Unsnare()
    {
        moveSpeed = 2;
    }
    void GetInfected()
    {
        isInfected = true;
    }
    void Infect()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            Vector3 dist = transform.position - obj.transform.position;
            if (obj.tag == "Enemy" && dist.magnitude < infectRange)
                obj.SendMessage("GetInfected", SendMessageOptions.DontRequireReceiver);

        }

    }
}
