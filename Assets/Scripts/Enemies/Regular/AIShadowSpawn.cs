using UnityEngine;
using System.Collections;

public class AIShadowSpawn : MonoBehaviour
{

    GameObject player;
    Health ShadowHealth;
    Health playerHealth;
    PlayerLight playerLight;
    CharacterController controller;
    GameObject NearestLightPickup;
    public Animation GetHurt;
    public Animator ghastAnimator;
    public GameObject Lightexplosion;
    Vector3 vectoplayer;
    float timerforfacingvectorupdate;
    float timerforfacingvectorupdate2;
    public AudioSource Playsounds;
    public AudioClip SapLight;
    public AudioClip GetScary;
    bool EnragedSoundPlaying;
    PlayerEquipment heroEquipment;

    Vector2 Moveto;
    Vector3 WayPoint;

    Vector3 AmIStuck;

    public float speed = 1;
    float DistancetoPlayer;
    float DistancetoLight;

    float timer;
    float DmgTimer;
    float playerConsumeTimer;
    int timerCount;
    int stuckCounter;
    bool newWayPoint;
    float maxSpeed;
    float AnimTimer;
    public int StoredLight;
    public GameObject BlindedParts;
    public GameObject ReturningParts;
    bool AbleToMelee;
    public float attackDamage;
    float AttackTimer;
    bool turningr = true;
    float SnareTimer;
    GameObject PrimaryThreat;
    GameObject SecondaryThreat;
    PlayerMovement playMove;
    public enum State { Idle = 0, Evade, SuperEvade, Enrage }
    State CurrentState = State.Idle;
    void Start()
    {
        timerforfacingvectorupdate = 2.0f;
        timerforfacingvectorupdate2 = 2.0f;
        AttackTimer = .5f;
        AbleToMelee = false;
        EnragedSoundPlaying = false;
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Playsounds = gameObject.GetComponent<AudioSource>();
        //Random.seed = 42;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponent<PlayerLight>();
        playMove = player.GetComponent<PlayerMovement>();
        ShadowHealth = GetComponent<Health>();
        Moveto = new Vector3(0, 0);
        controller = GetComponent<CharacterController>();
        speed = 1;
        timerCount = 1;
        DmgTimer = 1;
        maxSpeed = 1;
        timer = 1;
        playerConsumeTimer = 1;
        newWayPoint = true;
        stuckCounter = 0;
        DistancetoLight = 100;
        DistancetoPlayer = 100;
        //AnimTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (turningr == false)
        {
            timerforfacingvectorupdate -= Time.deltaTime;
            if(timerforfacingvectorupdate<=0)
            {
                turningr=true;
                timerforfacingvectorupdate2=2.0f;
	    
            }


      }
      if (turningr == true)
      {
          timerforfacingvectorupdate2 -= Time.deltaTime;
          if(timerforfacingvectorupdate2<=0)
          {
              turningr=false;
              timerforfacingvectorupdate=2.0f;
          }
      }
		TurnityTurn ();

        AttackTimer -= Time.deltaTime;
        if (heroEquipment.paused == false)
        {
            SnareTimer -= Time.deltaTime;

            //WorkPlease.SetInteger("AnimationNum", 0);
            timer -= Time.deltaTime;
            //AnimTimer -= Time.deltaTime;
            DistancetoPlayer = DistancetoLight = 100;
            //calculate distance to player.
            if (timer < 0)
            {
                DistancetoPlayer = Vector3.Distance(transform.position, player.transform.position);
                timer = 1;
                //print(DistancetoPlayer);

                timerCount += 1;
                stuckCounter--;
            }

            //"Animated" Movement
            if (newWayPoint)
            {
                speed = maxSpeed;
                newWayPoint = false;
            }
            if (speed > 0)
                speed -= 2 * Time.deltaTime;

            //Locate Primary and 2ndary Threat
            //PrimaryThreat = findNearestWithTag(transform.position, "Player");
            //Locate Nearest Light Pickup
            NearestLightPickup = findNearestWithTag(transform.position, "LightDrop");
            if (NearestLightPickup != null)
                DistancetoLight = Vector3.Distance(transform.position, NearestLightPickup.transform.position);


            //if (DistancetoLight < DistancetoPlayer)
            //{
            //    PrimaryThreat = NearestLightPickup;
            //    DistancetoPrimary = DistancetoLight;
            //}
            //else
            //{
            //    PrimaryThreat = player;
            //    DistancetoPrimary = DistancetoPlayer;
            //}
            //print(DistancetoPlayer);
            //Determine Which behavior to run
            if (playerLight.currentLight < 10)
            {
                CurrentState = State.Enrage;
            }
            else if (DistancetoPlayer > 7)
            {
                CurrentState = State.Idle;
            }
            else if (DistancetoPlayer < 7 && DistancetoPlayer > 3)
            {
                CurrentState = State.Evade;
            }
            else if (DistancetoPlayer < 3)
            {
                CurrentState = State.SuperEvade;
            }

            if (playerLight.currentLight > 10 && EnragedSoundPlaying == true)
            {
                EnragedSoundPlaying = false;
                Playsounds.Stop();
            }
            //print(CurrentState);
            //Call the Current behavior
            switch (CurrentState)
            {
                case State.Idle:
                    {
                        EnragedSoundPlaying = false;
                        Idle();
                        break;
                    }
                case State.Evade:
                    {
                        EnragedSoundPlaying = false;
                        Evade();
                        break;
                    }
                case State.SuperEvade:
                    {
                        EnragedSoundPlaying = false;
                        SuperEvade();
                        break;
                    }
                case State.Enrage:
                    {
                        Enrage();
                        break;
                    }
            }

            //Change Animations

            //if (AnimTimer < 0)
            //{
            //    if (State.Enrage == CurrentState)
            //        ghastAnimator.SetInteger("AnimationNum", 2);
            //    else
            //        ghastAnimator.SetInteger("AnimationNum", 0);

            //    //print("did stuff");
            //}

            if (DistancetoPlayer < 2f && AbleToMelee == true)
                Attack();

            Moveto.Normalize();
            if (Physics.Raycast(transform.position, Moveto, .7f) && CurrentState != State.Enrage)
            {
                if (Moveto.x > 0)
                    Moveto.x = 1;
                else
                    Moveto.x = -1;
                if (Moveto.y > 0)
                    Moveto.y = 1;
                else
                    Moveto.y = -1;
                //print(Moveto);
            }
            // speed = 2;
            Moveto *= speed * Time.deltaTime;

            ConsumeLight();
            if (speed > 1)
                controller.Move(Moveto);

            if (SnareTimer < 0)
            {
                Unsnare();
                SnareTimer = 100000;
            }
        }
    }

    void Idle()
    {
        maxSpeed = 3;
        AbleToMelee = false;

        //Change Idle Waypoint every 2 seconds.
        if (timerCount > 2)
        {
            timerCount = 0;
            float newX = Random.Range(-2f, 2f);
            float newY = Random.Range(-2f, 2f);
            WayPoint = new Vector3(transform.position.x + newX, transform.position.y + newY, -1.4f);

            Moveto = transform.position - WayPoint;
            newWayPoint = true;
        }
    }

    void Evade()
    {
        AbleToMelee = false;

        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = transform.position - player.transform.position;
            Moveto = -WayPoint;
            newWayPoint = true;
        }

    }

    void SuperEvade()
    {
        AbleToMelee = false;

        maxSpeed = 4;

        if (stuckCounter <= 0)
        {
            if (transform.position == AmIStuck)
            {
                Jump();
            }
            AmIStuck = transform.position;
            stuckCounter = 1;
        }
        if (timerCount > 1)
        {
            timerCount = 0;

            WayPoint = transform.position - player.transform.position;
            Moveto = -WayPoint;
            newWayPoint = true;

        }

    }
    void Snare()
    {
        speed = 0;
        SnareTimer = 3;
    }
    void Unsnare()
    {
        speed = 1;
    }
    void Enrage()
    {
        if (EnragedSoundPlaying == false)
        {
            Playsounds.PlayOneShot(GetScary);
            EnragedSoundPlaying = true;
        }
        maxSpeed = 3.5f;
        WayPoint = player.transform.position - transform.position;
        Moveto = WayPoint;
        newWayPoint = true;



        if (AttackTimer < 0)
        {
            AttackTimer = .5f;
            AbleToMelee = true;
        }
    }

    void Attack()
    {
        if (playMove != null)
        {
            playMove.KnockBack(transform.position);
            player.GetComponent<Health>().LoseHealth(attackDamage);
        }
    }
    void Jump()
    {
        Vector3 temp = player.transform.position;
        player.transform.position = transform.position;
        transform.position = temp;
        //print("JUMP!");
    }

    void ConsumeLight()
    {
        DmgTimer -= Time.deltaTime;
        playerConsumeTimer -= Time.deltaTime;
        if (playerConsumeTimer <= 0)
        {
            if (!Playsounds.isPlaying)
            {
                Playsounds.PlayOneShot(SapLight);
            }
            playerConsumeTimer = 2;
            Instantiate(Lightexplosion, player.transform.position, player.transform.rotation);
            playerLight.currentLight -= 4;
            StoredLight += 4;
        }


        if (DistancetoLight < 2 && DmgTimer <= 0)
        {

            DmgTimer = .5f;
            //print("GetLight");
            //INSTANTIATE PARTICLES ON THE PLAYER HERE TO SIGNIFY STEALING LIGHT AND DAMAGING THE SPAWN
            Instantiate(Lightexplosion, NearestLightPickup.transform.position, NearestLightPickup.transform.rotation);
            //print(DistancetoPrimary);

            //NearestLightPickup.GetComponent<LightID>();
            if (NearestLightPickup.GetComponent<LightID>().theID == lightID.Large)
            {
                StoredLight += 18;
            }
            else if (NearestLightPickup.GetComponent<LightID>().theID == lightID.Small)
            {
                StoredLight += 1;
            }
            Destroy(NearestLightPickup);

           // ghastAnimator.SetInteger("AnimationNum", 1);
           // AnimTimer = .3f;
        }

        //DO Dead things
        if (ShadowHealth.currentHP <= 0)
        {
            BlindedByTheLight();
            Destroy(gameObject);
        }




    }

    void BlindedByTheLight()
    {
        PlayerLight ReturnLight = player.GetComponent<PlayerLight>();
        ShadowHealth = GetComponent<Health>();
        if (ShadowHealth != null)
            ShadowHealth.LoseHealth(StoredLight);

        StoredLight += 8;
        Instantiate(BlindedParts, transform.position, transform.rotation);
        Instantiate(ReturningParts, transform.position, transform.rotation);
        ReturnLight.currentLight += StoredLight;

        if (ReturnLight.currentLight > ReturnLight.maxLight)
        {
            ReturnLight.currentLight = ReturnLight.maxLight;
        }

        StoredLight = 0;

    }




    public static GameObject findNearestWithTag(Vector3 fromPosition, string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

        if (gos.Length == 1)
            return gos[0];

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = fromPosition;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

 void TurnityTurn()
 {
     //float tempangle=Vector3.Angle (transform.up,disttoplayer);
     //transform.Rotate (Vector3.back,tempangle+180);
     if (timerforfacingvectorupdate == 2.0f) 
 
     {
         vectoplayer = player.transform.position - transform.position;
         turningr=false;
     }
     float tempangle = Mathf.Atan2 (vectoplayer.y, vectoplayer.x) * Mathf.Rad2Deg;
     if (turningr)
     {
 
         tempangle += 90.0f;
     }
     else
     {	 
         tempangle -= 90.0f;
     }
     Quaternion rotation = Quaternion.AngleAxis(tempangle, Vector3.forward);
     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);
 
 
 
 }


}
