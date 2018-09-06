using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    GameObject player;

    PlayerCooldowns cooldowns;
    PlayerMeleeAttack melee;
    public static bool controller = false;
    public bool isPaused = false;
    bool mapMenu = false;
    Animator anim;
    PlayerMovement move;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cooldowns = player.GetComponent<PlayerCooldowns>();
            melee = player.GetComponentInChildren<PlayerMeleeAttack>();
            anim = player.GetComponent<Animator>();
            move = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        // Toggle between KB/M and Controller modes with 0 (zero) key
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    controller = !controller;
        //}


        // Escape or P to pause
        if (!mapMenu && (Input.GetButtonDown("KBPause") ||  Input.GetButtonDown("CPause")))
        {
            GameObject[] allObjects;
            allObjects = GameObject.FindObjectsOfType<GameObject>();
            isPaused = !isPaused;
            if (isPaused)
            {
                foreach (GameObject obj in allObjects)
                {
                    obj.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                foreach (GameObject obj in allObjects)
                {
                    obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        if (controller)
        {
            #region Controller Input
            if (!isPaused && !mapMenu)
            {
                // LStick to move
                if (Input.GetAxis("CLSHorizontal") != 0.0f || Input.GetAxis("CLSVertical") != 0.0f)
                {
                    player.SendMessage("CMove");
                }
                // RStick to rotate
                if (Input.GetAxis("CRSHorizontal") != 0.0f || Input.GetAxis("CRSVertical") != 0.0f)
                {
                    player.SendMessage("Rotate");
                }
                // RTrigger to melee
                if (Input.GetAxis("CMeleeAndSpells") < 0.0f && !cooldowns.meleeCooling)
                {
                    melee.SendMessage("Melee");
                    cooldowns.meleeCooling = true;
                }
                // LTrigger to cast
                if (Input.GetAxis("CMeleeAndSpells") > 0.0f)
                {
                    player.SendMessage("CastSpell", SendMessageOptions.DontRequireReceiver);
                }
                // LB to dash
                if (Input.GetButtonDown("CDash") && !move.transitioning)
                {
                    player.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
                }
                // RB to collect light
                if (Input.GetButtonDown("CLightCollect"))
                {
                    player.SendMessage("CollectLight", SendMessageOptions.DontRequireReceiver);
                }
                // X to interact
                if (Input.GetButtonDown("CInteract"))
                {
                    player.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                    GameObject chest = GameObject.FindGameObjectWithTag("Chest");
                    if (chest != null)
                        chest.SendMessage("Interact");
                }
            }
            // Start to pause
            //if (!mapMen)
            //{
            //    GameObject[] allObjects;
            //    allObjects = GameObject.FindObjectsOfType<GameObject>();
            //    isPaused = !isPaused;
            //    if (isPaused)
            //    {
            //        foreach (GameObject obj in allObjects)
            //        {
            //            obj.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
            //        }
            //    }
            //    else
            //    {
            //        foreach (GameObject obj in allObjects)
            //        {
            //            obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
            //        }
            //    }
            //}
            // Back for map/stats
            if (!isPaused && Input.GetButtonDown("CMapAndStats"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                mapMenu = !mapMenu;
                if (mapMenu)
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("MapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("UnMapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            #endregion
        }
        else
        {
            #region KB/M controls
            if (!isPaused && !mapMenu && player != null)
            {
                // WASD or arrow keys to move
                if (Input.GetAxis("KBHorizontal") != 0.0f || Input.GetAxis("KBVertical") != 0.0f)
                {
                    player.SendMessage("KBMove");
                }
                else
                {
                    anim.SetFloat("Speed", 0);
					if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalking"))
					   anim.CrossFade("Idle",0.01f);       
                }
                // Mouse to rotate
                player.SendMessage("MouseRotate",SendMessageOptions.DontRequireReceiver);
                // Space to melee
                if (Input.GetButton("KBMelee") && !cooldowns.meleeCooling)
                {
                    melee.SendMessage("Melee");
                    cooldowns.meleeCooling = true;
                }
                // Left Click to cast
                if (Input.GetButtonDown("KBSpells"))
                {
                    player.SendMessage("CastSpell", SendMessageOptions.DontRequireReceiver);
                }
                // Left Shift to dash
                if (Input.GetButtonDown("KBDash") && !move.transitioning)
                {
                    player.SendMessage("Dash", SendMessageOptions.DontRequireReceiver);
                }
                // Q to collect light
                if (Input.GetButtonDown("KBLightCollect"))
                {
                    player.SendMessage("CollectLight", SendMessageOptions.DontRequireReceiver);
                }
                // E to interact
                if (Input.GetButtonDown("KBInteract"))
                {
                    player.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                    GameObject[] chest = GameObject.FindGameObjectsWithTag("Chest");
                    for (int i = 0; i < chest.Length; i++)
                    {
                        if (chest[i] != null)
                            chest[i].SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            // ~ for map/stats
            if (!isPaused && Input.GetButtonDown("KBMapAndStats"))
            {
                GameObject[] allObjects;
                allObjects = GameObject.FindObjectsOfType<GameObject>();
                mapMenu = !mapMenu;
                if (mapMenu)
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("MapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    foreach (GameObject obj in allObjects)
                    {
                        obj.SendMessage("UnMapAndStats", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            #endregion
        }
    }
}
