using UnityEngine;
using System.Collections;

public class RoomGeneration : MonoBehaviour
{
    GameObject player;
    public GameObject[] floorOneRooms;
    Room[] floorOneRoomsInfo;
    public GameObject[] floorOneMazes;
    Room[] floorOneMazesInfo;
    public GameObject dethrosRoom;
    Room dethrosRoomInfo;
    public GameObject[] floorTwoRooms;
    Room[] floorTwoRoomsInfo;
    public GameObject[] floorTwoMazes;
    Room[] floorTwoMazesInfo;
    public GameObject lorneRoom;
    Room lorneRoomInfo;
    public GameObject[] floorThreeRooms;
    Room[] floorThreeRoomsInfo;
    public GameObject[] floorThreeMazes;
    Room[] floorThreeMazesInfo;
    public GameObject morriusRoom;
    Room morriusRoomInfo;
    public GameObject[] finalRoomArray;
    [HideInInspector]
    public Room[] finalRoomInfoArray;
    //public int currentFloor = 1;
    public int currentRoom = 0;
    //public GameObject treasureRoom;
    //Room treasureRoomInfo;
    //public GameObject hazard;
    //public GameObject nDoor;
    //public GameObject sDoor;
    //public GameObject eDoor;
    //public GameObject wDoor;
    public GameObject waypoint;
    public GameObject chest;
    public GameObject torchWood;
    public GameObject CPCrystal;
    public SaveTest savenload;
    public GameObject[] checkpointRooms;
    Room[] checkpointRoomsInfo;
    PlayerMovement movement;

    bool easyMode;
    //int enemyMod;
    int prevRoom = -1;
    bool loading = false;
    //bool firstRoom;

    public PlayerData loadedData;

    void Start()
    {
        savenload = GameObject.FindObjectOfType<Options>().GetComponent<SaveTest>();
        //firstRoom = true;
        //DontDestroyOnLoad(this);
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();

        loading = GameObject.FindObjectOfType<Options>().shouldload;
        if (loading)
        {
            savenload.LoadDungeon();
            easyMode = loadedData.easymode;
        }
        else easyMode = GameObject.FindObjectOfType<Options>().easyMode;

        if (!loading)
            Utilities.ArrayShuffle(floorOneRooms);
        floorOneRoomsInfo = new Room[floorOneRooms.Length];
        for (int i = 0; i < floorOneRooms.Length; i++)
        {
            floorOneRoomsInfo[i] = floorOneRooms[i].GetComponent<Room>();
            floorOneRoomsInfo[i].setUsed();
        }
        if (!loading)
            Utilities.ArrayShuffle(floorOneMazes, 100);
        floorOneMazesInfo = new Room[floorOneMazes.Length];
        for (int i = 0; i < floorOneMazes.Length; i++)
        {
            floorOneMazesInfo[i] = floorOneMazes[i].GetComponent<Room>();
            floorOneMazesInfo[i].setUsed();
        }
        dethrosRoomInfo = dethrosRoom.GetComponent<Room>();
        dethrosRoomInfo.setUsed();
        if (!loading)
            Utilities.ArrayShuffle(floorTwoRooms);
        floorTwoRoomsInfo = new Room[floorTwoRooms.Length];
        for (int i = 0; i < floorTwoRooms.Length; i++)
        {
            floorTwoRoomsInfo[i] = floorTwoRooms[i].GetComponent<Room>();
            floorTwoRoomsInfo[i].setUsed();
        }
        if (!loading)
            Utilities.ArrayShuffle(floorTwoMazes);
        floorTwoMazesInfo = new Room[floorTwoMazes.Length];
        for (int i = 0; i < floorTwoMazes.Length; i++)
        {
            floorTwoMazesInfo[i] = floorTwoMazes[i].GetComponent<Room>();
            floorTwoMazesInfo[i].setUsed();
        }
        lorneRoomInfo = lorneRoom.GetComponent<Room>();
        lorneRoomInfo.setUsed();
        if (!loading)
            Utilities.ArrayShuffle(floorThreeRooms);
        floorThreeRoomsInfo = new Room[floorThreeRooms.Length];
        for (int i = 0; i < floorThreeRooms.Length; i++)
        {
            floorThreeRoomsInfo[i] = floorThreeRooms[i].GetComponent<Room>();
            floorThreeRoomsInfo[i].setUsed();
        }
        if (!loading)
            Utilities.ArrayShuffle(floorThreeMazes);
        floorThreeMazesInfo = new Room[floorThreeMazes.Length];
        for (int i = 0; i < floorThreeMazes.Length; i++)
        {
            floorThreeMazesInfo[i] = floorThreeMazes[i].GetComponent<Room>();
            floorThreeMazesInfo[i].setUsed();
        }
        morriusRoomInfo = morriusRoom.GetComponent<Room>();
        morriusRoomInfo.setUsed();

        //treasureRoomInfo = treasureRoom.GetComponent<Room>();

        if (easyMode)
        {
            checkpointRoomsInfo = new Room[checkpointRooms.Length];
            for (int i = 0; i < checkpointRooms.Length; i++)
            {
                checkpointRoomsInfo[i] = checkpointRooms[i].GetComponent<Room>();
                checkpointRoomsInfo[i].setUsed();
            }
        }


        finalRoomArray = new GameObject[easyMode ? 33 : 27];
        finalRoomInfoArray = new Room[easyMode ? 33 : 27];
        if (!loading)
            FillDungeon();
        else
        {
            FillLoadedDungeon();
        }

        //TESTING
        //finalRoomArray[0] = floorOneRooms[8];
        //finalRoomInfoArray[0] = floorOneRoomsInfo[8];
        //ENDTESTING

        CreateRoom();
        //firstRoom = false;
        //Reset();
        //enemyMod = (easyMode ? 11 : 9);
    }

    void CreateRoom()
    {
        bool skip = false;
        bool hasSkipped = false;

        //if (prevRoom > currentRoom)
        //    ++enemyMod;
        //else
        //    --enemyMod;
        //
        //if (enemyMod <= 0)
        //{
        //    enemyMod = (easyMode ? 11 : 9);
        //}

        // Spawn torches in the checkpoints
        if (easyMode && (currentRoom == 3 || currentRoom == 7 || currentRoom == 14 || currentRoom == 18 || currentRoom == 25 || currentRoom == 29))
        {
            Instantiate(torchWood, new Vector3(1, -1, -.8f), Quaternion.identity);
            Instantiate(torchWood, new Vector3(1, -8, -.8f), Quaternion.identity);
            Instantiate(torchWood, new Vector3(16, -8, -.8f), Quaternion.identity);
            Instantiate(torchWood, new Vector3(16, -1, -.8f), Quaternion.identity);
            Instantiate(CPCrystal, new Vector3(8.5f, -4.5f, -.8f), Quaternion.identity);
        }

        if (finalRoomInfoArray[currentRoom].entryText != null)
        {
            //finalRoomInfoArray[currentRoom].entryText.SetActive(true);
            //finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().enabled = true;
            switch (currentRoom)
            {
                case 0:
                    finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Forest-";
                    Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    GameObject.Find("TheOptions").GetComponent<Options>().CompleteTutorial();
                    GameObject.Find("TheOptions").GetComponent<Options>().StartAttempt();
                    break;
                case 3:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Checkpoint-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 7:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Checkpoint-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 8:
                    if (!easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Dethros-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 9:
                    if (!easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Cave-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 10:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Dethros-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 11:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Cave-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 14:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Checkpoint-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 18:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Checkpoint-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    else
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Dungeon-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 22:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Dungeon-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 25:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Checkpoint-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 26:
                    if (!easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Morrius-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 29:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Checkpoint-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                case 32:
                    if (easyMode)
                    {
                        finalRoomInfoArray[currentRoom].entryText.GetComponent<RoomEntryText>().toDisplay = "-Morrius-";
                        Instantiate(finalRoomInfoArray[currentRoom].entryText);
                    }
                    break;
                default:
                    break;
            }
        }

        // Spawn north wall and possibly door
        for (int i = 0; i < finalRoomInfoArray[currentRoom].width; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 2 || finalRoomInfoArray[currentRoom].exitDir == 2) && i == finalRoomInfoArray[currentRoom].width / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].NorthDoor, new Vector3((float)i + .5f, 0, -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, 0, 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(i, 0, -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, 0, 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn south wall and possibly door
        skip = false;
        hasSkipped = false;
        for (int i = 0; i < finalRoomInfoArray[currentRoom].width; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 0 || finalRoomInfoArray[currentRoom].exitDir == 0) && i == finalRoomInfoArray[currentRoom].width / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].SouthDoor, new Vector3((float)i + .5f, -(finalRoomInfoArray[currentRoom].height - 1), -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, -(finalRoomInfoArray[currentRoom].height - 1), 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(i, -(finalRoomInfoArray[currentRoom].height - 1), -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(i, -(finalRoomInfoArray[currentRoom].height - 1), 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn west wall and possibly door
        skip = false;
        hasSkipped = false;
        for (int i = 0; i < finalRoomInfoArray[currentRoom].height; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 1 || finalRoomInfoArray[currentRoom].exitDir == 1) && i == finalRoomInfoArray[currentRoom].height / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].WestDoor, new Vector3(0, -((float)i + .5f), -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(0, -i, 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(0, -i, -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(0, -i, 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn east wall and possibly door
        skip = false;
        hasSkipped = false;
        for (int i = 0; i < finalRoomInfoArray[currentRoom].height; i++)
        {
            if ((finalRoomInfoArray[currentRoom].entranceDir == 3 || finalRoomInfoArray[currentRoom].exitDir == 3) && i == finalRoomInfoArray[currentRoom].height / 2 - 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].EastDoor, new Vector3(finalRoomInfoArray[currentRoom].width - 1, -((float)i + .5f), -1.4f), Quaternion.identity);
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(finalRoomInfoArray[currentRoom].width - 1, -i, 0.0f), Quaternion.identity);
                skip = true;
            }
            else if (!skip)
            {
                Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(finalRoomInfoArray[currentRoom].width - 1, -i, -1.4f), Quaternion.identity);
            }
            if (hasSkipped)
            {
                skip = false;
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(finalRoomInfoArray[currentRoom].width - 1, -i, 0.0f), Quaternion.identity);
            }
            if (skip)
            {
                hasSkipped = true;
            }
        }
        // Spawn the floor
        for (int x = 1; x < finalRoomInfoArray[currentRoom].width - 1; ++x)
        {
            for (int y = 1; y < finalRoomInfoArray[currentRoom].height - 1; ++y)
            {
                Instantiate(finalRoomInfoArray[currentRoom].floorTiles[0], new Vector3(x, -y, 0.0f), Quaternion.identity);
            }
        }
        //
        for (int i = 0; i < finalRoomInfoArray[currentRoom].innerWallPositions.Length; i++)
        {
            Instantiate(finalRoomInfoArray[currentRoom].wallTiles[0], new Vector3(finalRoomInfoArray[currentRoom].innerWallPositions[i].x, -finalRoomInfoArray[currentRoom].innerWallPositions[i].y, -1.4f), Quaternion.identity);
        }
        for (int i = 0; i < finalRoomInfoArray[currentRoom].hazardSpawnPoints.Length; i++)
        {
            float z = -1;
            if (currentRoom > (easyMode ? 10 : 8) && currentRoom < (easyMode ? 22 : 18))
            {
                z = -.5f;
            }
            Instantiate(finalRoomInfoArray[currentRoom].hazard, new Vector3(finalRoomInfoArray[currentRoom].hazardSpawnPoints[i].x, -finalRoomInfoArray[currentRoom].hazardSpawnPoints[i].y, z), Quaternion.identity);
        }
        // Spawn enemies & possibly a chest
        if (!finalRoomInfoArray[currentRoom].beenThere)
        {
            Utilities.ArrayShuffle(finalRoomInfoArray[currentRoom].enemySpawnPoints);
            int min = finalRoomInfoArray[currentRoom].minEnemies;
            //if (min - enemyMod > 0)
            //{
            //    min -= enemyMod;
            //}
            int max = finalRoomInfoArray[currentRoom].maxEnemies;
            if (max + (currentRoom % (easyMode ? 11 : 9)) > finalRoomInfoArray[currentRoom].enemySpawnPoints.Length)
                max = finalRoomInfoArray[currentRoom].enemySpawnPoints.Length;
            else
                max += (currentRoom % (easyMode ? 11 : 9));
            //if (max - enemyMod > min)
            //{
            //    max -= enemyMod;
            //}
            finalRoomInfoArray[currentRoom].numEnemies = Random.Range(min, max);
            int enemiesSpawned = 0;
            if (finalRoomInfoArray[currentRoom].enemiesThatCanSpawn.Length > 0)
            {
                Instantiate(finalRoomInfoArray[currentRoom].enemiesThatCanSpawn[0], new Vector3(finalRoomInfoArray[currentRoom].enemySpawnPoints[0].x, -finalRoomInfoArray[currentRoom].enemySpawnPoints[0].y, -1f), Quaternion.identity);
                finalRoomInfoArray[currentRoom].enemySpawnPointUsed[0] = true;
                enemiesSpawned = 1;
            }
            if (finalRoomInfoArray[currentRoom].enemiesThatCanSpawn.Length > 1)
            {
                Instantiate(finalRoomInfoArray[currentRoom].enemiesThatCanSpawn[1], new Vector3(finalRoomInfoArray[currentRoom].enemySpawnPoints[1].x, -finalRoomInfoArray[currentRoom].enemySpawnPoints[1].y, -1f), Quaternion.identity);
                finalRoomInfoArray[currentRoom].enemySpawnPointUsed[1] = true;
                enemiesSpawned = 2;
            }
            while (enemiesSpawned < finalRoomInfoArray[currentRoom].numEnemies)
            {
                for (int i = 2; i < finalRoomInfoArray[currentRoom].enemySpawnPoints.Length; i++)
                {
                    int chance = Random.Range(1, 5);
                    if (!finalRoomInfoArray[currentRoom].enemySpawnPointUsed[i] && chance == 1)
                    {
                        Instantiate(finalRoomInfoArray[currentRoom].enemiesThatCanSpawn[Random.Range(2, finalRoomInfoArray[currentRoom].enemiesThatCanSpawn.Length)], new Vector3(finalRoomInfoArray[currentRoom].enemySpawnPoints[i].x, -finalRoomInfoArray[currentRoom].enemySpawnPoints[i].y, -1f), Quaternion.identity);
                        finalRoomInfoArray[currentRoom].enemySpawnPointUsed[i] = true;
                        ++enemiesSpawned;
                        if (enemiesSpawned == finalRoomInfoArray[currentRoom].numEnemies)
                            break;
                    }
                }
            }
            float chanceychance = Random.Range(0f, 1f);
            if (currentRoom == (easyMode ? 10 : 8) || currentRoom == (easyMode ? 21 : 17) || currentRoom == (easyMode ? 32 : 26))
            {
                Instantiate(chest, new Vector3(finalRoomInfoArray[currentRoom].chestSpawnLocations[0].x,
                    finalRoomInfoArray[currentRoom].chestSpawnLocations[0].y,
                    1.0f), Quaternion.identity);
            }
            else if (chanceychance <= .3f)
            {
                bool spawnedAChest = false;
                while (!spawnedAChest)
                {
                    for (int i = 0; i < finalRoomInfoArray[currentRoom].chestSpawnLocations.Length; i++)
                    {
                        int c = Random.Range(1, 5);
                        if (c == 1)
                        {
                            Instantiate(chest, new Vector3(finalRoomInfoArray[currentRoom].chestSpawnLocations[i].x,
                                -finalRoomInfoArray[currentRoom].chestSpawnLocations[i].y, -.9f),// Quaternion.identity);
                                Quaternion.Euler(0.0f, 0.0f, finalRoomInfoArray[currentRoom].chestRotations[i]));
                            spawnedAChest = true;
                            break;
                        }
                    }
                }
            }

        }
        //Spawn Waypoints
        for (int i = 0; i < finalRoomInfoArray[currentRoom].waypointLocations.Length; i++)
        {
            Instantiate(waypoint, new Vector3(finalRoomInfoArray[currentRoom].waypointLocations[i].x, -finalRoomInfoArray[currentRoom].waypointLocations[i].y, -1.0f), Quaternion.identity);
        }
        // Move player to entrance or exit
        if (finalRoomInfoArray[currentRoom].comingFromEntrance)
        {
            switch (finalRoomInfoArray[currentRoom].entranceDir)
            {
                case 0:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.x, -(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.y + (!movement.transitioning ? 0f : 2f)), -1.0f);
                    break;
                case 1:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].leftPlayerSpawn.x - (!movement.transitioning ? 0f : 2f), -finalRoomInfoArray[currentRoom].leftPlayerSpawn.y, -1.0f);
                    break;
                case 2:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].topPlayerSpawn.x, -(finalRoomInfoArray[currentRoom].topPlayerSpawn.y - (!movement.transitioning ? 0f : 2f)), -1.0f);
                    break;
                case 3:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].rightPlayerSpawn.x + (!movement.transitioning ? 0f : 2f), -finalRoomInfoArray[currentRoom].rightPlayerSpawn.y, -1.0f);
                    break;
                default:
                    break;
            }
        }
        else if (!finalRoomInfoArray[currentRoom].comingFromEntrance)
        {
            switch (finalRoomInfoArray[currentRoom].exitDir)
            {
                case 0:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.x, -(finalRoomInfoArray[currentRoom].bottomPlayerSpawn.y + (!movement.transitioning ? 0f : 2f)), -1.0f);
                    break;
                case 1:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].leftPlayerSpawn.x - (!movement.transitioning ? 0f : 2f), -finalRoomInfoArray[currentRoom].leftPlayerSpawn.y, -1.0f);
                    break;
                case 2:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].topPlayerSpawn.x, -(finalRoomInfoArray[currentRoom].topPlayerSpawn.y - (!movement.transitioning ? 0f : 2f)), -1.0f);
                    break;
                case 3:
                    player.transform.position = new Vector3(finalRoomInfoArray[currentRoom].rightPlayerSpawn.x + (!movement.transitioning ? 0f : 2f), -finalRoomInfoArray[currentRoom].rightPlayerSpawn.y, -1.0f);
                    break;
                default:
                    break;
            }
        }
        // make sure enemies can't respawn
        finalRoomInfoArray[currentRoom].beenThere = true;
        prevRoom = currentRoom;
        //firstRoom = false;
    }

    void FillDungeon()
    {
        finalRoomArray[0] = floorOneRooms[0];
        finalRoomInfoArray[0] = floorOneRoomsInfo[0];
        finalRoomInfoArray[0].entranceDir = Random.Range(0, 4);
        do
        {
            finalRoomInfoArray[0].exitDir = Random.Range(0, 4);
        } while (finalRoomInfoArray[0].exitDir == finalRoomInfoArray[0].entranceDir);
        finalRoomArray[1] = floorOneRooms[1];
        finalRoomInfoArray[1] = floorOneRoomsInfo[1];
        switch (finalRoomInfoArray[0].exitDir)
        {
            case 0:
                finalRoomInfoArray[1].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[1].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[1].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[1].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[1].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[1].exitDir == finalRoomInfoArray[1].entranceDir);
        finalRoomArray[2] = floorOneMazes[0];
        finalRoomInfoArray[2] = floorOneMazesInfo[0];
        switch (finalRoomInfoArray[1].exitDir)
        {
            case 0:
                finalRoomInfoArray[2].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[2].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[2].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[2].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[2].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[2].exitDir == finalRoomInfoArray[2].entranceDir);
        finalRoomArray[3] = easyMode ? checkpointRooms[0] : floorOneRooms[2];
        finalRoomInfoArray[3] = easyMode ? checkpointRoomsInfo[0] : floorOneRoomsInfo[2];
        switch (finalRoomInfoArray[2].exitDir)
        {
            case 0:
                finalRoomInfoArray[3].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[3].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[3].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[3].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[3].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[3].exitDir == finalRoomInfoArray[3].entranceDir);
        finalRoomArray[4] = easyMode ? floorOneRooms[2] : floorOneRooms[3];
        finalRoomInfoArray[4] = easyMode ? floorOneRoomsInfo[2] : floorOneRoomsInfo[3];
        switch (finalRoomInfoArray[3].exitDir)
        {
            case 0:
                finalRoomInfoArray[4].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[4].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[4].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[4].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[4].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[4].exitDir == finalRoomInfoArray[4].entranceDir);
        finalRoomArray[5] = easyMode ? floorOneRooms[3] : floorOneMazes[1];
        finalRoomInfoArray[5] = easyMode ? floorOneRoomsInfo[3] : floorOneMazesInfo[1];
        switch (finalRoomInfoArray[4].exitDir)
        {
            case 0:
                finalRoomInfoArray[5].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[5].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[5].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[5].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[5].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[5].exitDir == finalRoomInfoArray[5].entranceDir);
        finalRoomArray[6] = easyMode ? floorOneMazes[1] : floorOneRooms[4];
        finalRoomInfoArray[6] = easyMode ? floorOneMazesInfo[1] : floorOneRoomsInfo[4];
        switch (finalRoomInfoArray[5].exitDir)
        {
            case 0:
                finalRoomInfoArray[6].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[6].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[6].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[6].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[6].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[6].exitDir == finalRoomInfoArray[6].entranceDir);
        finalRoomArray[7] = easyMode ? checkpointRooms[1] : floorOneRooms[5];
        finalRoomInfoArray[7] = easyMode ? checkpointRoomsInfo[1] : floorOneRoomsInfo[5];
        switch (finalRoomInfoArray[6].exitDir)
        {
            case 0:
                finalRoomInfoArray[7].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[7].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[7].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[7].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[7].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[7].exitDir == finalRoomInfoArray[7].entranceDir);
        finalRoomArray[8] = easyMode ? floorOneRooms[4] : dethrosRoom;
        finalRoomInfoArray[8] = easyMode ? floorOneRoomsInfo[4] : dethrosRoomInfo;
        switch (finalRoomInfoArray[7].exitDir)
        {
            case 0:
                finalRoomInfoArray[8].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[8].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[8].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[8].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[8].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[8].exitDir == finalRoomInfoArray[8].entranceDir);
        finalRoomArray[9] = easyMode ? floorOneRooms[5] : floorTwoRooms[0];
        finalRoomInfoArray[9] = easyMode ? floorOneRoomsInfo[5] : floorTwoRoomsInfo[0];
        switch (finalRoomInfoArray[8].exitDir)
        {
            case 0:
                finalRoomInfoArray[9].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[9].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[9].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[9].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[9].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[9].exitDir == finalRoomInfoArray[9].entranceDir);
        finalRoomArray[10] = easyMode ? dethrosRoom : floorTwoRooms[1];
        finalRoomInfoArray[10] = easyMode ? dethrosRoomInfo : floorTwoRoomsInfo[1];
        switch (finalRoomInfoArray[9].exitDir)
        {
            case 0:
                finalRoomInfoArray[10].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[10].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[10].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[10].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[10].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[10].exitDir == finalRoomInfoArray[10].entranceDir);
        finalRoomArray[11] = easyMode ? floorTwoRooms[0] : floorTwoMazes[0];
        finalRoomInfoArray[11] = easyMode ? floorTwoRoomsInfo[0] : floorTwoMazesInfo[0];
        switch (finalRoomInfoArray[10].exitDir)
        {
            case 0:
                finalRoomInfoArray[11].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[11].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[11].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[11].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[11].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[11].exitDir == finalRoomInfoArray[11].entranceDir);
        finalRoomArray[12] = easyMode ? floorTwoRooms[1] : floorTwoRooms[2];
        finalRoomInfoArray[12] = easyMode ? floorTwoRoomsInfo[1] : floorTwoRoomsInfo[2];
        switch (finalRoomInfoArray[11].exitDir)
        {
            case 0:
                finalRoomInfoArray[12].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[12].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[12].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[12].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[12].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[12].exitDir == finalRoomInfoArray[12].entranceDir);
        finalRoomArray[13] = easyMode ? floorTwoMazes[0] : floorTwoRooms[3];
        finalRoomInfoArray[13] = easyMode ? floorTwoMazesInfo[0] : floorTwoRoomsInfo[3];
        switch (finalRoomInfoArray[12].exitDir)
        {
            case 0:
                finalRoomInfoArray[13].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[13].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[13].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[13].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[13].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[13].exitDir == finalRoomInfoArray[13].entranceDir);
        finalRoomArray[14] = easyMode ? checkpointRooms[2] : floorTwoMazes[1];
        finalRoomInfoArray[14] = easyMode ? checkpointRoomsInfo[2] : floorTwoMazesInfo[1];
        switch (finalRoomInfoArray[13].exitDir)
        {
            case 0:
                finalRoomInfoArray[14].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[14].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[14].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[14].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[14].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[14].exitDir == finalRoomInfoArray[14].entranceDir);
        finalRoomArray[15] = easyMode ? floorTwoRooms[2] : floorTwoRooms[4];
        finalRoomInfoArray[15] = easyMode ? floorTwoRoomsInfo[2] : floorTwoRoomsInfo[4];
        switch (finalRoomInfoArray[14].exitDir)
        {
            case 0:
                finalRoomInfoArray[15].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[15].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[15].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[15].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[15].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[15].exitDir == finalRoomInfoArray[15].entranceDir);
        finalRoomArray[16] = easyMode ? floorTwoRooms[3] : floorTwoRooms[5];
        finalRoomInfoArray[16] = easyMode ? floorTwoRoomsInfo[3] : floorTwoRoomsInfo[5];
        switch (finalRoomInfoArray[15].exitDir)
        {
            case 0:
                finalRoomInfoArray[16].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[16].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[16].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[16].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[16].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[16].exitDir == finalRoomInfoArray[16].entranceDir);
        finalRoomArray[17] = easyMode ? floorTwoMazes[1] : lorneRoom;
        finalRoomInfoArray[17] = easyMode ? floorTwoMazesInfo[1] : lorneRoomInfo;
        switch (finalRoomInfoArray[16].exitDir)
        {
            case 0:
                finalRoomInfoArray[17].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[17].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[17].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[17].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[17].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[17].exitDir == finalRoomInfoArray[17].entranceDir);
        finalRoomArray[18] = easyMode ? checkpointRooms[3] : floorThreeRooms[0];
        finalRoomInfoArray[18] = easyMode ? checkpointRoomsInfo[3] : floorThreeRoomsInfo[0];
        switch (finalRoomInfoArray[17].exitDir)
        {
            case 0:
                finalRoomInfoArray[18].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[18].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[18].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[18].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[18].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[18].exitDir == finalRoomInfoArray[18].entranceDir);
        finalRoomArray[19] = easyMode ? floorTwoRooms[4] : floorThreeRooms[1];
        finalRoomInfoArray[19] = easyMode ? floorTwoRoomsInfo[4] : floorThreeRoomsInfo[1];
        switch (finalRoomInfoArray[18].exitDir)
        {
            case 0:
                finalRoomInfoArray[19].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[19].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[19].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[19].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[19].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[19].exitDir == finalRoomInfoArray[19].entranceDir);
        finalRoomArray[20] = easyMode ? floorTwoRooms[5] : floorThreeMazes[0];
        finalRoomInfoArray[20] = easyMode ? floorTwoRoomsInfo[5] : floorThreeMazesInfo[0];
        switch (finalRoomInfoArray[19].exitDir)
        {
            case 0:
                finalRoomInfoArray[20].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[20].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[20].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[20].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[20].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[20].exitDir == finalRoomInfoArray[20].entranceDir);
        finalRoomArray[21] = easyMode ? lorneRoom : floorThreeRooms[2];
        finalRoomInfoArray[21] = easyMode ? lorneRoomInfo : floorThreeRoomsInfo[2];
        switch (finalRoomInfoArray[20].exitDir)
        {
            case 0:
                finalRoomInfoArray[21].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[21].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[21].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[21].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[21].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[21].exitDir == finalRoomInfoArray[21].entranceDir);
        finalRoomArray[22] = easyMode ? floorThreeRooms[0] : floorThreeRooms[3];
        finalRoomInfoArray[22] = easyMode ? floorThreeRoomsInfo[0] : floorThreeRoomsInfo[3];
        switch (finalRoomInfoArray[21].exitDir)
        {
            case 0:
                finalRoomInfoArray[22].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[22].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[22].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[22].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[22].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[22].exitDir == finalRoomInfoArray[22].entranceDir);
        finalRoomArray[23] = easyMode ? floorThreeRooms[1] : floorThreeMazes[1];
        finalRoomInfoArray[23] = easyMode ? floorThreeRoomsInfo[1] : floorThreeMazesInfo[1];
        switch (finalRoomInfoArray[22].exitDir)
        {
            case 0:
                finalRoomInfoArray[23].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[23].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[23].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[23].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[23].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[23].exitDir == finalRoomInfoArray[23].entranceDir);
        finalRoomArray[24] = easyMode ? floorThreeMazes[0] : floorThreeRooms[4];
        finalRoomInfoArray[24] = easyMode ? floorThreeMazesInfo[0] : floorThreeRoomsInfo[4];
        switch (finalRoomInfoArray[23].exitDir)
        {
            case 0:
                finalRoomInfoArray[24].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[24].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[24].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[24].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[24].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[24].exitDir == finalRoomInfoArray[24].entranceDir);
        finalRoomArray[25] = easyMode ? checkpointRooms[4] : floorThreeRooms[5];
        finalRoomInfoArray[25] = easyMode ? checkpointRoomsInfo[4] : floorThreeRoomsInfo[5];
        switch (finalRoomInfoArray[24].exitDir)
        {
            case 0:
                finalRoomInfoArray[25].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[25].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[25].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[25].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[25].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[25].exitDir == finalRoomInfoArray[25].entranceDir);
        finalRoomArray[26] = easyMode ? floorThreeRooms[2] : morriusRoom;
        finalRoomInfoArray[26] = easyMode ? floorThreeRoomsInfo[2] : morriusRoomInfo;
        switch (finalRoomInfoArray[25].exitDir)
        {
            case 0:
                finalRoomInfoArray[26].entranceDir = 2;
                break;
            case 1:
                finalRoomInfoArray[26].entranceDir = 3;
                break;
            case 2:
                finalRoomInfoArray[26].entranceDir = 0;
                break;
            case 3:
                finalRoomInfoArray[26].entranceDir = 1;
                break;
            default:
                break;
        }
        do
        {
            finalRoomInfoArray[26].exitDir = Random.Range(0, 3);
        } while (finalRoomInfoArray[26].exitDir == finalRoomInfoArray[26].entranceDir);
        //
        if (easyMode)
        {
            finalRoomArray[27] = floorThreeRooms[3];
            finalRoomInfoArray[27] = floorThreeRoomsInfo[3];
            switch (finalRoomInfoArray[26].exitDir)
            {
                case 0:
                    finalRoomInfoArray[27].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[27].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[27].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[27].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[27].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[27].exitDir == finalRoomInfoArray[27].entranceDir);
            finalRoomArray[28] = floorThreeMazes[1];
            finalRoomInfoArray[28] = floorThreeMazesInfo[1];
            switch (finalRoomInfoArray[27].exitDir)
            {
                case 0:
                    finalRoomInfoArray[28].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[28].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[28].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[28].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[28].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[28].exitDir == finalRoomInfoArray[28].entranceDir);
            finalRoomArray[29] = checkpointRooms[5];
            finalRoomInfoArray[29] = checkpointRoomsInfo[5];
            switch (finalRoomInfoArray[28].exitDir)
            {
                case 0:
                    finalRoomInfoArray[29].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[29].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[29].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[29].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[29].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[29].exitDir == finalRoomInfoArray[29].entranceDir);
            finalRoomArray[30] = floorThreeRooms[4];
            finalRoomInfoArray[30] = floorThreeRoomsInfo[4];
            switch (finalRoomInfoArray[29].exitDir)
            {
                case 0:
                    finalRoomInfoArray[30].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[30].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[30].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[30].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[30].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[30].exitDir == finalRoomInfoArray[30].entranceDir);
            finalRoomArray[31] = floorThreeRooms[5];
            finalRoomInfoArray[31] = floorThreeRoomsInfo[5];
            switch (finalRoomInfoArray[30].exitDir)
            {
                case 0:
                    finalRoomInfoArray[31].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[31].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[31].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[31].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[31].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[31].exitDir == finalRoomInfoArray[31].entranceDir);
            finalRoomArray[32] = morriusRoom;
            finalRoomInfoArray[32] = morriusRoomInfo;
            switch (finalRoomInfoArray[31].exitDir)
            {
                case 0:
                    finalRoomInfoArray[32].entranceDir = 2;
                    break;
                case 1:
                    finalRoomInfoArray[32].entranceDir = 3;
                    break;
                case 2:
                    finalRoomInfoArray[32].entranceDir = 0;
                    break;
                case 3:
                    finalRoomInfoArray[32].entranceDir = 1;
                    break;
                default:
                    break;
            }
            do
            {
                finalRoomInfoArray[32].exitDir = Random.Range(0, 3);
            } while (finalRoomInfoArray[32].exitDir == finalRoomInfoArray[32].entranceDir);
        }
        //
        for (int i = 0; i < finalRoomInfoArray.Length; i++)
        {
            finalRoomInfoArray[i].comingFromEntrance = true;
        }
    }

    void FillLoadedDungeon()
    {
        if (easyMode)
        {
            finalRoomArray[3] = checkpointRooms[0];
            finalRoomInfoArray[3] = checkpointRoomsInfo[0];
            finalRoomArray[7] = checkpointRooms[1];
            finalRoomInfoArray[7] = checkpointRoomsInfo[1];
            finalRoomArray[10] = dethrosRoom;
            finalRoomInfoArray[10] = dethrosRoomInfo;
            finalRoomArray[14] = checkpointRooms[2];
            finalRoomInfoArray[14] = checkpointRoomsInfo[2];
            finalRoomArray[18] = checkpointRooms[3];
            finalRoomInfoArray[18] = checkpointRoomsInfo[3];
            finalRoomArray[21] = lorneRoom;
            finalRoomInfoArray[21] = lorneRoomInfo;
            finalRoomArray[25] = checkpointRooms[4];
            finalRoomInfoArray[25] = checkpointRoomsInfo[4];
            finalRoomArray[29] = checkpointRooms[5];
            finalRoomInfoArray[29] = checkpointRoomsInfo[5];
            finalRoomArray[32] = morriusRoom;
            finalRoomInfoArray[32] = morriusRoomInfo;
        }
        else
        {
            finalRoomArray[8] = dethrosRoom;
            finalRoomInfoArray[8] = dethrosRoomInfo;
            finalRoomArray[17] = lorneRoom;
            finalRoomInfoArray[17] = lorneRoomInfo;
            finalRoomArray[26] = morriusRoom;
            finalRoomInfoArray[26] = morriusRoomInfo;
        }
        for (int i = 0; i < finalRoomArray.Length; i++)
        {
            switch (loadedData.roominfo[i].roomID)
            {
                case 1:
                    finalRoomArray[i] = floorOneRooms[0];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[0];
                    break;
                case 2:
                    finalRoomArray[i] = floorOneRooms[1];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[1];
                    break;
                case 3:
                    finalRoomArray[i] = floorOneRooms[2];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[2];
                    break;
                case 4:
                    finalRoomArray[i] = floorOneRooms[3];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[3];
                    break;
                case 5:
                    finalRoomArray[i] = floorOneRooms[4];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[4];
                    break;
                case 6:
                    finalRoomArray[i] = floorOneRooms[5];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[5];
                    break;
                case 7:
                    finalRoomArray[i] = floorOneRooms[6];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[6];
                    break;
                case 8:
                    finalRoomArray[i] = floorOneRooms[7];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[7];
                    break;
                case 9:
                    finalRoomArray[i] = floorOneRooms[8];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[8];
                    break;
                case 10:
                    finalRoomArray[i] = floorOneRooms[9];
                    finalRoomInfoArray[i] = floorOneRoomsInfo[9];
                    break;
                case 11:
                    finalRoomArray[i] = floorOneMazes[0];
                    finalRoomInfoArray[i] = floorOneMazesInfo[0];
                    break;
                case 12:
                    finalRoomArray[i] = floorOneMazes[1];
                    finalRoomInfoArray[i] = floorOneMazesInfo[1];
                    break;
                case 13:
                    finalRoomArray[i] = floorOneMazes[2];
                    finalRoomInfoArray[i] = floorOneMazesInfo[2];
                    break;
                case 14:
                    finalRoomArray[i] = floorOneMazes[3];
                    finalRoomInfoArray[i] = floorOneMazesInfo[3];
                    break;
                case 15:
                    finalRoomArray[i] = floorTwoRooms[0];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[0];
                    break;
                case 16:
                    finalRoomArray[i] = floorTwoRooms[1];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[1];
                    break;
                case 17:
                    finalRoomArray[i] = floorTwoRooms[2];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[2];
                    break;
                case 18:
                    finalRoomArray[i] = floorTwoRooms[3];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[3];
                    break;
                case 19:
                    finalRoomArray[i] = floorTwoRooms[4];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[4];
                    break;
                case 20:
                    finalRoomArray[i] = floorTwoRooms[5];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[5];
                    break;
                case 21:
                    finalRoomArray[i] = floorTwoRooms[6];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[6];
                    break;
                case 22:
                    finalRoomArray[i] = floorTwoRooms[7];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[7];
                    break;
                case 23:
                    finalRoomArray[i] = floorTwoRooms[8];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[8];
                    break;
                case 24:
                    finalRoomArray[i] = floorTwoRooms[9];
                    finalRoomInfoArray[i] = floorTwoRoomsInfo[9];
                    break;
                case 25:
                    finalRoomArray[i] = floorTwoMazes[0];
                    finalRoomInfoArray[i] = floorTwoMazesInfo[0];
                    break;
                case 26:
                    finalRoomArray[i] = floorTwoMazes[1];
                    finalRoomInfoArray[i] = floorTwoMazesInfo[1];
                    break;
                case 27:
                    finalRoomArray[i] = floorTwoMazes[2];
                    finalRoomInfoArray[i] = floorTwoMazesInfo[2];
                    break;
                case 28:
                    finalRoomArray[i] = floorTwoMazes[3];
                    finalRoomInfoArray[i] = floorTwoMazesInfo[3];
                    break;
                case 29:
                    finalRoomArray[i] = floorThreeRooms[0];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[0];
                    break;
                case 30:
                    finalRoomArray[i] = floorThreeRooms[1];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[1];
                    break;
                case 31:
                    finalRoomArray[i] = floorThreeRooms[2];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[2];
                    break;
                case 32:
                    finalRoomArray[i] = floorThreeRooms[3];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[3];
                    break;
                case 33:
                    finalRoomArray[i] = floorThreeRooms[4];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[4];
                    break;
                case 34:
                    finalRoomArray[i] = floorThreeRooms[5];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[5];
                    break;
                case 35:
                    finalRoomArray[i] = floorThreeRooms[6];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[6];
                    break;
                case 36:
                    finalRoomArray[i] = floorThreeRooms[7];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[7];
                    break;
                case 37:
                    finalRoomArray[i] = floorThreeRooms[8];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[8];
                    break;
                case 38:
                    finalRoomArray[i] = floorThreeRooms[9];
                    finalRoomInfoArray[i] = floorThreeRoomsInfo[9];
                    break;
                case 39:
                    finalRoomArray[i] = floorThreeMazes[0];
                    finalRoomInfoArray[i] = floorThreeMazesInfo[0];
                    break;
                case 40:
                    finalRoomArray[i] = floorThreeMazes[1];
                    finalRoomInfoArray[i] = floorThreeMazesInfo[1];
                    break;
                case 41:
                    finalRoomArray[i] = floorThreeMazes[2];
                    finalRoomInfoArray[i] = floorThreeMazesInfo[2];
                    break;
                case 42:
                    finalRoomArray[i] = floorThreeMazes[3];
                    finalRoomInfoArray[i] = floorThreeMazesInfo[3];
                    break;
                default:
                    break;
            }
            finalRoomInfoArray[i].entranceDir = loadedData.roominfo[i].entranceDir;
            finalRoomInfoArray[i].exitDir = loadedData.roominfo[i].exitDir;
            finalRoomInfoArray[i].beenThere = loadedData.roominfo[i].beenThere;
        }
    }

    public void Reset()
    {
        GameObject[] objArray = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objArray)
        {
            if (obj.name.Contains("Wall") || obj.name.Contains("Floor") || obj.name.Contains("Hazard") || obj.name.Contains("Door")
                || obj.tag.Contains("Drop") || obj.tag == "LightTrail" || obj.name.Contains("Pickup") || obj.name.Contains("dead")
                || obj.name.Contains("Burn") || obj.tag == "Waypoint" || obj.name.Contains("Spawn") || obj.name.Contains("lone"))
            {
                Destroy(obj);
            }
        }
        // player.GetComponent<SaveTest>().saved = false;
        savenload.saved = false;
        CreateRoom();
    }
}
