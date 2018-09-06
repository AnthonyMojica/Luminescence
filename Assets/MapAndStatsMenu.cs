using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MapAndStatsMenu : MonoBehaviour
{
    bool showing;
    GameObject player;
    PlayerEquipment equipment;
    GameObject canvas;
    GUIStyle style;
    GUIStyle crStyle;
    GUIStyle doorStyle;
    GameObject dungeon;
    RoomGeneration generator;
    bool easyMode;

    public Text SpellPower;
    public Text AttackDamage;
    public Text MaxHP;
    public Text MaxLight;

    [Header("Accessory Tip")]
    public Text T1Name;
    public Text T1Stat1;
    public Text T1Stat2;
    public Text T1StatAmount1;
    public Text T1StatAmount2;

    [Header("Boot Tooltip")]
    public Text T2Name;
    public Text T2Stat1;
    public Text T2Stat2;
    public Text T2StatAmount1;
    public Text T2StatAmount2;

    [Header("Ember Tooltip")]
    public GameObject EmberIcon;
    public Text T3Name;
    public Text T3Stat1;
    public Text T3Stat2;
    public Text T3StatAmount1;
    public Text T3StatAmount2;


    float timer;

    void Start()
    {
        timer = .5f;
        player = GameObject.FindGameObjectWithTag("Player");
        equipment = player.GetComponent<PlayerEquipment>();
        canvas = GameObject.Find("Map&StatsCanvas");
        canvas.SetActive(false);
        showing = false;

        Texture2D texture = new Texture2D(1, 1);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.SetPixel(1, 1, new Color(.75f, .75f, .75f));
        texture.Apply();
        style = new GUIStyle();
        style.normal.background = texture;

        Texture2D texture2 = new Texture2D(1, 1);
        texture2.wrapMode = TextureWrapMode.Repeat;
        texture2.SetPixel(1, 1, Color.blue);
        texture2.Apply();
        crStyle = new GUIStyle();
        crStyle.normal.background = texture2;

        Texture2D door = new Texture2D(1, 1);
        door.wrapMode = TextureWrapMode.Repeat;
        door.SetPixel(1, 1, Color.magenta);
        door.Apply();
        doorStyle = new GUIStyle();
        doorStyle.normal.background = door;


        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        if (dungeon)
            generator = dungeon.GetComponent<RoomGeneration>();
        easyMode = GameObject.FindObjectOfType<Options>().easyMode;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = .5f;
            GetStatsFromEquipment();
        }
    }

    void OnGUI()
    {
        if (showing && dungeon)
        {
            int offsetIndex = 0;
            if (easyMode)
            {
                if (generator.currentRoom < 11)
                {
                    offsetIndex = 0;
                }
                else if (generator.currentRoom < 22)
                {
                    offsetIndex = 11;
                }
                else offsetIndex = 22;
            }
            else
            {
                if (generator.currentRoom < 9)
                {
                    offsetIndex = 0;
                }
                else if (generator.currentRoom < 18)
                {
                    offsetIndex = 9;
                }
                else offsetIndex = 18;
            }
            Vector2 offset = new Vector2(((Screen.width / 2) - (generator.finalRoomInfoArray[offsetIndex].width / 2) * 5) + 250,
                (Screen.height / 2) - (generator.finalRoomInfoArray[offsetIndex].height / 2) * 5);
            for (int i = offsetIndex + 1; i <= generator.currentRoom; i++)
            {
                switch (generator.finalRoomInfoArray[i - 1].exitDir)
                {
                    case 0:
                        offset.y += 25 + generator.finalRoomInfoArray[i - 1].height * 5;
                        break;
                    case 1:
                        offset.x += 25 + generator.finalRoomInfoArray[i - 1].width * 5;
                        break;
                    case 2:
                        offset.y -= 25 + generator.finalRoomInfoArray[i - 1].height * 5;
                        break;
                    case 3:
                        offset.x -= 25 + generator.finalRoomInfoArray[i - 1].width * 5;
                        break;
                    default:
                        break;
                }
            }
            for (int i = offsetIndex; i < generator.finalRoomInfoArray.Length; i++)
            {
                if (generator.finalRoomInfoArray[i].beenThere)
                {
                    GUI.Label(
                        new Rect(offset.x,
                            Screen.height - (offset.y + generator.finalRoomInfoArray[i].height * 5),
                        generator.finalRoomInfoArray[i].width * 5,
                        generator.finalRoomInfoArray[i].height * 5),
                        "", i == generator.currentRoom ? crStyle : style);
                    switch (generator.finalRoomInfoArray[i].entranceDir)
                    {
                        case 0:
                            GUI.Label(new Rect(offset.x + (generator.finalRoomInfoArray[i].width / 2) * 5, Screen.height - (offset.y + 5), 5, 5),
                                "", doorStyle);
                            break;
                        case 1:
                            GUI.Label(new Rect(offset.x, Screen.height - (offset.y + (generator.finalRoomInfoArray[i].height / 2) * 5), 5, 5),
                                "", doorStyle);
                            break;
                        case 2:
                            GUI.Label(new Rect(offset.x + (generator.finalRoomInfoArray[i].width / 2) * 5, Screen.height - (offset.y + generator.finalRoomInfoArray[i].height * 5), 5, 5),
                                "", doorStyle);
                            break;
                        case 3:
                            GUI.Label(new Rect(offset.x - 5 + generator.finalRoomInfoArray[i].width * 5, Screen.height - (offset.y + (generator.finalRoomInfoArray[i].height / 2) * 5), 5, 5),
                                "", doorStyle);
                            break;
                        default:
                            break;
                    }
                    switch (generator.finalRoomInfoArray[i].exitDir)
                    {
                        case 0:
                            GUI.Label(new Rect(offset.x + (generator.finalRoomInfoArray[i].width / 2) * 5, Screen.height - (offset.y + 5), 5, 5),
                                "", doorStyle);
                            offset.y -= 25 + generator.finalRoomInfoArray[i].height * 6;
                            break;
                        case 1:
                            GUI.Label(new Rect(offset.x, Screen.height - (offset.y + (generator.finalRoomInfoArray[i].height / 2) * 5), 5, 5),
                                "", doorStyle);
                            offset.x -= 25 + generator.finalRoomInfoArray[i].width * 6;
                            break;
                        case 2:
                            GUI.Label(new Rect(offset.x + (generator.finalRoomInfoArray[i].width / 2) * 5, Screen.height - (offset.y + generator.finalRoomInfoArray[i].height * 5), 5, 5),
                                "", doorStyle);
                            offset.y += 25 + generator.finalRoomInfoArray[i].height * 6;
                            break;
                        case 3:
                            GUI.Label(new Rect(offset.x - 5 + generator.finalRoomInfoArray[i].width * 5, Screen.height - (offset.y + (generator.finalRoomInfoArray[i].height / 2) * 5), 5, 5),
                                "", doorStyle);
                            offset.x += 25 + generator.finalRoomInfoArray[i].width * 6;
                            break;
                        default:
                            break;
                    }
                    //offset.x += (20 + generator.finalRoomInfoArray[i].width * 5) + 5;
                }
            }
            //for (int i = 0; i < generator.finalRoomInfoArray.Length; i++)
            //{
            //    //GUI.Label(new Rect(i * 10, i * 10, 10, 10), "", style);
            //}
        }
    }

    void MapAndStats()
    {
        showing = true;
        canvas.SetActive(true);
        equipment.paused = true;
    }

    void UnMapAndStats()
    {
        showing = false;
        canvas.SetActive(false);
        equipment.paused = false;
    }

    void GetStatsFromEquipment()
    {

        PlayerStats plyStats = player.GetComponent<PlayerStats>();
        float health = plyStats.maxHPModifier + 100;
        float lght = plyStats.maxLightModifier + 100;



        SpellPower.text = plyStats.spellModifier.ToString();
        AttackDamage.text = plyStats.meleeModifier.ToString();
        MaxHP.text = health.ToString();
        MaxLight.text = lght.ToString();

        T1Name.text = equipment.AccessoryName;
        T1Stat1.text = equipment.AccessoryStat1.TheStat.ToString();
        T1Stat2.text = equipment.AccessoryStat2.TheStat.ToString();


        float Amount = 0;
        switch (equipment.AccessoryStat1.TheStat)
        {
            case StatType.SpellMod:
                {
                    Amount = equipment.AccessoryStat1.StatAmount * .3f;
                    break;
                }
            case StatType.MeleeMod:
                {
                    Amount = equipment.AccessoryStat1.StatAmount * .3f;
                    break;
                }
            case StatType.MaxHP:
                {
                    Amount = equipment.AccessoryStat1.StatAmount * 2f;
                    break;
                }
            case StatType.MaxLight:
                {
                    Amount = equipment.AccessoryStat1.StatAmount * 1.3f;
                    break;
                }
        }
        T1StatAmount1.text = Amount.ToString();

        Amount = 0;
        switch (equipment.AccessoryStat2.TheStat)
        {
            case StatType.SpellMod:
                {
                    Amount = equipment.AccessoryStat2.StatAmount * .3f;
                    break;
                }
            case StatType.MeleeMod:
                {
                    Amount = equipment.AccessoryStat2.StatAmount * .3f;
                    break;
                }
            case StatType.MaxHP:
                {
                    Amount = equipment.AccessoryStat2.StatAmount * 2f;
                    break;
                }
            case StatType.MaxLight:
                {
                    Amount = equipment.AccessoryStat2.StatAmount * 1.3f;
                    break;
                }
        }
        T1StatAmount2.text = Amount.ToString();


        T2Name.text = equipment.BootName;
        T2Stat1.text = equipment.BootStat1.TheStat.ToString();
        T2Stat2.text = equipment.BootStat2.TheStat.ToString();



        Amount = 0;
        switch (equipment.BootStat1.TheStat)
        {
            case StatType.SpellMod:
                {
                    Amount = equipment.BootStat1.StatAmount * .3f;
                    break;
                }
            case StatType.MeleeMod:
                {
                    Amount = equipment.BootStat1.StatAmount * .3f;
                    break;
                }
            case StatType.MaxHP:
                {
                    Amount = equipment.BootStat1.StatAmount * 2f;
                    break;
                }
            case StatType.MaxLight:
                {
                    Amount = equipment.BootStat1.StatAmount * 1.3f;
                    break;
                }
        }
        T2StatAmount1.text = Amount.ToString();
        Amount = 0;
        switch (equipment.BootStat2.TheStat)
        {
            case StatType.SpellMod:
                {
                    Amount = equipment.BootStat2.StatAmount * .3f;
                    break;
                }
            case StatType.MeleeMod:
                {
                    Amount = equipment.BootStat2.StatAmount * .3f;
                    break;
                }
            case StatType.MaxHP:
                {
                    Amount = equipment.BootStat2.StatAmount * 2f;
                    break;
                }
            case StatType.MaxLight:
                {
                    Amount = equipment.BootStat2.StatAmount * 1.3f;
                    break;
                }
        }
        T2StatAmount2.text = Amount.ToString();




        if (equipment.emberDurability > 0)
        {
            EmberIcon.SetActive(true);
            T3Name.text = equipment.EmberName;
            T3Stat1.text = "Ember Durability";
            T3Stat2.text = "";
            T3StatAmount1.text = equipment.emberDurability.ToString();
            T3StatAmount2.text = "";
        }
        else
        {
            EmberIcon.SetActive(false);
            T3Name.text = " ";
            T3Stat1.text = " ";
            T3Stat2.text = " ";
            T3StatAmount1.text = " ";
            T3StatAmount2.text = " ";
        }

    }
}
