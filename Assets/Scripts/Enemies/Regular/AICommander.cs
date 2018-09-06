using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AICommander : MonoBehaviour
{


    GameObject player;
    public GameObject[] list;
    public bool isReinforcing = false;
    public bool isAttacking = false;
    public bool isInfected = false;
    public bool isPathing = false;
    CharacterController controller;
    public float atkdmg;
    public float atkrange;
    public float atkcooldown;
    public float atkcooldownref;
    public float movementspeed = 3.0f;
    public float timer;
    public float[] distance;
    PlayerEquipment heroEquipment;
    public float dist;
    public Vector3 vectoplayer;
    public Vector3 path;
    public Vector3 MoveTo;
    public int size;
    float snaredSpeed;
    float SnareTimer;
    bool isSnared;
    public float infectRange;
    public float infecttimer;
    // Use this for initialization
    PlayerMovement hMove;
    GameObject[] Commanders;
    public int commandercount;
    void Start()
    {
        Commanders = new GameObject[20];
        timer = 2.0f;
        commandercount = 0;
        path = Vector3.zero;
        MoveTo = Vector3.zero;
        infecttimer = 3.0f;
        isSnared = false;
        player = GameObject.FindGameObjectWithTag("Player");
        list = GameObject.FindGameObjectsWithTag("Enemy");
        hMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        controller = GetComponent<CharacterController>();
        movementspeed = 1.2f;
    }

    // Update is called once per frame

    void Update()
    {
        if (heroEquipment.paused == false && !hMove.transitioning)
        {
            SnareTimer -= Time.deltaTime;
            if (isInfected)
                Infect();
            if (isAttacking)
            {
                atkcooldown -= Time.deltaTime;
                if (atkcooldown <= 0.0f)
                {
                    atkcooldown = atkcooldownref;
                    isAttacking = false;
                }
            }
            vectoplayer = transform.position - player.transform.position;
            dist = vectoplayer.magnitude;
            FacePlayer();
            size = list.Length;
            if (size == 1 || size == commandercount)
            {
                MoveTowardsPlayer();
                AttackPlayer();
                isReinforcing = false;
            }
            if (!isReinforcing)
            {

                if (size > 1)
                {
                    distance = new float[size];
                    for (int i = 0; i < size; i++)
                    {
                        if (list[i])
                            distance[i] = (transform.position - list[i].transform.position).magnitude;
                    }

                    FacePlayer();

                    if (dist < 3.5f)
                    {
                        RunAway();
                    }
                    else
                        isReinforcing = true;
                }
                else
                {
                    MoveTowardsPlayer();
                    AttackPlayer();
                }
            }

            else
            {

                list = GameObject.FindGameObjectsWithTag("Enemy");
                if (dist < 1.75f)
                {
                    foreach (GameObject obj in list)
                    {
                        obj.SendMessage("UnReinforce", SendMessageOptions.DontRequireReceiver);
                    }
                    isReinforcing = false;
                }
                else
                {

                    foreach (GameObject obj in list)
                    {
                        if (obj.GetComponent<AICommander>() != null)
                        {
                            bool shouldadd = false;
                            for (int i = 0; i < 20; i++)
                            {
                                if (obj != Commanders[i])
                                    shouldadd = true;
                                else
                                    shouldadd = false;

                            }
                            if (shouldadd == true)
                            {
                                Commanders[commandercount] = obj;
                                ++commandercount;
                            }
                        }
                        obj.SendMessage("Reinforce", SendMessageOptions.DontRequireReceiver);
                    }

                }
            }
            if (SnareTimer < 0)
            {
                Unsnare();
                SnareTimer = 100000;
            }
        }

    }
    void RunAway()
    {

        controller.Move(vectoplayer.normalized * Time.deltaTime * movementspeed);
    }
    void MoveTowardsPlayer()
    {
        if (timer == 2.0f)
        {

            MoveTo = Pathfind();

        }
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
            timer = 2.0f;
        controller.Move(MoveTo * Time.deltaTime * movementspeed);
    }

    void FacePlayer()
    {
        //float tempangle=Vector3.Angle (transform.up,disttoplayer);
        //transform.Rotate (Vector3.back,tempangle+180);


        float tempangle = Mathf.Atan2(vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
        tempangle += 270.0f;
        Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6.5f);



    }
    void AttackPlayer()
    {

        if (dist < atkrange && !isAttacking)
        {

            if (player != null)
                player.GetComponent<PlayerMovement>().SendMessage("Knockback", vectoplayer.normalized, SendMessageOptions.DontRequireReceiver);
            player.GetComponent<Health>().LoseHealth(atkdmg);

            isAttacking = true;
        }


    }

    void Slow()
    {
        movementspeed = movementspeed * 0.5f;
    }

    void Unslow()
    {
        movementspeed = movementspeed * 2;
    }
    void Snare()
    {
        movementspeed = 0;
        SnareTimer = 3;
    }
    void Unsnare()
    {
        movementspeed = 1;
    }
    void Decoy()
    {
        player = GameObject.FindGameObjectWithTag("Decoy");
    }
    void UnDecoy()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

    Vector3 Pathfind()
    {


        path = -vectoplayer;

        RaycastHit info;
        Physics.Raycast(transform.position, path.normalized, out info);
        {

            Debug.Log(info.collider.tag);
            if (info.collider.tag == "Wall" && (info.collider.transform.position - transform.position).magnitude <= (GetComponent<Renderer>().bounds.size.x + 0.25f))
            {


                Debug.Log("Direction should change!");

                Vector3 temp = new Vector3(path.x, -path.y);
                //path.x = vectoplayer.y / Mathf.Sqrt(vectoplayer.x * vectoplayer.x + vectoplayer.y*vectoplayer.y);
                //if (vectoplayer.y!=0)
                //path.y = -vectoplayer.x * path.x / vectoplayer.y;

                temp = temp / Mathf.Sqrt(vectoplayer.x * vectoplayer.x + vectoplayer.y * vectoplayer.y);

                if (Random.value > 0.5)
                {
                    temp.x = -temp.x;
                    temp.y = -temp.y;
                }
                path = temp;
                Debug.Log(path);






                //float tempangle = Mathf.Atan2(path.y, path.x) * Mathf.Rad2Deg;
                //tempangle += 90.0f;
                //Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.5f);
            }
            else
            {
                path = -vectoplayer;
                Debug.Log("No need!");

            }


        }


        return path.normalized;
    }


}
