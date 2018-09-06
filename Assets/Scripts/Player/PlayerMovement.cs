using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{

    public float fullSpeed;
    public float halfSpeed;
    public bool isinHazard = false;
    float speed;
    float knockbackTimer;
    //    GameObject player;
    public bool stunned = false;
    float stunTimer;
    float sTimerMax;

    public bool transitioning = false;
    int leaveDir;
    float leaveTime = 1f;
    bool entering = false;
    int roomMod = 0;
    bool hasLeft = false;

    CharacterController controller;
    //Rigidbody2D rb2d;
    PlayerEquipment heroEquipment;
    PlayerCooldowns heroCooldowns;
    Vector2 MoveDirect;
    Vector2 CharRotate;
    Quaternion Rotation;
    Vector3 Rotate3d;
    Vector3 KnockbackVec;
    Animator anim;

    RoomGeneration generator;

    void Start()
    {
        if (tag != "Decoy")
        {


            //        player = GameObject.FindGameObjectWithTag("Player");
            anim = gameObject.GetComponent<Animator>();
            heroEquipment = gameObject.GetComponent<PlayerEquipment>();
            controller = gameObject.GetComponent<CharacterController>();
            heroCooldowns = gameObject.GetComponent<PlayerCooldowns>();
            //rb2d = GetComponent<Rigidbody2D>();
        }
        else
        {
            heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        }
        fullSpeed = 3.1f;
        halfSpeed = 1.6f;
        knockbackTimer = 0;
        stunned = false;
        sTimerMax = .5f;
        stunTimer = sTimerMax;
        if (GameObject.FindGameObjectWithTag("Dungeon"))
            generator = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
    }

    void Update()
    {

        if (heroEquipment.paused == false && tag != "Decoy")
        {
            if (stunned)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0f)
                {
                    stunTimer = sTimerMax;
                    stunned = false;
                }
            }
            knockbackTimer -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);

            if (knockbackTimer > 0)
            {
                controller.Move(KnockbackVec * 9 * Time.deltaTime);
            }

            if (transitioning)
            {
                if (leaveTime > 0f)
                {
                    switch (leaveDir)
                    {
                        case 0:
                            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
                            break;
                        case 1:
                            transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
                            break;
                        case 2:
                            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
                            break;
                        case 3:
                            transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
                            break;
                        default:
                            break;
                    }
                    leaveTime -= Time.deltaTime;
                    if (leaveTime < 0f)
                    {
                        leaveTime = 0f;
                        entering = true;
                        generator.currentRoom += roomMod;
                        generator.Reset();
                    }
                }
                if (entering)
                {
                    switch (leaveDir)
                    {
                        case 0:
                            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
                            if (transform.position.y <= -generator.finalRoomInfoArray[generator.currentRoom].topPlayerSpawn.y)
                            {
                                transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].topPlayerSpawn.x,
                                    -generator.finalRoomInfoArray[generator.currentRoom].topPlayerSpawn.y, -1f);
                                entering = false;
                                leaveTime = 1f;
                            }
                            break;
                        case 1:
                            transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
                            if (transform.position.x <= generator.finalRoomInfoArray[generator.currentRoom].rightPlayerSpawn.x)
                            {
                                transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].rightPlayerSpawn.x,
                                    -generator.finalRoomInfoArray[generator.currentRoom].rightPlayerSpawn.y, -1f);
                                entering = false;
                                leaveTime = 1f;
                            }
                            break;
                        case 2:
                            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
                            if (transform.position.y >= -generator.finalRoomInfoArray[generator.currentRoom].bottomPlayerSpawn.y)
                            {
                                transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].bottomPlayerSpawn.x,
                                    -generator.finalRoomInfoArray[generator.currentRoom].bottomPlayerSpawn.y, -1f);
                                entering = false;
                                leaveTime = 1f;
                            }
                            break;
                        case 3:
                            transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
                            if (transform.position.x >= generator.finalRoomInfoArray[generator.currentRoom].leftPlayerSpawn.x)
                            {
                                transform.position = new Vector3(generator.finalRoomInfoArray[generator.currentRoom].leftPlayerSpawn.x,
                                    -generator.finalRoomInfoArray[generator.currentRoom].leftPlayerSpawn.y, -1f);
                                entering = false;
                                leaveTime = 1f;
                            }
                            break;
                        default:
                            break;
                    }
                    hasLeft = transitioning = entering;

                }
            }
        }
    }

    void CMove()
    {
        if (heroEquipment.paused == false && !stunned && !transitioning && tag != "Decoy")
        {

            //Check for Left Stick Axis to 
            //see if it is surpressed fully
            //for more speed

            if (Input.GetAxis("CLSHorizontal") > .8f || Input.GetAxis("CLSVertical") > .8f || Input.GetAxis("CLSVertical") < -.8f || Input.GetAxis("CLSHorizontal") < -.8f)
            {

                speed = fullSpeed;
            }
            else
                speed = halfSpeed;

            //Check Left Joysticks for Movement
            MoveDirect.x = Input.GetAxis("CLSHorizontal");
            MoveDirect.y = Input.GetAxis("CLSVertical");


            // if (MoveDirect != Vector2.zero && anim.name != "Spellcasting")
            //  anim.CrossFade("PlayerWalking", 0.01f);
            //else
            //anim.CrossFade("Idle", 0.01f);
            //Normalize the directional vector
            //Factor in speed and time
            anim.SetFloat("Speed", MoveDirect.magnitude);
            MoveDirect.Normalize();
            MoveDirect *= speed * Time.deltaTime;

            //Actually Move the player

            controller.Move(MoveDirect);

            //Rotate the player to where they are moving
            if (MoveDirect != Vector2.zero)
            {

                float angle = Mathf.Atan2(MoveDirect.y, MoveDirect.x) * Mathf.Rad2Deg;
                angle += 270;
                controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            if (MoveDirect != Vector2.zero)
            {
                float angle = Mathf.Atan2(MoveDirect.y, MoveDirect.x) * Mathf.Rad2Deg;
                angle += 270;
                controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void KBMove()
    {
        if (heroEquipment.paused == false && !stunned && !transitioning && tag != "Decoy")
        {

            //Check for WASD to 
            //see if it is surpressed fully
            //for more speed
            if (Input.GetAxis("KBHorizontal") > .8f || Input.GetAxis("KBVertical") > .8f || Input.GetAxis("KBVertical") < -.8f || Input.GetAxis("KBHorizontal") < -.8f)
            {
                speed = fullSpeed;
            }
            else
                speed = halfSpeed;

            //Check WASD for Movement
            MoveDirect.x = Input.GetAxis("KBHorizontal");
            MoveDirect.y = Input.GetAxis("KBVertical");

            //Normalize the directional vector

            //Factor in speed and time
            anim.SetFloat("Speed", MoveDirect.magnitude);

            MoveDirect.Normalize();
            MoveDirect *= speed * Time.deltaTime;

            //Actually Move the player

            controller.Move(MoveDirect);
        }
    }

    void Rotate()
    {
        if (heroEquipment.paused == false && !stunned && tag != "Decoy")
        {

            //Check Right sticks for Rotation
            MoveDirect.x = Input.GetAxis("CRSHorizontal");
            MoveDirect.y = Input.GetAxis("CRSVertical");
            MoveDirect.Normalize();

            //If Right sticks have input
            //override the Move() function rotation
            //with this one for aiming
            if (MoveDirect != Vector2.zero)
            {
                float angle = Mathf.Atan2(MoveDirect.y, -MoveDirect.x) * Mathf.Rad2Deg;
                angle += 90;
                controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            if (MoveDirect != Vector2.zero)
            {
                float angle = Mathf.Atan2(MoveDirect.y, -MoveDirect.x) * Mathf.Rad2Deg;
                angle += 90;
                controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    void MouseRotate()
    {
        if (!stunned && tag != "Decoy" && heroEquipment.paused == false || (heroCooldowns.dashCooling == true && heroEquipment.equippedBoot == boot.Whirlwind))
        {

            //if (heroEquipment.equippedBoot!=boot.Whirlwind)
            //{
            // Rotate to face the mouse at all 
            // times if Mouse/keyboar is active
            Vector3 pos = Camera.main.WorldToScreenPoint(controller.transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //}
        }
    }
    public void KnockBack(Vector3 Direction)
    {
        if (tag != "Decoy")
        {


            knockbackTimer = .1f;
            KnockbackVec = transform.position - Direction;
            KnockbackVec.Normalize();
        }
    }
    public void PullThePlayer(Vector3 Direction)
    {
        if (tag != "Decoy")
        {
            knockbackTimer = .3f;
            KnockbackVec = Direction - transform.position;
            KnockbackVec.Normalize();
        }
    }

    public void LeaveRoom(bool thruExit)
    {
        if (!hasLeft && tag != "Decoy")
        {
            transitioning = true;
            if (thruExit)
            {
                leaveDir = generator.finalRoomInfoArray[generator.currentRoom].exitDir;
                roomMod = 1;
            }
            else
            {
                leaveDir = generator.finalRoomInfoArray[generator.currentRoom].entranceDir;
                roomMod = -1;
            }
            generator.finalRoomInfoArray[generator.currentRoom + roomMod].comingFromEntrance = thruExit;
            hasLeft = true;
        }
    }
}
