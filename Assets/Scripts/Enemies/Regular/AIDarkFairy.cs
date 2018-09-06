using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDarkFairy : MonoBehaviour
{

    GameObject player;
    GameObject[] AllLight;
    GameObject currentlight;
    GameObject[] SpellDodge;
    public GameObject Forcefield;

    public GameObject AbsorbParts;
    float AbsorbTimer;
    CharacterController controller;
    public float atkdmg;
    public float atkrange;
    public float stealrange = 0.2f;
    PlayerEquipment heroEquipment;
    public float moveSpeed;
    public bool isReinforced = false;
    public bool isCasting = false;
    public bool isInfected = false;
    bool ResetAbsorbCastingTime = true;
    GameObject Target;
    Vector3 IdleOrgin;
    public GameObject IdleTarget;
    float timer;
    float dodgeTimer;
    float InvincibleTimer;
    float TargetTimer;
    int currentState;
    bool HIncrease;
    bool VIncrease;
    float distanceToPlayer;
    float distanceToTarget;
    float attackCD;
    public float infectRange;
    public float infecttimer;
    float snaredSpeed;
    float SnareTimer;

    PlayerMovement hMove;

    public GameObject darkOrb;
    // Use this for initialization

    void Start()
    {
        distanceToPlayer = 0;
        currentState = 0;
        infecttimer = 3.0f;
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        heroEquipment = player.GetComponent<PlayerEquipment>();
        hMove = player.GetComponent<PlayerMovement>();
        currentlight = null;
        TargetTimer = 0;
        attackCD = 3;
        distanceToTarget = 0;
        dodgeTimer = 0;
        AbsorbTimer = 1000000;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z != -1)
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        if (heroEquipment.paused == false && !hMove.transitioning)
        {
            SnareTimer -= Time.deltaTime;
            dodgeTimer -= Time.deltaTime;
            timer -= Time.deltaTime;
            TargetTimer -= Time.deltaTime;
            attackCD -= Time.deltaTime;
            InvincibleTimer -= Time.deltaTime;


            SpellDodge = GameObject.FindGameObjectsWithTag("Spell");

            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (InvincibleTimer < 0)
            {
                this.tag = "Enemy";
                Forcefield.SetActive(false);
            }
            if (SpellDodge.Length > 0 && dodgeTimer < 0)
            {
                for (int i = 0; i < SpellDodge.Length; i++)
                {
                    if (Vector3.Distance(transform.position, SpellDodge[i].transform.position) < 2)
                    {
                        AbsorbParts.SetActive(false);
                        ResetAbsorbCastingTime = true;
                        Dodge();
                    }
                }
            }

            //DETERMINE STATE
            if (timer < 0)
            {
                timer = .25f;
                if (LocateNearestLight() && currentState != 2)
                {
                    if (currentlight != null)
                        if (Vector3.Distance(currentlight.transform.position, transform.position) < 5)
                            currentState = 1;
                }
                if (distanceToPlayer < 4 && attackCD > 0 && (currentState != 1 || currentState != 2))
                {
                    currentState = 3;
                }
                else if (attackCD < 0 && (currentState != 1 || currentState != 2))
                {
                    currentState = 4;
                }
                else if (currentState != 1 && currentState != 2)
                {
                    currentState = 0;
                }
                if ((currentState == 1 || currentState == 2) && currentlight == null)
                {
                    currentState = 0;
                }
                if (currentState == 1)
                {
                    if (distanceToTarget < 1)
                        currentState = 2;
                }
            }




            switch (currentState)
            {
                case 0:
                    {
                        //IDLE
                        if (TargetTimer < 0)
                        {
                            TargetTimer = 3;
                            Idle();
                        }
                        moveSpeed = .5f;
                        MoveToTarget();
                        break;
                    }
                case 1:
                    {
                        moveSpeed = 2;
                        if (TargetTimer < 0)
                        {
                            TargetTimer = 2.5f;
                            Target = currentlight;
                        }
                        MoveToTarget();
                        //RACE TO LIGHT
                        break;
                    }
                case 2:
                    {
                        moveSpeed = 2;
                        StealLightDrop();
                        Target = currentlight;
                        MoveToTarget();
                        //OBSORB LIGHT
                        break;
                    }
                case 3:
                    {
                        ResetAbsorbCastingTime = true;
                        AbsorbParts.SetActive(false);
                        moveSpeed = 2;
                        Target = player;
                        MoveFromTarget();
                        //AVOID PLAYER
                        break;
                    }
                case 4:
                    {
                        moveSpeed = 3;
                        Target = player;
                        if (distanceToPlayer < 5)
                        {
                            if (attackCD < 0)
                                SpellCast();
                        }
                        MoveToTarget();
                        //FIRE SPELLS
                        break;
                    }
            }
            if (SnareTimer < 0)
            {
                Unsnare();
                SnareTimer = 100000;
            }
        }

        print(currentState);
    }


    bool LocateNearestLight()
    {
        float DistanceCheck = 10000;
        AllLight = GameObject.FindGameObjectsWithTag("LightDrop");

        for (int i = 0; i < AllLight.Length; i++)
        {
            distanceToTarget = Vector3.Distance(transform.position, AllLight[i].transform.position);
            if (distanceToTarget < DistanceCheck)
            {
                distanceToTarget = DistanceCheck;
                currentlight = AllLight[i];
            }
        }

        if (currentlight != null)
            distanceToTarget = Vector3.Distance(transform.position, currentlight.transform.position);
        if (AllLight.Length > 0)
            return true;
        else
            return false;

    }
    void MoveToTarget()
    {
        if (Target != null)
        {
            Vector2 moveTo = (Target.transform.position - transform.position).normalized;
            controller.Move(moveTo * Time.deltaTime * moveSpeed);
        }
    }

    void MoveFromTarget()
    {
        Vector2 moveTo = (transform.position - Target.transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }


    void StealLightDrop()
    {
        if (currentlight != null)
        {
            float distanceToCurrentLight = Vector3.Distance(currentlight.transform.position, transform.position);

            if (distanceToTarget < 1 && ResetAbsorbCastingTime == true)
            {
                ResetAbsorbCastingTime = false;
                AbsorbParts.SetActive(true);
                AbsorbTimer = 1;
            }
            if (ResetAbsorbCastingTime == false)
                AbsorbTimer -= Time.deltaTime;
            if (AbsorbTimer < 0 && distanceToTarget < 1 && ResetAbsorbCastingTime == false)
            {
                ResetAbsorbCastingTime = true;
                AbsorbParts.SetActive(false);
                Destroy(currentlight);
                AbsorbTimer = 1000000;
            }
        }
    }


    void Dodge()
    {
        this.tag = "Invincible";

        Forcefield.SetActive(true);

        InvincibleTimer = 1;
        dodgeTimer = 3;

    }
    void SpellCast()
    {
        AbsorbTimer = 1;
        Vector3 temp = player.transform.position - transform.position;
        float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg + 270;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(darkOrb, transform.position, rot);
        attackCD = 3;
    }

    void Idle()
    {
        int RandX = Random.Range(-10, 10);
        int RandY = Random.Range(-10, 10);

        Target = IdleTarget;
        IdleTarget.transform.position = new Vector3(transform.position.x + RandX, transform.position.y + RandY, -1);

    }
    void Turn()
    {
        //Vector3 vectorToPlayer = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        //angle -= 90.0f;
        //transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        //Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 4);
    }



    #region //CROWD CONTROL SECTION
    void Reinforce()
    {
        if (!isReinforced)
        {
            stealrange *= 1.2f;
            moveSpeed *= 1.2f;
            isReinforced = true;
        }

    }

    void UnReinforce()
    {
        if (isReinforced)
        {
            stealrange /= 1.2f;
            moveSpeed /= 1.2f;
            isReinforced = false;
        }

    }
    void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
    }
    void Snare()
    {
        moveSpeed = 0;
        SnareTimer = 3;
    }
    void Unsnare()
    {
        moveSpeed = 1;
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
    #endregion

}
