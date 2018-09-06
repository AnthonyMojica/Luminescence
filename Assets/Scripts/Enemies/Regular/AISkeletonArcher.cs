using UnityEngine;
using System.Collections;

public class AISkeletonArcher : MonoBehaviour
{

    GameObject player;
    PlayerEquipment heroEquipment;

    public float attackCooldownMax;
    float attackCooldown;
    public float attackMaxRange;
    public float attackMinRange;
    public float moveSpeed;
    public float turnSpeed;
    public float infectRange;
    public float infecttimer;
    float distanceToPlayer;
    public bool isReinforced = false;
    public bool isInfected = false;
    public GameObject projectile;
    bool hasAttacked = false;

    AudioSource aPlayer;

    PlayerMovement hMove;

    CharacterController controller;
    [HideInInspector]
    public Utilities.ppList<GameObject> usedWaypoints;
    //GameObject currentWaypoint;
    Vector3 vectorToPlayer;
    float forgetTimer = 0.0f;

    float SnareTimer;
//    float SnareTimer;
//    bool isSnared;

    void Start()
    {

        infectRange = 1.5f;
//        isSnared = false;   
        infecttimer = 3.0f;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        player = GameObject.FindGameObjectWithTag("Player");
        hMove = player.GetComponent<PlayerMovement>();
        attackCooldown = attackCooldownMax;
        controller = GetComponent<CharacterController>();
        usedWaypoints = new Utilities.ppList<GameObject>();
        aPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (heroEquipment.paused == false && !hMove.transitioning)
        {
            SnareTimer -= Time.deltaTime;
            if (forgetTimer >= 10.0f)
            {
                usedWaypoints.Forget();
                forgetTimer = 0.0f;
            }
            //if (isInfected)
                //Infect();
            if (hasAttacked)
            {
                UpdateAttackCooldown();
            }
            vectorToPlayer = player.transform.position - transform.position;
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (LookForPlayer() && distanceToPlayer > (attackMaxRange + attackMinRange) / 2.0f)
            {
                MoveTowards(player);
            }
            else if (LookForPlayer() && distanceToPlayer < (attackMaxRange + attackMinRange) / 2.0f)
            {
                MoveAway();
            }
            else if (!LookForPlayer())
                MoveTowards(NearestWaypoint());
            Turn();
            if (LookForPlayer() && !hasAttacked && distanceToPlayer <= attackMaxRange && distanceToPlayer >= attackMinRange)
            {
                Attack();
                hasAttacked = true;
            }
            forgetTimer += Time.deltaTime;
            if (SnareTimer < 0)
            {
                Unsnare();
                SnareTimer = 100000;
            }
        }
    }

    void MoveTowards(GameObject target)
    {
        Vector2 moveTo = (target.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
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
    void MoveAway()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * -moveSpeed);
    }

    void Turn()
    {
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        angle -= 90.0f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void Attack()
    {
        aPlayer.Play();
        Instantiate(projectile, transform.position, transform.rotation);
    }

    void UpdateAttackCooldown()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0.0f)
        {
            attackCooldown = attackCooldownMax;
            hasAttacked = false;
        }
    }


    void Decoy()
    {
        player = GameObject.FindGameObjectWithTag("Decoy");
    }
    void UnDecoy()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Reinforce()
    {
        if (!isReinforced)
        {
            attackMaxRange *= 1.5f;
            moveSpeed *= 1.5f;
            isReinforced = true;
        }

    }

    void UnReinforce()
    {
        if (isReinforced)
        {
            attackMaxRange /= 1.5f;
            moveSpeed /= 1.5f;
            isReinforced = false;
        }

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
    void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
    }
    void GetInfected()
    {
        isInfected = true;
    }
    bool LookForPlayer()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, vectorToPlayer, distanceToPlayer);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag == "Wall")
            {
                return false;
            }
        }

        return true;
    }

    GameObject NearestWaypoint()
    {
        // Find all waypoints
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        // Starting at [0], find the closest one
        int x = 0;

        float leastDistance = Vector3.Distance(waypoints[x].transform.position, transform.position);
        float toPlayer = Vector3.Distance(waypoints[x].transform.position, player.transform.position);
        GameObject toReturn = waypoints[x];
        while (true)
        {
            if (LookForWaypoint(waypoints[x]) || x == waypoints.Length - 1)
            {
                break;
            }
            x++;
        }
        bool used = false;
        for (int i = x + 1; i < waypoints.Length; i++)
        {
            for (int index = 0; index < usedWaypoints.Length(); index++)
            {
                if (waypoints[i] == usedWaypoints.Index(index))
                {
                    used = true;
                    break;
                }
            }
            if (used)
                continue;
            if (Vector3.Distance(waypoints[i].transform.position, transform.position) < leastDistance
                || Vector3.Distance(waypoints[i].transform.position, player.transform.position) < toPlayer)
            {
                leastDistance = Vector3.Distance(waypoints[i].transform.position, transform.position);
                toPlayer = Vector3.Distance(waypoints[i].transform.position, player.transform.position);
            }
        }
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (leastDistance == Vector3.Distance(waypoints[i].transform.position, transform.position)
                && toPlayer == Vector3.Distance(waypoints[i].transform.position, player.transform.position))
            {
                toReturn = waypoints[i];
                break;
            }
        }
        return toReturn;
    }

    bool LookForWaypoint(GameObject wp)
    {
        Vector3 vectorToWP = transform.position - wp.transform.position;
        float distance = Vector3.Distance(transform.position, wp.transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, vectorToWP, distance);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag == "Wall")
            {
                return false;
            }
        }

        return true;
    }
}
