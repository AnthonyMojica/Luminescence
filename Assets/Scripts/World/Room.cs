using UnityEngine;
using System.Collections;

[System.Serializable]
public class Room : MonoBehaviour
{
    [Header("Dimensions")]
    public int width;
    public int height;
    [Header("Tiles")]
    public GameObject[] wallTiles;
    public GameObject[] floorTiles;
    [Header("Doors")]
    public GameObject SouthDoor;
    public GameObject NorthDoor;
    public GameObject EastDoor;
    public GameObject WestDoor;
    [Header("Inner Walls")]
    public Vector2[] innerWallPositions;
    [Header("Enemy Stuff")]
    public GameObject[] enemiesThatCanSpawn;
    public Vector2[] enemySpawnPoints;
    [HideInInspector]
    public bool[] enemySpawnPointUsed;
    public int minEnemies;
    public int maxEnemies;
    [Header("Hazard Stuff")]
    public GameObject hazard;
    public Vector2[] hazardSpawnPoints;
    [Header("Player Start Points")]
    public Vector2 bottomPlayerSpawn;
    public Vector2 rightPlayerSpawn;
    public Vector2 topPlayerSpawn;
    public Vector2 leftPlayerSpawn;
    [Header("Other Stuff")]
    public int entranceDir; // 0 = bottom, 1 = left, 2 = top, 3 = right, 4 = TREASURE ROOM!!!
    public int exitDir;
    public Vector2[] chestSpawnLocations;
    public float[] chestRotations;
    [HideInInspector]
    public bool beenThere;
    public int numEnemies = 0;
    public bool comingFromEntrance = true;
    public Vector2[] waypointLocations;
    public GameObject entryText;
    public int roomID;

    public void setUsed()
    {
        enemySpawnPointUsed = new bool[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            comingFromEntrance = true;
            enemySpawnPointUsed[i] = false;
            beenThere = false;
        }
    }
}
