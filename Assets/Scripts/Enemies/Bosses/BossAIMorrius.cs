using UnityEngine;
using System.Collections;

public class BossAIMorrius : MonoBehaviour
{
    GameObject player;

    //Need this for knockback maybe? remove if wraith doesnt knock back
    PlayerMovement playMove;
    PlayerEquipment heroEquipment;

    //Particles
    public GameObject DarkWavePushParts;
    public GameObject DarkWavePullParts;
    public GameObject ChargeParts;
    public GameObject DarknessParts;
    public GameObject DarknessParts2;
    public GameObject DarknessParts3;
    GameObject TheDark;
    public GameObject SpellCastingParts;
    public GameObject CastingEffects;
    //Summon
    GameObject SpawnedShadow;

    //Spells
    public GameObject MorriusOrb;
    GameObject[] WallCheck;
    public GameObject MorriusExplosion;
    public GameObject RemainsDropoOnWallHit;
    public GameObject RemainsDropEffect;
    public GameObject SummonShadowSpawn;

    Vector2 ChargeTo;
    Health playerHealth;
    CharacterController controller;
    Vector3 WayPoint;
    Vector3 WayPointLeft;
    Vector3 WayPointRight;
    Vector3 Orgin;
    float DistanceToWayPoint;
    float DistanceToPlayer;
    public float moveSpeed;
    public float turnSpeed;
    public bool isInfected = false;
    public bool isReinforced = false;
    float timer;
    int currentState;
    float AttackCD;
    bool castingDarkPULL, castingDarkPUSH, CastingMorriusOrb, Charging, Returning;
    float StateTimer;
    float returnOrginTimer;
    float EndSpellTimer;
    float castTimer;

    bool FirstShadowSwap, SecondShadowSwap;
    bool EndShadowSwap;

    Health Myhealth;
    public GameObject BossHealthBar;
    GameObject healthB;
    GameObject HealthRemaining;
    GameObject[] EnemyCount;
    public AudioSource Source;
    public AudioClip AwakeSound;
    public AudioClip Clip2;
    public GameObject LingerSound;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        AttackCD = 0;
        castingDarkPULL = castingDarkPUSH = CastingMorriusOrb = Charging = Returning = false;         //Spells Set to False
        FirstShadowSwap = SecondShadowSwap = false;
        EndShadowSwap = true;
        timer = 3;
        currentState = 0;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        NewWayPoint();
        EndSpellTimer = 2;
        StateTimer = 5;
        returnOrginTimer = 0;
        Orgin = transform.position;
        WayPointLeft = new Vector3(Orgin.x + 8, Orgin.y, Orgin.z);
        WayPointRight = new Vector3(Orgin.x - 8, Orgin.y, Orgin.z);
        WayPoint = WayPointRight;
        Instantiate(DarknessParts2, new Vector3(10, -10, -6), new Quaternion(0, 0, 0, 0));
        TheDark = (GameObject)Instantiate(DarknessParts3, new Vector3(10, -10, -6), new Quaternion(0, 0, 0, 0));
        TheDark.SetActive(false);
        currentState = 555;
        RenderSettings.ambientLight = new Color(42f / 255f, 42f / 255f, 42f / 255f);
        Myhealth = gameObject.GetComponent<Health>();
		GameObject.FindObjectOfType<BGM> ().finalbossmusic ();
        //healthB = (GameObject)Instantiate(BossHealthBar);
        //HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");
        Source.PlayOneShot(AwakeSound);

    }



    // Update is called once per frame
    void Update()
    {

        if (!heroEquipment.paused)
        {


            if ((Vector3.Distance(transform.position, player.transform.position) < 6 || Myhealth.healthPercent <= .99) && currentState == 555)
            {
                healthB = (GameObject)Instantiate(BossHealthBar);
                HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");
                currentState = 0;
            }
            if (currentState == 555)// DO NOTHING until the player approaches
                return;

            if (HealthRemaining != null)
                HealthRemaining.transform.localScale = new Vector3(Myhealth.healthPercent, 1, 1);
            else
                HealthRemaining = GameObject.FindGameObjectWithTag("Boss Health");

            timer -= Time.deltaTime;
            EndSpellTimer -= Time.deltaTime;
            castTimer -= Time.deltaTime;
            returnOrginTimer -= Time.deltaTime;

            if (currentState == 9001)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 10);
            }
            //CAST SPELLS (if they're active) ////////////////////////////////////////////////////////////////////////////
            if (timer < 0)
            {
                timer = .15f;
                if (castingDarkPUSH)
                    DarkWavePush();
            }
            if (timer < .5f)
            {
                if (CastingMorriusOrb)
                    Attack(); //CD is in function
                if (Charging)
                    Charge();
            }
            if (Returning)
            {
                if (Vector3.Distance(transform.position, Orgin) < 4)
                    EndSpellTimer = -1;
                if (returnOrginTimer < .1f)
                {
                    ReturnToOrgin();
                }

                if (returnOrginTimer < 0)
                {
                    Camera.main.SendMessage("ScreenShake");
                    returnOrginTimer = .4f;
                }

            }
            if (FirstShadowSwap && timer < .2f && castingDarkPULL == true) // FIRST SHADOWSWAP
            {
                castingDarkPULL = false;
                ShadowSwap();
            }
            if (SecondShadowSwap && timer < .2f && castingDarkPULL == true)// SECOND SHADOWSWAP
            {
                castingDarkPULL = false;
                ShadowSwap();
            }
            if (castingDarkPULL)   // PULL RIGHT BEFORE SWAP
                DarkWavePull();
            if (currentState == 0)
                EndSpellTimer = -1;
            //STATE 1 (Controlling the Durations) Activate and set the duration of preperation and casting//////////////////////////////
            if (currentState == 1 && CastingMorriusOrb == false) //CASTING ORBS
            {
                WayPoint = WayPointRight;
                SpellCasting(3, 0); //prep
                CastingMorriusOrb = true;
                EndSpellTimer = 15; // duration
            }
            if (currentState == 2) // IDLE
            {
                EndSpellTimer = -1;
            }
            if (currentState == 3 && castingDarkPUSH == false) //CASTING PUSH
            {
                SpellCasting(1, 0); //prep
                castingDarkPUSH = true;
                EndSpellTimer = 2; // duration
            }
            if (currentState == 4 && Charging == false)//CHARGING!
            {
                WayPoint = player.transform.position;
                SpellCasting(2, 0); //prep
                Charging = true;
                ChargeTo = (WayPoint - transform.position).normalized;
                EndSpellTimer = 8; //duration
            }
            if (currentState == 5)//IDLE
            {
                EndSpellTimer = -1;
            }
            if (currentState == 6 && Returning == false) // RETURNING FROM CHARGE
            {
                returnOrginTimer = 0;
                SpellCasting(.6f, 0);
                Returning = true;
                EndSpellTimer = 8; // duration (noprep)
            }
            if (currentState == 7) // IDLE
            {
                EndSpellTimer = -1;
            }


            if (Myhealth.healthPercent <= .65f && FirstShadowSwap == false) // FIRST SHADOW SWAP
            {
                currentState = 9001; // yup.
                castingDarkPUSH = castingDarkPULL = CastingMorriusOrb = Charging = Returning = false;
                FirstShadowSwap = true;
                SpellCasting(5, 1);
                EndSpellTimer = 20000;
            }
            if (Myhealth.healthPercent <= .35f && SecondShadowSwap == false) //  SECOND SHADOW SWAP
            {
                currentState = 9001;
                castingDarkPUSH = castingDarkPULL = CastingMorriusOrb = Charging = Returning = false;
                SecondShadowSwap = true;
                SpellCasting(5, 1);
                EndSpellTimer = 20000;
            }

            if (currentState == 9001)
                EndSpellTimer = 1000;
            EnemyCount = GameObject.FindGameObjectsWithTag("Enemy"); // IF ALL ADDS ARE DEAD, RESUME FIGHT
            if (EnemyCount.Length <= 1 && EndShadowSwap == false && timer < .2f)
            {
                //print("sTUFF");
                EndShadowSwap = true;
                EndTheSwap();
            }

            if (castTimer < 0) // END PREP CASTING
            {
                StopCasting();
            }
            if (EndSpellTimer < 0) // END SPELLS
            {
                castingDarkPUSH = castingDarkPULL = CastingMorriusOrb = Charging = Returning = false;
                CastingEffects.SetActive(false);
                EndSpellTimer = 10000;
                currentState++;
                if (currentState == 7)
                    currentState = 0;
            }
            //print(currentState);
            //print(EndSpellTimer);
            // print(Vector3.Distance(transform.position, Orgin));
            //Left and right movement
            if (!Charging)
            {
                if (Vector3.Distance(transform.position, WayPointLeft) < 3)
                    WayPoint = WayPointRight;
                if (Vector3.Distance(transform.position, WayPointRight) < 3)
                    WayPoint = WayPointLeft;
            }
            //PUSH PLAYER IF HE GETS TOO CLOSE

            DistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (DistanceToPlayer < 2 && castingDarkPULL == false)
                PushPlayerAway();
        }
    }

    void Move()
    {
        Vector2 moveTo = (WayPoint - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void MoveAwayFromWalls()
    {
        Vector2 moveTo = (new Vector3(8, -8, 0) - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void MoveAtPlayer()
    {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void Attack()
    {
        moveSpeed = 7;
        if (Myhealth.healthPercent < .5f)
            moveSpeed = 9;
        if (Myhealth.healthPercent < .25f)
            moveSpeed = 12;
        if (Myhealth.healthPercent < .75)
        {
            if (returnOrginTimer < .1f)
            {
                MoveAtPlayer();
            }

            if (returnOrginTimer < 0)
            {
                Camera.main.SendMessage("ScreenShake");
                returnOrginTimer = .4f;
            }
        }
        else
        {
            if (returnOrginTimer < .1f)
            {
                Move();
            }

            if (returnOrginTimer < 0)
            {
                Camera.main.SendMessage("ScreenShake");
                returnOrginTimer = .4f;
            }
        }
        CastingEffects.SetActive(true);
        AttackCD -= Time.deltaTime;
        if (AttackCD < 0)
        {
            Vector3 temp = player.transform.position - transform.position;
            float angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg + 270;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(MorriusOrb, transform.position, rot);
            AttackCD = 1;
        }
    }

    void PushPlayerAway()
    {
        player.SendMessage("KnockBack", transform.position, SendMessageOptions.DontRequireReceiver);
    }

    void NewWayPoint()
    {

    }

    void SpellCasting(float castTime, int CastingType)
    {

        WallCheck = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < WallCheck.Length; i++)
        {
            if (Vector3.Distance(transform.position, WallCheck[i].transform.position) < 3.5f)
            {
                MoveAwayFromWalls();
                return;
            }
        }


        castTimer = castTime;
        timer = castTime;

        if (CastingType == 0)
            SpellCastingParts.SetActive(true);
        if (CastingType == 1)
        {
            TheDark.SetActive(true);
            SpellCastingParts.SetActive(true);
            GameObject temp = (GameObject)Instantiate(DarknessParts, transform.position, transform.rotation);
            Destroy(temp, 10);
            castingDarkPULL = true;
        }
       // print("Active");
    }
    void StopCasting()
    {

        SpellCastingParts.SetActive(false);
    }

    void DarkWavePush()
    {
        playMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GameObject temp = (GameObject)Instantiate(DarkWavePushParts, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(temp, 3);
        //Vector3 AboveMorrius = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        playMove.KnockBack(transform.position);
    }

    void DarkWavePull()
    {
        playMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GameObject temp = (GameObject)Instantiate(DarkWavePullParts, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(temp, 3);
        //Vector3 AboveMorrius = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        playMove.PullThePlayer(transform.position);
    }

    void Charge()
    {
        //Check if Charge has hit anything
        DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (DistanceToPlayer < 2)
            endCharge(player.transform.position);

        WallCheck = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < WallCheck.Length; i++)
        {
            if (Vector3.Distance(transform.position, WallCheck[i].transform.position) < 2f)
            {
                endCharge(WallCheck[i].transform.position);
                ExplodeLightReamins();
                return;
            }
        }

        //Continue Charging if nothing hit
        moveSpeed = 10;
        if (Myhealth.healthPercent < .5f)
            moveSpeed = 15;
        if (Myhealth.healthPercent < .25f)
            moveSpeed = 20;
        controller.Move(ChargeTo * Time.deltaTime * moveSpeed);
    }
    void endCharge(Vector3 ExplodeOnSpot)
    {
        EndSpellTimer = -1;
        Instantiate(MorriusExplosion, ExplodeOnSpot, transform.rotation);
        WayPoint = Orgin;
    }
    void ReturnToOrgin()
    {
        moveSpeed = 8;
        Vector2 moveTo = (Orgin - transform.position).normalized;
        controller.Move(moveTo * Time.deltaTime * moveSpeed);
    }
    void ShadowSwap()
    {

        Source.PlayOneShot(Clip2);
        RenderSettings.ambientLight = new Color(1f / 255f, 1f / 255f, 1f / 255f);

        float RandX = Random.Range(-5, 5);
        float RandY = Random.Range(-5, 5);
        gameObject.GetComponent<GenerateLoot>().DropAPieceOfGear(new Vector3(transform.position.x + RandX, transform.position.y + RandY, -1));
           
        if (FirstShadowSwap)
        {
            Instantiate(SummonShadowSpawn, new Vector3(player.transform.position.x - 3, player.transform.position.y, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(SummonShadowSpawn, new Vector3(player.transform.position.x + 3, player.transform.position.y, -1), new Quaternion(0, 0, 0, 0));
        }
        if (SecondShadowSwap)
        {
            Instantiate(SummonShadowSpawn, new Vector3(player.transform.position.x - 3, player.transform.position.y, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(SummonShadowSpawn, new Vector3(player.transform.position.x + 3, player.transform.position.y, -1), new Quaternion(0, 0, 0, 0));
            Instantiate(SummonShadowSpawn, new Vector3(player.transform.position.x + 3, player.transform.position.y, -1), new Quaternion(0, 0, 0, 0));
        }
        timer = 3;
        EndShadowSwap = false;


    }
    void EndTheSwap()
    {
        //print("ENDED");
        transform.position = Orgin;
        RenderSettings.ambientLight = new Color(42f / 255f, 42f / 255f, 42f / 255f);
        currentState = 0;
        EndSpellTimer = 3;
        TheDark.SetActive(false);
    }
    void ExplodeLightReamins()
    {

        Instantiate(RemainsDropEffect, transform.position, new Quaternion(0, 0, 0, 0));
        for (int i = 0; i < 5; i++)
        {
            float RandX = Random.Range(-5, 5);
            float RandY = Random.Range(-5, 5);
            Instantiate(RemainsDropoOnWallHit, new Vector3(transform.position.x + RandX, transform.position.y + RandY, -1f), new Quaternion(0, 0, 0, 0));
        }
    }
    void DestroyHealthBar()
    {
        Destroy(healthB);
        Instantiate(LingerSound);
    }
}