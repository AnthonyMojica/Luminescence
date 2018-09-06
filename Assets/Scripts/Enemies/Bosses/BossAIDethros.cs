using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossAIDethros : MonoBehaviour
{
    GameObject player;
    float distanceToPlayer;
    public float meleeRange;
    public float spellMinRange;
    public float spellMaxRange;
    public float moveSpeed;
    public float turnSpeed;
    CharacterController controller;
    Health myHealth;
    PlayerEquipment heroEquipment;
    enum state { casual, aggravated, intense };
    state currState = state.casual;
    float retreatTimer;
    float rTimerMax = 5.0f;
    float specialTimer;
    float sTimerMax = 10.0f;
    bool wiggled;
    bool wiggling;
    float wiggleTimer;
    float wTimerMax = .5f;
    Vector3 wiggleStartPos;
    public GameObject meleeAttackObject;
    DethrosMeleeAttack meleeScript;
    public GameObject enemySpawner;
    GameObject[] spawners;
    public GameObject spellAttack;
    float spellTimer;
    float spTimerMax = 2.5f;
    public GameObject hazard;
    float hazardPlacementTimer;
    float hTimerMax = 1.0f;
    //public Text YouWinText;
    //bool Victory;
    //float VictoryTimer;

    public GameObject Healthbar;
    GameObject HealthRemaining;

    void Start()
    {
        //Victory = false;
        //VictoryTimer = 5;
        // Get Player ref
        player = GameObject.FindGameObjectWithTag("Player");
        // Get Controller ref for movement
        controller = GetComponent<CharacterController>();
        // Pause bool is in player
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        // Ref to Health
        myHealth = GetComponent<Health>();
        meleeAttackObject.SetActive(false);
        meleeScript = meleeAttackObject.GetComponent<DethrosMeleeAttack>();
        Instantiate(enemySpawner, new Vector3(5, -5, -1), Quaternion.Euler(0, 0, 225));
        Instantiate(enemySpawner, new Vector3(14, -5, -1), Quaternion.Euler(0, 0, 135));
        Instantiate(enemySpawner, new Vector3(5, -14, -1), Quaternion.Euler(0, 0, 315));
        Instantiate(enemySpawner, new Vector3(14, -14, -1), Quaternion.Euler(0, 0, 45));
        spawners = GameObject.FindGameObjectsWithTag("DethSpawn");
        specialTimer = sTimerMax;
        spellTimer = spTimerMax;

        Instantiate(Healthbar);
        HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");
    }

    void Update()
    {
        if(HealthRemaining != null)
            HealthRemaining.transform.localScale = new Vector3(myHealth.healthPercent, 1, 1);
        else
            HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");


        if (transform.position.x < 0f ||
            transform.position.x > 20f ||
            transform.position.y > 0f ||
            transform.position.y < -20f)
        {
            transform.position = new Vector3(9.5f, -9.5f, -1f);
        }
        //if (Victory)
        //{
        //    VictoryTimer -= Time.deltaTime;
        //}
        // Only update if game is unpaused
        if (heroEquipment.paused == false)
        {
            // Get the distance to the player
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (retreatTimer > 0.0f)
            {
                retreatTimer -= Time.deltaTime;
                if (retreatTimer <= 0.0f)
                {
                    retreatTimer = 0.0f;
                }
            }
            switch (currState)
            {
                case state.casual:
                    {
                        if (retreatTimer == 0.0f)
                        {
                            MoveTowardPlayer();
                            Turn();
                            if (distanceToPlayer <= 2f)
                            {
                                MeleeAttack(false);
                                retreatTimer = rTimerMax;
                            }
                        }
                        else
                        {
                            MoveAwayFromPlayer();
                            Turn();
                        }

                        if (myHealth.currentHP < myHealth.maxHP * 2f / 3f)
                        {
                            retreatTimer = 0f;
                            currState = state.aggravated;
                        }
                    }
                    break;
                case state.aggravated:
                    {
                        //                        YouWinText.text = "You Win!";
                        //                        heroEquipment.paused = true;
                        //#if UNITY_STANDALONE
                        //                        if (VictoryTimer < 0)
                        //                        {
                        //                            YouWinText.text = " ";
                        //                            Application.Quit();
                        //                        }
                        //#elif UNITY_EDITOR
                        //                
                        //                        UnityEditor.EditorApplication.isPlaying = false;
                        //#endif
                        spellTimer -= Time.deltaTime;
                        if (retreatTimer == 0.0f)
                        {
                            MoveTowardPlayer();
                            Turn();
                            if (distanceToPlayer <= 2f)
                            {
                                Jump();
                                MeleeAttack(false);
                                retreatTimer = rTimerMax;
                            }
                        }
                        else
                        {
                            if (spellTimer <= 0f)
                            {
                                Instantiate(spellAttack, transform.position, transform.rotation);
                                spellTimer = spTimerMax;
                            }
                            MoveAwayFromPlayer();
                            Turn();
                        }
                        specialTimer -= Time.deltaTime;
                        if (specialTimer <= Time.deltaTime && specialTimer > 0.0f)
                        {
                            player.GetComponent<PlayerMovement>().KnockBack(transform.position);
                            player.GetComponent<PlayerMovement>().stunned = true;
                            wiggling = true;
                            wiggleTimer = wTimerMax;
                            wiggleStartPos = transform.position;
                        }
                        if (wiggling)
                        {
                            Wiggle(wiggleStartPos);
                        }
                        if (specialTimer <= 0f)
                        {
                            if (true)
                            {
                                foreach (GameObject spawn in spawners)
                                {
                                    float c = Random.Range(0f, 1f);
                                    if (c <= .8f)
                                        spawn.SendMessage("SpawnThings");
                                }
                            }
                            specialTimer = sTimerMax;
                        }

                        if (myHealth.currentHP < myHealth.maxHP / 3f)
                            currState = state.intense;
                    }
                    break;
                case state.intense:
                    {
                        spellTimer -= Time.deltaTime;
                        hazardPlacementTimer -= Time.deltaTime;
                        if (retreatTimer == 0.0f)
                        {
                            if (spellTimer <= 0f)
                            {
                                Instantiate(spellAttack, transform.position, transform.rotation);
                                spellTimer = spTimerMax;
                            }
                            MoveTowardPlayer();
                            Turn();
                            if (distanceToPlayer <= 2f)
                            {
                                Jump();
                                MeleeAttack(false);
                                retreatTimer = rTimerMax;
                            }
                        }
                        else
                        {
                            if (hazardPlacementTimer <= 0f)
                            {
                                GameObject h = hazard;
                                h.tag = "Temporary2";
                                Instantiate(h, transform.position, transform.rotation);
                                //Destroy(h, 10f);
                                hazardPlacementTimer = hTimerMax;
                            }
                            MoveAwayFromPlayer();
                            Turn();
                        }
                        specialTimer -= Time.deltaTime;
                        if (specialTimer <= Time.deltaTime && specialTimer > 0.0f)
                        {
                            player.GetComponent<PlayerMovement>().KnockBack(transform.position);
                            player.GetComponent<PlayerMovement>().stunned = true;
                            wiggling = true;
                            wiggleTimer = wTimerMax;
                            wiggleStartPos = transform.position;
                        }
                        if (wiggling)
                        {
                            Wiggle(wiggleStartPos);
                        }
                        if (specialTimer <= 0f)
                        {
                            if (true)
                            {
                                foreach (GameObject spawn in spawners)
                                {
                                    float c = Random.Range(0f, 1f);
                                    if (c <= .9f)
                                        spawn.SendMessage("SpawnThings");
                                }
                            }
                            specialTimer = sTimerMax;
                        }
                    }
                    break;
                default:
                    break;
            }

            // Lock Z
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        }
    }

    void MoveTowardPlayer()
    {

        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }

    void MoveAwayFromPlayer()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * -moveSpeed);
    }

    void MeleeAttack(bool stun)
    {
        meleeAttackObject.SetActive(true);
        meleeScript.stun = stun;
        meleeScript.attacking = true;
    }

    void Turn()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        angle += 90.0f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void SpellAttack()
    {

    }

    void Wiggle(Vector3 startPos)
    {
        wiggleTimer -= Time.deltaTime;
        if (!wiggled)
        {
            wiggled = true;
        }
        else
        {
            Vector2 dir = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            transform.position = new Vector3(startPos.x + dir.x, startPos.y + dir.y, startPos.z);
        }
        if (wiggleTimer <= 0f)
        {
            wiggling = false;
            wiggled = false;
            transform.position = startPos;
        }
    }
    void Jump()
    {
        Vector3 temp = player.transform.position;
        player.transform.position = transform.position;
        transform.position = temp;
    }

    void Poof()
    {




    }
}
