using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum equipmentType { Boot, Accessory, Ember };

public enum StatType { None = 0, MeleeMod, SpellMod, MaxHP, MaxLight };

public struct ItemStat
{
    public StatType TheStat;
    public float StatAmount;
};
public class Pickup : MonoBehaviour
{
    public bool displaytooltips = false;
    public equipmentType typeOfEquipment;     // See above for enum.
    public int whichOneToEquip = 1;     // See PlayerEquipment script for these enums.
    public GameObject orbOfLightPickup;
    public GameObject boltOfLightPickup;
    public GameObject blastOfLightPickup;
    public GameObject chainLightningPickup;
    public GameObject singularityPickup;
    public GameObject snarePickup;
    public GameObject lightMinePickup;
    public GameObject TrailblazerBootPickup;
    public GameObject whirlwindBootPickup;
    public GameObject chargeBootPickup;
    public GameObject decoyBootPickup;
    public GameObject blinkBootPickup;
    public Font font;
    float sendTimer;
    public GameObject TooltipWindow;
    RawImage ToolTipBack;
    public Vector3 ToolPOS;
    SetToolTipTexts ToolTipTexts;
    //    Camera cameras;
    public GameObject Temp;

    GameObject player;
    PlayerEquipment equipment;


    ItemStat Stat1forPickup;
    ItemStat Stat2forPickup;
    string NameforPickup;


    int phase;
    float timer;
    void Start()
    {
        sendTimer = 1;
        //        cameras = GameObject.FindObjectOfType<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (TooltipWindow)
            Temp = Instantiate(TooltipWindow);
        // TooltipWindow = GameObject.FindGameObjectWithTag("ToolTipCanvas");

        equipment = player.GetComponent<PlayerEquipment>();
        phase = 0;
    }

    void Update()
    {
        sendTimer -= Time.deltaTime;
        if (phase == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 0.1f, transform.position.z);
            timer += Time.deltaTime;
            if (timer >= 0.8f)
            {
                timer = 0;
                phase = 1;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 0.1f, transform.position.z);
            timer += Time.deltaTime;
            if (timer >= 0.8f)
            {
                timer = 0;
                phase = 0;
            }
        }

        //if (TooltipWindow != null)
        //{
        //    ToolTipBack.transform.localPosition = transform.position;
        //    print("Change");
        //}
        if (sendTimer < 0 && ToolTipBack != null && displaytooltips == true)
        {
            //ToolTipBack.SendMessage("ToolSetTexts", ToolTipTexts, SendMessageOptions.DontRequireReceiver);
            sendTimer = .5f;
        }
        if (ToolTipBack != null && displaytooltips == true)
        {
            ToolPOS = Camera.main.WorldToScreenPoint(transform.position);
            float offsetY = Screen.height - 310;
            ToolTipBack.transform.localPosition = new Vector3(ToolPOS.x - 550, ToolPOS.y - offsetY, -5);
        }


    }

    void OnGUI()
    {

        #region //if (theName.Length != 0)
        //{
        //string temp = "Press E to pick up the \n";
        //    temp += theName;
        //}
        //if (theName.Contains("Ember"))
        //    temp += "\n Durabilty: 10\n";

        //GUI.Box(new Rect(cameras.WorldToScreenPoint(player.transform.position).x + 32, Screen.height - cameras.WorldToScreenPoint(player.transform.position).y, 250, 150), temp);
        #endregion

    }
    void DisplayTooltip()
    {

        displaytooltips = true;
        if (Temp != null)
        {
            Temp.SetActive(true);
            ToolTipBack = Temp.GetComponentInChildren<RawImage>();
            ToolTipBack.SendMessage("ToolSetTexts", ToolTipTexts, SendMessageOptions.DontRequireReceiver);
        }
    }

    void DoNotDisplayTooltip()
    {
        displaytooltips = false;
        if (Temp != null)
            Temp.SetActive(false);
    }


    void OnTriggerStay(Collider other)
    {
        if ((InputManager.controller && Input.GetButtonDown("CInteract") || (!InputManager.controller && Input.GetButtonDown("KBInteract")))
            && other.gameObject == player)
        {

            GameObject[] gos = GameObject.FindGameObjectsWithTag("PickUp");

            float distance = Vector3.Distance(transform.position, player.transform.position);

            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i] == gameObject)
                {
                    continue;
                }
                if (Vector3.Distance(gos[i].transform.position, player.transform.position) <= distance)
                {
                    return;
                }
            }


            switch (typeOfEquipment)
            {
                case equipmentType.Boot:
                    #region Boots
                    switch (equipment.equippedBoot)
                    {

                        case boot.None:
                            break;
                        case boot.Trailblazer:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);

                            GameObject Temp0 = (GameObject)Instantiate(TrailblazerBootPickup, transform.position, transform.rotation);
                            Temp0.SendMessage("SetName", equipment.BootName, SendMessageOptions.DontRequireReceiver);
                            Temp0.SendMessage("SetStat1", equipment.BootStat1, SendMessageOptions.DontRequireReceiver);
                            Temp0.SendMessage("SetStat2", equipment.BootStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case boot.Whirlwind:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject Temp2 = (GameObject)Instantiate(whirlwindBootPickup, transform.position, transform.rotation);
                            Temp2.SendMessage("SetName", equipment.BootName, SendMessageOptions.DontRequireReceiver);
                            Temp2.SendMessage("SetStat1", equipment.BootStat1, SendMessageOptions.DontRequireReceiver);
                            Temp2.SendMessage("SetStat2", equipment.BootStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case boot.Charge:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject Temp3 = (GameObject)Instantiate(chargeBootPickup, transform.position, transform.rotation);
                            Temp3.SendMessage("SetName", equipment.BootName, SendMessageOptions.DontRequireReceiver);
                            Temp3.SendMessage("SetStat1", equipment.BootStat1, SendMessageOptions.DontRequireReceiver);
                            Temp3.SendMessage("SetStat2", equipment.BootStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case boot.Decoy:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject Temp4 = (GameObject)Instantiate(decoyBootPickup, transform.position, transform.rotation);
                            Temp4.SendMessage("SetName", equipment.BootName, SendMessageOptions.DontRequireReceiver);
                            Temp4.SendMessage("SetStat1", equipment.BootStat1, SendMessageOptions.DontRequireReceiver);
                            Temp4.SendMessage("SetStat2", equipment.BootStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case boot.Blink:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject Temp5 = (GameObject)Instantiate(blinkBootPickup, transform.position, transform.rotation);
                            Temp5.SendMessage("SetName", equipment.BootName, SendMessageOptions.DontRequireReceiver);
                            Temp5.SendMessage("SetStat1", equipment.BootStat1, SendMessageOptions.DontRequireReceiver);
                            Temp5.SendMessage("SetStat2", equipment.BootStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        default:
                            break;
                    }
                    switch (whichOneToEquip)
                    {
                        case 1:
                            equipment.equippedBoot = boot.Trailblazer;
                            equipment.BootName = NameforPickup;
                            equipment.BootStat1 = Stat1forPickup;
                            equipment.BootStat2 = Stat2forPickup;
                            break;
                        case 2:
                            equipment.equippedBoot = boot.Whirlwind;
                            equipment.BootName = NameforPickup;
                            equipment.BootStat1 = Stat1forPickup;
                            equipment.BootStat2 = Stat2forPickup;
                            break;
                        case 3:
                            equipment.equippedBoot = boot.Charge;
                            equipment.BootName = NameforPickup;
                            equipment.BootStat1 = Stat1forPickup;
                            equipment.BootStat2 = Stat2forPickup;
                            break;
                        case 4:
                            equipment.equippedBoot = boot.Decoy;
                            equipment.BootName = NameforPickup;
                            equipment.BootStat1 = Stat1forPickup;
                            equipment.BootStat2 = Stat2forPickup;
                            break;
                        case 5:
                            equipment.equippedBoot = boot.Blink;
                            equipment.BootName = NameforPickup;
                            equipment.BootStat1 = Stat1forPickup;
                            equipment.BootStat2 = Stat2forPickup;
                            break;
                        default:
                            break;
                    }
                    break;
                    #endregion
                case equipmentType.Accessory:
                    #region Accessories
                    switch (equipment.equippedAccessory)
                    {
                        case accessory.None:
                            break;
                        case accessory.OrbOfLight:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp0 = (GameObject)Instantiate(orbOfLightPickup, transform.position, transform.rotation);
                            _Temp0.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp0.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp0.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case accessory.BoltOfLight:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp1 = (GameObject)Instantiate(boltOfLightPickup, transform.position, transform.rotation);
                            _Temp1.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp1.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp1.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case accessory.BlastOfLight:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp2 = (GameObject)Instantiate(blastOfLightPickup, transform.position, transform.rotation);
                            _Temp2.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp2.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp2.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case accessory.ChainLightning:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp3 = (GameObject)Instantiate(chainLightningPickup, transform.position, transform.rotation);
                            _Temp3.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp3.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp3.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case accessory.Singularity:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp4 = (GameObject)Instantiate(singularityPickup, transform.position, transform.rotation);
                            _Temp4.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp4.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp4.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case accessory.Snare:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp5 = (GameObject)Instantiate(snarePickup, transform.position, transform.rotation);
                            _Temp5.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp5.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp5.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        case accessory.LightMine:
                            player.SendMessage("PlayEquipmentSound", SendMessageOptions.DontRequireReceiver);
                            GameObject _Temp6 = (GameObject)Instantiate(lightMinePickup, transform.position, transform.rotation);
                            _Temp6.SendMessage("SetName", equipment.AccessoryName, SendMessageOptions.DontRequireReceiver);
                            _Temp6.SendMessage("SetStat1", equipment.AccessoryStat1, SendMessageOptions.DontRequireReceiver);
                            _Temp6.SendMessage("SetStat2", equipment.AccessoryStat2, SendMessageOptions.DontRequireReceiver);
                            break;
                        default:
                            break;
                    }
                    switch (whichOneToEquip)
                    {
                        case 1:
                            equipment.equippedAccessory = accessory.OrbOfLight;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        case 2:
                            equipment.equippedAccessory = accessory.BoltOfLight;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        case 3:
                            equipment.equippedAccessory = accessory.BlastOfLight;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        case 4:
                            equipment.equippedAccessory = accessory.ChainLightning;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        case 5:
                            equipment.equippedAccessory = accessory.Singularity;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        case 6:
                            equipment.equippedAccessory = accessory.Snare;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        case 7:
                            equipment.equippedAccessory = accessory.LightMine;
                            equipment.AccessoryName = NameforPickup;
                            equipment.AccessoryStat1 = Stat1forPickup;
                            equipment.AccessoryStat2 = Stat2forPickup;
                            break;
                        default:
                            break;
                    }
                    break;
                    #endregion
                case equipmentType.Ember:
                    #region Embers
                    switch (whichOneToEquip)
                    {
                        case 1:
                            equipment.emberDurability = 10;
                            equipment.equippedEmber = ember.Life;
                            GameObject.Find("TheOptions").GetComponent<Options>().LifeAchv();
                            break;
                        case 2:
                            equipment.emberDurability = 10;
                            equipment.equippedEmber = ember.Death;
                            GameObject.Find("TheOptions").GetComponent<Options>().DeathAchv();

                            break;
                        case 3:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Earth;
                            GameObject.Find("TheOptions").GetComponent<Options>().EarthAchv();
                            break;
                        case 4:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Wind;
                            GameObject.Find("TheOptions").GetComponent<Options>().WindAchv();
                            break;
                        case 5:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Fire;
                            GameObject.Find("TheOptions").GetComponent<Options>().FireAchv();
                            break;
                        case 6:
                            equipment.emberDurability = 10;

                            equipment.equippedEmber = ember.Ice;
                            GameObject.Find("TheOptions").GetComponent<Options>().FrostAchv();
                            break;
                        default:
                            break;
                    }
                    break;
                    #endregion
                default:
                    break;
            }
            equipment.CalculateStats();
            Destroy(Temp);
            Destroy(gameObject);
        }
    }

    void SetName(string aName)
    {
        // print("CHANGENAME");
        NameforPickup = aName;
        ToolTipTexts._ItemName = aName;
    }

    void SetStat1(ItemStat firstStat)
    {
        Stat1forPickup = firstStat;
        string theName = "nada";
        switch (firstStat.TheStat)
        {
            case StatType.None:
                {
                    theName = " ";
                    break;
                }
            case StatType.SpellMod:
                {
                    theName = "Spell Power";
                    firstStat.StatAmount *= .3f;
                    break;
                }
            case StatType.MeleeMod:
                {
                    theName = "Attack Damage";
                    firstStat.StatAmount *= .3f;
                    break;
                }
            case StatType.MaxHP:
                {
                    theName = "Max HP";
                    firstStat.StatAmount *= 2;
                    break;
                }
            case StatType.MaxLight:
                {
                    theName = "Max Light";
                    firstStat.StatAmount *= 1.3f;
                    break;
                }
        }

        ToolTipTexts._ItemStat1 = theName;
        if (firstStat.StatAmount == 0)
            ToolTipTexts._ItemAmount2 = " ";
        else
            ToolTipTexts._ItemAmount1 = firstStat.StatAmount.ToString();
    }

    void SetStat2(ItemStat secondStat)
    {
        Stat2forPickup = secondStat;
        string theName = "nada";
        switch (secondStat.TheStat)
        {
            case StatType.None:
                {
                    theName = " ";
                    break;
                }
            case StatType.SpellMod:
                {
                    theName = "Spell Power";
                    secondStat.StatAmount *= .3f;
                    break;
                }
            case StatType.MeleeMod:
                {
                    theName = "Attack Damage";
                    secondStat.StatAmount *= .3f;
                    break;
                }
            case StatType.MaxHP:
                {
                    theName = "Max HP";
                    secondStat.StatAmount *= 2;
                    break;
                }
            case StatType.MaxLight:
                {
                    theName = "Max Light";
                    secondStat.StatAmount *= 1.3f;
                    break;
                }
        }
        ToolTipTexts._ItemStat2 = theName;
        if (secondStat.StatAmount == 0)
            ToolTipTexts._ItemAmount2 = " ";
        else
            ToolTipTexts._ItemAmount2 = secondStat.StatAmount.ToString();
    }



}
