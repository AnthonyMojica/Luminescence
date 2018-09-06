using UnityEngine;
using System.Collections;

public class WestDoor : MonoBehaviour
{
    public bool isLocked = true;
    public GameObject dungeon;
    RoomGeneration generator;
    GameObject player;
	public bool displaytooltips = false;
    int enemyCount;
    bool iHopeThisWorks;
    bool easyMode;
    PlayerMovement movement;
    bool hasTransitioned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        generator = dungeon.GetComponent<RoomGeneration>();
        isLocked = true;
        iHopeThisWorks = true;
        easyMode = GameObject.FindObjectOfType<Options>().easyMode;
        hasTransitioned = false;
    }
 
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("ShadowSpawn").Length + GameObject.FindGameObjectsWithTag("Invincible").Length;
        if (movement.transitioning)
        {
            isLocked = false;
            Unlock();
        }
        else if (!hasTransitioned)
        {
            hasTransitioned = true;
            isLocked = true;
            Lock();
        }
        if (isLocked &&
            ((generator.currentRoom != 0 && generator.currentRoom != (easyMode ? 11 : 9) && generator.currentRoom != (easyMode ? 22 : 18)
            && generator.finalRoomInfoArray[generator.currentRoom].entranceDir == 1)
            || (generator.currentRoom < (easyMode ? 33 : 27) && generator.finalRoomInfoArray[generator.currentRoom].exitDir == 1))
            && enemyCount == 0)
        {
            isLocked = false;
            Unlock();
        }
    }

    void Lock()
    {
        BoxCollider[] tryingThis = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider box in tryingThis)
        {
            if (box.gameObject.name == "Lock")
            {
                box.enabled = true;
            }
        }
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    void Unlock()
    {
        BoxCollider[] tryingThis = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider box in tryingThis)
        {
            if (box.gameObject.name == "Lock")
            {
                box.enabled = false;
            }
        }
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && generator.currentRoom == (easyMode ? 32 : 26)
                && !movement.transitioning)
        {
            Application.LoadLevel("EndCutscene");
        }
        if (iHopeThisWorks)
        {
            if (other.gameObject == player
                && !movement.transitioning
                && generator.finalRoomInfoArray[generator.currentRoom].exitDir == 1
                && generator.currentRoom < (easyMode ? 33 : 27))
            {
                movement.LeaveRoom(true);
                //++generator.currentRoom;
                //generator.finalRoomInfoArray[generator.currentRoom].comingFromEntrance = true;
                //generator.Reset();
                iHopeThisWorks = false;
            }
            else if (other.gameObject == player && !movement.transitioning && generator.currentRoom > 0)
            {
                movement.LeaveRoom(false);
                //--generator.currentRoom;
                //generator.finalRoomInfoArray[generator.currentRoom].comingFromEntrance = false;
                //generator.Reset();
                iHopeThisWorks = false;
            }
        }
    }
}
