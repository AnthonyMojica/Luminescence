using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AILorneImproved : MonoBehaviour
{

    PlayerEquipment heroEquipment;

    GameObject player;
    PlayerMovement playMove;
    Health playerHealth;
    //    Health playerHealth;
    //    Rigidbody2D rb2d;
    public bool isReinforced = false;
    public GameObject LorneSignature;
    public GameObject LorneSigHazards;
    GameObject LornSig;
    GameObject LornSigHaz;
    Health MyHealth;
    public GameObject LightRemainsDropped;
    public GameObject LightRemainExplosion;
    public GameObject BossLoot;
    CharacterController controller;
    public float attackDamage;
    public float attackRange;
    public bool isInfected = false;
    float attackCooldown;
    int currentState;
    public float attackCooldownMax;
    public float moveSpeed;
    public float turnSpeed;
    float distanceToPlayer;
    bool AttackActive;
    GameObject[] Fairies;
    GameObject[] FairySpawners;
    Vector3[] WayPoints = new Vector3[5];
    int currentWaypoint;
    float stateTimer;
    float WayPointChangeTimer;
    float fairieSpawnTimer;
    public GameObject PullParticles;
    bool doOnce;
    bool DoOnce2;
    public GameObject BossHealthBar;
    GameObject HealthRemaining;
    GameObject healthB;

    void Start()
    {
        DoOnce2 = true;
        doOnce = true;
        currentState = 0;
        currentWaypoint = 1;
        stateTimer = 5;
        WayPointChangeTimer = 5;
        fairieSpawnTimer = 5;
        AttackActive = false;
        WayPoints[0] = new Vector3(4, -16, -1);
        WayPoints[1] = new Vector3(4, -4, -1);
        WayPoints[2] = new Vector3(16, -4, -1);
        WayPoints[3] = new Vector3(16, -16, -1);
        WayPoints[4] = new Vector3(10, -10, -1);
        player = GameObject.FindGameObjectWithTag("Player");
        playMove = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<Health>();
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        MyHealth = gameObject.GetComponent<Health>();
        //playerHealth = player.GetComponent<Health>();
        attackCooldownMax = 1;
        attackCooldown = attackCooldownMax;
        //rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
        LornSig = (GameObject)Instantiate(LorneSignature, new Vector3(10, -10f, -1), new Quaternion(0, 0, 0, 0));
        LornSigHaz = (GameObject)Instantiate(LorneSigHazards, new Vector3(7.25f, -9.55f, -1), new Quaternion(0, 0, 0, 0));
        healthB = (GameObject)Instantiate(BossHealthBar);
        HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

    }

    void Update()
    {


        if (HealthRemaining != null)
            HealthRemaining.transform.localScale = new Vector3(MyHealth.healthPercent, 1, 1);
        else
            HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

        if (heroEquipment.paused == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            stateTimer -= Time.deltaTime;
            WayPointChangeTimer -= Time.deltaTime;
            fairieSpawnTimer -= Time.deltaTime;

            if (stateTimer < 0)
            {
                if (currentState == 0)
                {
                    FairySpawners = GameObject.FindGameObjectsWithTag("DethSpawn");

                    foreach (GameObject Spawner in FairySpawners)
                    {
                        Spawner.SendMessage("ReverseRotate", SendMessageOptions.DontRequireReceiver);
                    }
                    currentState = 1;
                    currentWaypoint = 4;
                    WayPointChangeTimer = 5;
                    fairieSpawnTimer = 5f;
                    stateTimer = 25;
                }
                else
                {
                    FairySpawners = GameObject.FindGameObjectsWithTag("DethSpawn");

                    foreach (GameObject Spawner in FairySpawners)
                    {
                        Spawner.SendMessage("SpinNormal", SendMessageOptions.DontRequireReceiver);
                    }
                    DoOnce2 = true;
                    doOnce = true;
                    this.tag = "Enemy";
                    LornSig.SetActive(false);
                    currentState = 0;
                    fairieSpawnTimer = 5;
                    stateTimer = 20;
                    if (MyHealth.healthPercent > .50f)
                        moveSpeed = 3;
                    else
                        moveSpeed = 4f;
                }

            }


            switch (currentState)
            {
                case 0:
                    {
                        moveSpeed = 2;

                        if (MyHealth.healthPercent < .5f)
                            moveSpeed = 3;
                        if (MyHealth.healthPercent < .25f)
                            moveSpeed = 4;

                        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                        if (AttackActive)
                        {
                            UpdateAttackCooldown();
                        }
                        if (distanceToPlayer >= attackRange)
                            Move();

                        Turn();

                        if (distanceToPlayer <= attackRange && !AttackActive)
                        {
                            Attack();
                        }
                        break;
                    }
                case 1:
                    {

                        //Switch Waypoints (Not Spawn Fairies)
                        if (fairieSpawnTimer < 2)
                        {
                            this.tag = "Invincible";
                            SignatureMove();
                        }
                        if (fairieSpawnTimer < 1)
                        {
                            if(DoOnce2)
                            {
                                DoOnce2 = false; 
                                ExplodeLightReamins();
                            }
                         
                            LornSig.SetActive(true);
                        }

                        if (fairieSpawnTimer < 0)
                        {
                            moveSpeed = 7;
                            if (MyHealth.healthPercent < .50f)
                                SummonFairies();
                            SummonFairies();
                            fairieSpawnTimer = 30;
                        }

                        if (WayPointChangeTimer < 0)
                        {
                            WayPointChangeTimer = 1.8f;
                            currentWaypoint++;
                            if (currentWaypoint > 3)
                                currentWaypoint = 0;
                        }
                        MoveToWayPoint();
                        break;
                    }
            }

        }
    }

    void Move()
    {
        //rb2d.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed));
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void MoveToWayPoint()
    {
        Vector2 moveTo = (WayPoints[currentWaypoint] - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void Turn()
    {
        //Vector3 vectorToPlayer = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        //angle -= 90.0f;
        //Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);w
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void Attack()
    {
        if (playMove != null && currentState == 0)
        {
            playMove.KnockBack(transform.position);
            player.GetComponent<Health>().LoseHealth(attackDamage);
        }
    }

    void UpdateAttackCooldown()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0.0f)
        {
            attackCooldown = attackCooldownMax;
            AttackActive = false;
        }
    }
    void SummonFairies()
    {
        FairySpawners = GameObject.FindGameObjectsWithTag("DethSpawn");

        foreach (GameObject Spawner in FairySpawners)
        {
            Spawner.SendMessage("SpawnFairy", SendMessageOptions.DontRequireReceiver);
        }
    }

    void SignatureMove()
    {
        playMove = player.GetComponent<PlayerMovement>();
        if (playMove != null)
        {

            PlayerLight playHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLight>();
            playHealth.currentLight = 0;



            playMove.PullThePlayer(transform.position);
            if (doOnce)
            {
                Instantiate(PullParticles, player.transform.position, new Quaternion(0, 0, 0, 0));
                doOnce = false;
                float RandX = Random.Range(-5, 5);
                float RandY = Random.Range(-5, 5);
                gameObject.GetComponent<GenerateLoot>().DropAPieceOfGear(new Vector3(transform.position.x + RandX, transform.position.y + RandY, -1));
            }
        }

    }


    void ExplodeLightReamins()
    {

        Instantiate(LightRemainExplosion, player.transform.position, new Quaternion(0, 0, 0, 0));
        for (int i = 0; i < 20; i++)
        {
            float RandX = Random.Range(-5, 5);
            float RandY = Random.Range(-5, 5);
            Instantiate(LightRemainsDropped, new Vector3(player.transform.position.x + RandX, player.transform.position.y + RandY, -1f), new Quaternion(0, 0, 0, 0));
        }
    }
    void DestroyHealthBar()
    {
        LornSig.SetActive(false);
        Destroy(healthB);
        Instantiate(BossLoot, transform.position, Quaternion.identity);
    }

}

