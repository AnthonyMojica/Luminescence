using UnityEngine;
using System.Collections;

public class AIBossDethImproved : MonoBehaviour
{
    GameObject player;
    PlayerMovement playMove;
    PlayerEquipment heroEquipment;
    Health playerHealth;
    Health Myhealth;
    CharacterController controller;

    public GameObject enemySpawner;
    public GameObject DarkOrb;
    public GameObject VanishParts;
    public GameObject AppearParts;
    public GameObject BossLoot;
    public AudioSource Playsounds;
    public AudioClip DethSound1;
    public GameObject LingerSound;
    public GameObject BossHealthBar;
    public GameObject Forcefield;
    public Rect Bounds;

    Vector3 WayPoint;
    // EastDoorX, WestDoorX, SouthDoorY, NorthDoorY; // used for Waypoints
    public float moveSpeed;
    public float turnSpeed;
    public bool isInfected = false;
    public bool isReinforced = false;

    bool vanishbool;
    bool vanishbool2;
    bool increaseOffset = true;
    bool attacking;
    bool AttackCD;

    float vanishTimer;
    float DistanceToWayPoint;
    float randX, randY;
    float wayPointTimer, timer;
    float angleOffset;
    float AttackTimer;
    float snaredSpeed;
    float TopDoor, LeftDoor, roomWidth, roomHeight;
    float DistanceToPlayer;
    float StateTimer;
    float SpawnTimer;

    int CurrentState;
    int SummonState;

    GameObject[] Spawn;
    GameObject healthB;
    GameObject HealthRemaining;

    //Initialize
    void Start()
    {
        DistanceToPlayer = 10;
        CurrentState = 0;
        StateTimer = 1000000;
        SpawnTimer = 20;
        vanishTimer = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        //Random.seed = 8675309;
        attacking = false;

        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Instantiate(enemySpawner, new Vector3(5, -5, -1), Quaternion.Euler(0, 0, 225));
        Instantiate(enemySpawner, new Vector3(14, -5, -1), Quaternion.Euler(0, 0, 135));
        Instantiate(enemySpawner, new Vector3(5, -14, -1), Quaternion.Euler(0, 0, 315));
        Instantiate(enemySpawner, new Vector3(14, -14, -1), Quaternion.Euler(0, 0, 45));

        wayPointTimer = 8;
        timer = .5f;
        AttackTimer = 2;
        DetermineDoorPositions();
        Bounds = new Rect(TopDoor, LeftDoor, roomWidth, roomHeight);
        NewWayPoint();
        angleOffset = 0;
        vanishbool = true;
        vanishbool2 = true;
        SummonState = 0;
        Myhealth = gameObject.GetComponent<Health>();
        healthB = (GameObject)Instantiate(BossHealthBar);
        HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

        GameObject.FindObjectOfType<BGM>().bossmusic();
    }

    //Used for future Waypoint finding
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
        if (HealthRemaining != null)
            HealthRemaining.transform.localScale = new Vector3(Myhealth.healthPercent, 1, 1);
        else
            HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

        if (heroEquipment.paused == false)
        {
            StateTimer -= Time.deltaTime;
            SpawnTimer -= Time.deltaTime;
            wayPointTimer -= Time.deltaTime;
            timer -= Time.deltaTime;
            //if (Myhealth.healthPercent < .75f && StateTimer > 1000)
            //    StateTimer = 1;  //FOR DEBUGGING

            //Activates State 2: "Summon Creatures"
            if (Myhealth.healthPercent < .75f && CurrentState == 0 && SummonState == 0)
            {
                VanishEffect();
                Vanish();
                Spawn = GameObject.FindGameObjectsWithTag("DethSpawn");

                foreach (GameObject Spawner in Spawn)
                {
                    Spawner.SendMessage("TurnOnSummoning", SendMessageOptions.DontRequireReceiver);
                }
                gameObject.tag = "Invincible";
                Forcefield.SetActive(true);
                StateTimer = 20;
                SpawnTimer = 4;
                CurrentState = 1;
                SummonState = 1;
            }
            if (Myhealth.healthPercent < .50f && CurrentState == 0 && SummonState == 1)
            {
                VanishEffect();
                Vanish();
                Spawn = GameObject.FindGameObjectsWithTag("DethSpawn");

                foreach (GameObject Spawner in Spawn)
                {
                    Spawner.SendMessage("TurnOnSummoning", SendMessageOptions.DontRequireReceiver);
                }
                gameObject.tag = "Invincible";
                Forcefield.SetActive(true);
                StateTimer = 20;
                SpawnTimer = 4;
                CurrentState = 1;
                SummonState = 2;
            }
            if (Myhealth.healthPercent < .25f && CurrentState == 0 && SummonState == 2)
            {
                VanishEffect();
                Vanish();
                Spawn = GameObject.FindGameObjectsWithTag("DethSpawn");

                foreach (GameObject Spawner in Spawn)
                {
                    Spawner.SendMessage("TurnOnSummoning", SendMessageOptions.DontRequireReceiver);
                }
                gameObject.tag = "Invincible";
                Forcefield.SetActive(true);
                StateTimer = 20;
                SpawnTimer = 4;
                CurrentState = 1;
                SummonState = 3;
            }
            if (Myhealth.healthPercent < .75f && CurrentState == 1 && StateTimer < 0)
            {
                Spawn = GameObject.FindGameObjectsWithTag("DethSpawn");

                foreach (GameObject Spawner in Spawn)
                {
                    Spawner.SendMessage("TurnOffSummoning", SendMessageOptions.DontRequireReceiver);
                }
                gameObject.tag = "Enemy";
                Forcefield.SetActive(false);
                float RandX = Random.Range(-5, 5);
                float RandY = Random.Range(-5, 5);
                gameObject.GetComponent<GenerateLoot>().DropAPieceOfGear(new Vector3(transform.position.x + RandX, transform.position.y + RandY, -1));
                StateTimer = 60;
                CurrentState = 0;
                vanishTimer = -1;
            }

            switch (CurrentState)
            {

                //BASIC STATE
                case 0:
                    {
                        if (wayPointTimer < .5f && vanishbool)
                        {
                            VanishEffect();
                            vanishbool = false;
                        }
                        if (wayPointTimer < 0)
                        {
                            VanishEffect();
                            vanishbool = true;
                            Vanish();
                            wayPointTimer = 8;
                            NewWayPoint();
                            DistanceToWayPoint = Vector3.Distance(transform.position, WayPoint);
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

                        vanishTimer -= Time.deltaTime;

                        if (vanishTimer < .3f && vanishbool2 == true)
                        {
                            AppearEffect();
                            vanishbool2 = false;
                        }
                        if (vanishTimer < 0)
                        {
                            vanishTimer = 10000;
                            AppearEffect();
                            Appear();
                        }
                        Turn();
                        break;
                    }

                //SUMMON CREATURES STATE
                case 1:
                    {
                        transform.position = new Vector3(9.5f, -9, -1);

                        if (SpawnTimer < 0)
                        {
                            SpawnTimer = 7;
                            RaiseTheDead();
                        }
                        break;
                    }
            }

            if (angleOffset < -50)
            {
                increaseOffset = true;
            }

            if (angleOffset > 50)
            {
                increaseOffset = false;
            }

            if (increaseOffset)
                angleOffset += Time.deltaTime * 55;
            else
                angleOffset -= Time.deltaTime * 55;
        }
    }

    //Moves to chosen waypoint
    void Move()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (DistanceToPlayer < 1)
            player.SendMessage("KnockBack", transform.position, SendMessageOptions.DontRequireReceiver);
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    //Rotates forward vector during spray fire
    void StarFire()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        int p = 0;
        for (int i = 0; i < 6; i++)
        {
            p += 60;
            Vector3 orbRot2 = transform.rotation.eulerAngles;
            Vector3 orbRot1 = new Vector3(orbRot2.x, orbRot2.y, orbRot2.z + p);
            Quaternion rot2 = transform.rotation;
            rot2.eulerAngles = orbRot1;
            Instantiate(DarkOrb, transform.position, rot2);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    //Rotates forward vector during spray fire V2
    void StarFire2()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        int p = 0;
        for (int i = 0; i < 12; i++)
        {
            p += 30;
            Vector3 orbRot2 = transform.rotation.eulerAngles;
            Vector3 orbRot1 = new Vector3(orbRot2.x, orbRot2.y, orbRot2.z + p);
            Quaternion rot2 = transform.rotation;
            rot2.eulerAngles = orbRot1;
            Instantiate(DarkOrb, transform.position, rot2);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    //Send an attack
    void Attack()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (DistanceToPlayer < 1)
            player.SendMessage("KnockBack", transform.position, SendMessageOptions.DontRequireReceiver);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        if (AttackCD)
        {
            AttackCD = false;
            if (Myhealth.healthPercent <= .50f)
            {
                AttackTimer = .3f;
            }
            else if (Myhealth.healthPercent <= .25f)
            {
                AttackTimer = .2f;
            }
            else if (Myhealth.healthPercent <= .10f)
            {
                AttackTimer = .1f;
            }
            else
                AttackTimer = .5f;
        }
        AttackTimer -= Time.deltaTime;
    
        if (AttackCD == false && AttackTimer < 0)
        {
            Instantiate(DarkOrb, transform.position, transform.rotation);
            AttackTimer = .5f;
            AttackCD = true;
        }
    }

    //Pick a new Waypoint
    void NewWayPoint()
    {
        vanishbool2 = true;
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

    //Turn Forward Vector to the player
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
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);

            Quaternion rot = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);

            //print(angleOffset);
        }
    }

    //Clouds for effect
    void VanishEffect()
    {
        Instantiate(VanishParts, transform.position, transform.rotation);
    }

    //Translates under the floor to simulate Vanishing for one waypoint
    void Vanish()
    {
        print("Disappeared");
        transform.position = new Vector3(transform.position.x, transform.position.y, 5);
        NewWayPoint();
        vanishTimer = 2.5f;
        vanishbool2 = true;
        Playsounds.PlayOneShot(DethSound1);
    }

    //Clouds for effect
    void AppearEffect()
    {
        Vector3 goingToAppear = new Vector3(transform.position.x, transform.position.y, -1);
        Instantiate(AppearParts, goingToAppear, transform.rotation);
    }

    //Translate back above the floor
    void Appear()
    {
        print("Re-appeared");
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        if (Myhealth.healthPercent < .25f)
            StarFire2();
        else if (Myhealth.healthPercent < .5f)
            StarFire();
    }

    //Spawning minions on bone piles
    void RaiseTheDead()
    {
        Spawn = GameObject.FindGameObjectsWithTag("DethSpawn");

        foreach (GameObject Spawner in Spawn)
        {

            if (Myhealth.healthPercent < .25f)
                Spawner.SendMessage("SpawnWraith", SendMessageOptions.DontRequireReceiver);
            else
                Spawner.SendMessage("SpawnLivingDead", SendMessageOptions.DontRequireReceiver);
        }
    }

    //Remove UI Effects on death
    void DestroyHealthBar()
    {
        Destroy(healthB);
        Instantiate(BossLoot, transform.position, Quaternion.identity);
        Instantiate(LingerSound);
    }

}
