using UnityEngine;
using System.Collections;

public class BossAILorne : MonoBehaviour {
   
    
    GameObject player;
    float distanceToPlayer;
    public Vector3 IdleVec;
    public float spellMinRange;
    public float spellMaxRange;
    public float moveSpeed;
    public float turnSpeed;
    CharacterController controller;
    Health Health;
    public float idletimer;
    GameObject temphaz;
    PlayerEquipment heroEquipment;
    public GameObject darkOrb;
    public GameObject fairyorb;
    int maxcasts;
    bool isCasting = false;
    enum state { happy, upset, ravingMad,spiritform };
    state currState = state.happy;
    float retreatTimer;
    int currentcasts;
//    float rTimerMax = 5.0f;
//    float specialTimer;
    float spellTimerMax = 5.0f;
    bool hazarding;
    public GameObject LorneHazard;
    public GameObject enemySpawner;
//    GameObject[] spawners;
    public GameObject seeking;
    Vector3 posref;
    float spellTimer;
//    float spTimerMax = 2.5f;
    public GameObject hazard;
    float hazardPlacementTimer;
//    float hTimerMax = 1.0f;



	// Use this for initialization
	void Start () 
    {
        IdleVec = new Vector3(1, 1, 0);
        maxcasts = 3;
        currentcasts = 0;
        idletimer = 0.0f;
        moveSpeed = 1.2f;
            player = GameObject.FindGameObjectWithTag("Player");
            controller = GetComponent<CharacterController>();
            heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
            Health = GetComponent<Health>();   
            Instantiate(enemySpawner, new Vector3(5, -5, -1), Quaternion.Euler(0, 0, 225));
            Instantiate(enemySpawner, new Vector3(14, -5, -1), Quaternion.Euler(0, 0, 135));
            Instantiate(enemySpawner, new Vector3(5, -14, -1), Quaternion.Euler(0, 0, 315));
            Instantiate(enemySpawner, new Vector3(14, -14, -1), Quaternion.Euler(0, 0, 45));
 //           spawners = GameObject.FindGameObjectsWithTag("LorneSpawn");
            spellMaxRange = 7.0f;
            spellMinRange = 3.0f;
   spellTimerMax=4.0f;
            spellTimer=spellTimerMax;
	//	GameObject.FindObjectOfType<BGM> ().bossmusic ();
		//            spTimerMax = 10.0f;
//            specialTimer = spTimerMax;
        
	
	}
	
	// Update is called once per frame
	void Update ()
    
    {
	 

        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (transform.position.x < -9.0f ||
            transform.position.x > 20f ||
            transform.position.y > 13.0f ||
            transform.position.y < -20f)
        {
            transform.position = new Vector3(9.5f, -9.5f, -1f);
        }
        if (heroEquipment.paused == false)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            Turn();
            if(isCasting)
            {
                spellTimer -= Time.deltaTime;
                if(spellTimer<=0.0f)
                {
                    spellTimer = spellTimerMax;
                    isCasting = false;
                }
            
            }
            if(hazarding)
            {
        
                if(temphaz==null)
                {
                   
                    hazarding = false;

                }

            }
            switch(currState)
            {
                case state.happy:
                    if (distanceToPlayer<spellMinRange)
                    MoveAwayFromPlayer();
                    else if (distanceToPlayer > spellMaxRange)
                        MoveTowardPlayer();
                    else
                        Idle();
                   
                    SpellCast();
                        
                    break;
                case state.upset:
                    if (distanceToPlayer < spellMinRange)
                        MoveAwayFromPlayer();

                    else if (distanceToPlayer > spellMaxRange)
                        MoveTowardPlayer();
                    SpellCast();
                    if (!isCasting && !hazarding && Random.value > 0.65)
                    {
                       temphaz = (GameObject)Instantiate(LorneHazard, transform.position, transform.rotation);
                        hazarding = true;
                    }

                    break;
                case state.ravingMad:
                    moveSpeed = 1.5f;
                    if(!isCasting)
                    { if(Random.value>0.7f)
                    {
                        Instantiate(seeking, transform.position, transform.rotation);

                    }

                    }

                    break;
                case state.spiritform:
          
                    if (GetComponent<SpriteRenderer>().enabled)
                    {
                        posref = transform.position;
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<CharacterController>().enabled = false;

                    }
                    if (transform.position != posref)
                        transform.position = posref;

                    break;


            }
            if (Health.currentHP / Health.maxHP < 0.7f)
                currState = state.upset;
            if (Health.currentHP / Health.maxHP < 0.35f)
                currState = state.ravingMad;
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
    void Turn()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        angle -= 180.0f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
    }

    void SpellCast()
    {
        Vector3 vectoplayer = transform.position - player.transform.position;
        
        if (!Physics.Raycast(transform.position, vectoplayer.normalized, spellMaxRange))
        {
            if (Random.value > 0.1f&&!isCasting)
            {
                Vector3 vectorToPlayer = player.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
                angle -= 90.0f;
                Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
                if(Random.value>0.7)
                Instantiate(darkOrb, transform.position, rot);
                else
                    Instantiate(fairyorb, transform.position, rot);
                ++currentcasts;
                    if(currentcasts==maxcasts)
                    {
                        spellTimer = spellTimerMax;
                isCasting = true;
                currentcasts = 0;
                    }
            }
        }




    }
    void Idle()
    {


       idletimer -= Time.deltaTime;
        if (idletimer <= 0.0f)
        {

            IdleVec.x = Random.value;
            if (Random.value > 0.5f)
                IdleVec.x = -IdleVec.x;
            IdleVec.y = Random.value;
            if (Random.value > 0.5f)
                IdleVec.y = -IdleVec.y;
            idletimer = 2.0f;
        }
        controller.Move(IdleVec.normalized * Time.deltaTime * moveSpeed);

    }
    void FairyAttack()
    {




    }
}
