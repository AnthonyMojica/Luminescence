using UnityEngine;
using System.Collections;

public class AIWraith : MonoBehaviour
{

    GameObject player;

    //Need this for knockback maybe? remove if wraith doesnt knock back
    PlayerMovement playMove;
    PlayerEquipment heroEquipment;

    Health playerHealth;
    CharacterController controller;
    public GameObject DarkOrb;

    Vector3 WayPoint;
    float DistanceToWayPoint;
    float randX, randY;
    // EastDoorX, WestDoorX, SouthDoorY, NorthDoorY; // used for Waypoints
    public float moveSpeed;
    public float turnSpeed;
    bool attacking;
    public bool isInfected = false;
    public bool isReinforced = false;
    float wayPointTimer, timer;

    float AttackTimer;
    bool AttackCD;
    float SnareTimer;
    
//    float SnareTimer;
//    bool isSnared;
    float TopDoor, LeftDoor, roomWidth, roomHeight;
    public Rect Bounds;

    PlayerMovement hMove;

    // Use this for initialization
    void Start()
    {
//        isSnared = false;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        //Random.seed = 8675309;
        attacking = false;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        hMove = player.GetComponent<PlayerMovement>();
        wayPointTimer = 8;
        timer = .5f;
        AttackTimer = 2;
        DetermineDoorPositions();
        Bounds = new Rect(TopDoor, LeftDoor, roomWidth, roomHeight);
        NewWayPoint();

    }




    void DetermineDoorPositions()
    {

        GameObject[] Doors = GameObject.FindGameObjectsWithTag("Door");
        float PossibleBoundary;
        TopDoor = 0;
        LeftDoor = 100000;
        float BottomDoor = 0;
        float RightDoor = 0;


        //FIND TOP DOOR LOCATION
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.y;
            if (TopDoor < PossibleBoundary)
                TopDoor = PossibleBoundary;
        }
        PossibleBoundary = 0;
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.y;
            if (BottomDoor > PossibleBoundary)
                BottomDoor = PossibleBoundary;
        }

        //Width
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.x;
            if (LeftDoor > PossibleBoundary)
                LeftDoor = PossibleBoundary;
        }

        PossibleBoundary = 0;
        for (int i = 0; i < Doors.Length; i++)
        {
            PossibleBoundary = Doors[i].transform.position.x;
            if (RightDoor < PossibleBoundary)
                RightDoor = PossibleBoundary;
        }

        roomWidth = RightDoor - LeftDoor;
        roomHeight = BottomDoor - TopDoor;

    }

    // Update is called once per frame
    void Update()
    {


        if (heroEquipment.paused == false && !hMove.transitioning)
        {
            wayPointTimer -= Time.deltaTime;
            timer -= Time.deltaTime;
            SnareTimer -= Time.deltaTime;

            if (wayPointTimer < 0)
            {
                wayPointTimer = 8;
                NewWayPoint();
            }

            if (timer <= 0)
            {
                timer = .5f;
                DistanceToWayPoint = Vector3.Distance(transform.position, WayPoint);
                //print(DistanceToWayPoint);
            }


            if (DistanceToWayPoint < 1.5f)
            {
                attacking = true;
            }
            else
                attacking = false;


            if (!attacking)
                Move();
            else
            {
                Attack();
            }

            if (SnareTimer < 0)
            {
                Unsnare();
                SnareTimer = 100000;
            }
            Turn();
        }
    }

    void Move()
    {
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Attack()
    {
        if (AttackCD)
        {
            AttackCD = false;
            AttackTimer = 2;
        }
        AttackTimer -= Time.deltaTime;

        if (AttackCD == false && AttackTimer < 0)
        {
            AttackTimer = 1.2f;
            float DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            Vector3 Target = player.transform.position - transform.position;
            RaycastHit[] things = Physics.RaycastAll(transform.position, Target, DistanceToPlayer);
            for (int i = 0; i < things.Length; i++)
            {
                if (things[i].collider.gameObject.tag == "Wall")
                {
                    NewWayPoint();
                    return;
                }
            }
            Instantiate(DarkOrb, transform.position, transform.rotation);
            AttackTimer = 1.2f;
            AttackCD = true;





        }




    }

    void NewWayPoint()
    {

        for (int i = 0; i < 40; i++)
        {
            for (int k = 0; k < 30; k++)
            {
                randX = Random.Range(-5, 5);
                randY = Random.Range(-5, 5);

                Vector2 b1 = new Vector2(player.transform.position.x + randX, player.transform.position.y + randY);
                if (b1.x > Bounds.xMin && b1.x < Bounds.xMax && b1.y < Bounds.yMin && b1.y > Bounds.yMax)
                {
                    break;
                }

            }


            WayPoint = new Vector3(player.transform.position.x + randX, player.transform.position.y + randY);

            GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
            float WaytoWall = 0;
            bool BadPoint = false;

            for (int p = 0; p < Walls.Length; p++)
            {
                WaytoWall = Vector3.Distance(WayPoint, Walls[p].transform.position);

                if (WaytoWall < 2)
                {
                    BadPoint = true;
                }


            }

            if (BadPoint == false)
            {
                break;
            }
        }



        return;

    }

    void Snare()
    {
//        isSnared = true;
//        SnareTimer = 2;
        moveSpeed = 0;
        SnareTimer = 3;
    }
    void Unsnare()
    {
        moveSpeed = 4;
    }

    void Turn()
    {

        if (!attacking)
        {
            Vector3 vectorToWayPoint = WayPoint - transform.position;
            float angle = Mathf.Atan2(vectorToWayPoint.y, vectorToWayPoint.x) * Mathf.Rad2Deg;
            angle -= 90.0f;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
        }
        else
        {
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
            angle -= 90.0f;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
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
    void Slow()
    {
        moveSpeed = moveSpeed * 0.5f;
    }

    void Unslow()
    {
        moveSpeed = moveSpeed * 2;
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
    void GetInfected()
    {
        isInfected = true;
    }
}
