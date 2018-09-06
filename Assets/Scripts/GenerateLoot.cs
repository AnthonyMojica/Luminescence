using UnityEngine;
using System.Collections;



public class GenerateLoot : MonoBehaviour
{


    public GameObject EmberFire;
    public GameObject EmberLife;
    public GameObject EmberWind;
    public GameObject EmberFrost;
    public GameObject EmberDeath;
    public GameObject EmberEarth;

    public GameObject AccesOrb;
    public GameObject AccesBolt;
    public GameObject AccesSnare;
    public GameObject AccesBlast;
    public GameObject AccesMine;
    public GameObject AccesSing;
    public GameObject AccesChain;

    public GameObject Decoy;
    public GameObject Blink;
    public GameObject WhirlWind;
    public GameObject Trailblazer;
    public GameObject Charge;

    ItemStat SendStat;

    public enum Loots
    {
        none = 0, emberfire, emberlife, emberwind, emberfrost, accesOrb, accesbolt, accessnare,
        accesblast, accesmine, accessing, acceschain
    }
    Loots lootToDrop;
    // Use this for initialization
    void Start()
    {

    }

    public void DropAPieceOfGear(Vector3 POS)
    {
        Vector3 ObjectPOS = transform.position;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PickUp");
        for (int i = 0; i < objects.Length; i++)
        {
            if (ObjectPOS.x < objects[i].transform.position.x + .3f && ObjectPOS.x > objects[i].transform.position.x - .3f && ObjectPOS.y < objects[i].transform.position.y + .3f && ObjectPOS.y > objects[i].transform.position.y - .3f)
            {
                //print("Loot Stacked! Deleted Generating Loot.");
                return;
            }
        }
        int SecondStat;
        int DetStat1, DetStat2;

        int RandNum = Random.Range(7, 19);
        GameObject Loot = DetermineType(RandNum);
        DetStat1 = DetermineStat(RandNum);
        DetStat2 = DetermineStat(RandNum);
        string SendName = DetermineName(RandNum, DetStat1, DetStat2);
        GameObject temp = (GameObject)Instantiate(Loot, POS, Quaternion.identity);
        temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);

        SendStat.TheStat = (StatType)DetStat1;
        SendStat.StatAmount = DetermineStatAmount(RandNum);
        temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);

        //50% Chance to Generate a Second stat
        SecondStat = Random.Range(0, 100);
        if (SecondStat > 50)
        {
            SendStat.TheStat = (StatType)DetStat2;
            SendStat.StatAmount = DetermineStatAmount(RandNum);
            temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void DropAnEmber()
    {

        Vector3 ObjectPOS = transform.position;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PickUp");
        for (int i = 0; i < objects.Length; i++)
        {
            if (ObjectPOS.x < objects[i].transform.position.x + .3f && ObjectPOS.x > objects[i].transform.position.x - .3f && ObjectPOS.y < objects[i].transform.position.y + .3f && ObjectPOS.y > objects[i].transform.position.y - .3f)
            {
                //print("Loot Stacked! Deleted Generating Loot.");
                return;
            }
        }
        int SecondStat;
        int DetStat1, DetStat2;

        int RandNum = Random.Range(1, 6);
        GameObject Loot = DetermineType(RandNum);
        DetStat1 = DetermineStat(RandNum);
        DetStat2 = DetermineStat(RandNum);
        string SendName = DetermineName(RandNum, DetStat1, DetStat2);
        GameObject temp = (GameObject)Instantiate(Loot, transform.position, Quaternion.identity);
        temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);

        SendStat.TheStat = (StatType)DetStat1;
        SendStat.StatAmount = DetermineStatAmount(RandNum);
        temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);

        //50% Chance to Generate a Second stat
        SecondStat = Random.Range(0, 100);
        if (SecondStat > 50)
        {
            SendStat.TheStat = (StatType)DetStat2;
            SendStat.StatAmount = DetermineStatAmount(RandNum);
            temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
        }
    }
    public void Generateloot()
    {
        int SecondStat;
        int DetStat1, DetStat2;

        if (gameObject.tag == "Chest")
        {
            int RandNum = Random.Range(1, 19);
            GameObject Loot = DetermineType(RandNum);
            DetStat1 = DetermineStat(RandNum);
            DetStat2 = DetermineStat(RandNum);
            string SendName = DetermineName(RandNum, DetStat1, DetStat2);
            GameObject temp = (GameObject)Instantiate(Loot, transform.position, Quaternion.identity);
            temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);

            SendStat.TheStat = (StatType)DetStat1;
            SendStat.StatAmount = DetermineStatAmount(RandNum);
            temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);
            //50% Chance to Generate a Second stat
            SecondStat = Random.Range(0, 100);
            if (SecondStat > 50)
            {
                SendStat.TheStat = (StatType)DetStat2;
                SendStat.StatAmount = DetermineStatAmount(RandNum);
                temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
            }

            int MoreLoots = Random.Range(0, 100);
            if (MoreLoots > 50)
            {
                transform.position += new Vector3(.5f, 0, 0);
                RandNum = Random.Range(1, 19);
                Loot = DetermineType(RandNum);
                DetStat1 = DetermineStat(RandNum);
                DetStat2 = DetermineStat(RandNum);
                SendName = DetermineName(RandNum, DetStat1, DetStat2);
                temp = (GameObject)Instantiate(Loot, transform.position, Quaternion.identity);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);
                transform.position -= new Vector3(.5f, 0, 0);

                SendStat.TheStat = (StatType)DetStat1;
                SendStat.StatAmount = DetermineStatAmount(RandNum);
                temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);

                //50% Chance to Generate a Second stat
                SecondStat = Random.Range(0, 100);
                if (SecondStat > 50)
                {
                    SendStat.TheStat = (StatType)DetStat2;
                    SendStat.StatAmount = DetermineStatAmount(RandNum);
                    temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
                }
            }
            int MoarLoots = Random.Range(0, 100);
            if (MoarLoots > 70)
            {
                transform.position -= new Vector3(.7f, 0, 0);
                RandNum = Random.Range(1, 19);
                Loot = DetermineType(RandNum);
                DetStat1 = DetermineStat(RandNum);
                DetStat2 = DetermineStat(RandNum);
                SendName = DetermineName(RandNum, DetStat1, DetStat2);
                temp = (GameObject)Instantiate(Loot, transform.position, Quaternion.identity);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);
                transform.position += new Vector3(.7f, 0, 0);

                SendStat.TheStat = (StatType)DetStat1;
                SendStat.StatAmount = DetermineStatAmount(RandNum);
                temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);

                //50% Chance to Generate a Second stat
                SecondStat = Random.Range(0, 100);
                if (SecondStat > 50)
                {
                    SendStat.TheStat = (StatType)DetStat2;
                    SendStat.StatAmount = DetermineStatAmount(RandNum);
                    temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
                }
            }
            int MoarLootz = Random.Range(0, 100);
            if (MoarLootz > 80)
            {
                transform.position += new Vector3(0, .6f, 0);
                RandNum = Random.Range(1, 19);
                Loot = DetermineType(RandNum);
                DetStat1 = DetermineStat(RandNum);
                DetStat2 = DetermineStat(RandNum);
                SendName = DetermineName(RandNum, DetStat1, DetStat2);
                temp = (GameObject)Instantiate(Loot, transform.position, Quaternion.identity);
                temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);
                transform.position -= new Vector3(0, .6f, 0);

                SendStat.TheStat = (StatType)DetStat1;
                SendStat.StatAmount = DetermineStatAmount(RandNum);
                temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);

                //50% Chance to Generate a Second stat
                SecondStat = Random.Range(0, 100);
                if (SecondStat > 50)
                {
                    SendStat.TheStat = (StatType)DetStat2;
                    SendStat.StatAmount = DetermineStatAmount(RandNum);
                    temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        if (gameObject.tag == "Enemy")
        {
            int MoarLootz = Random.Range(0, 100);
            if (MoarLootz > 86)
            {
                int MoarLootzz = Random.Range(0, 100);

                if (MoarLootzz > 70)
                {
                    DropAPieceOfGear(transform.position);
                }
                else if (MoarLootzz < 70)
                {
                    DropAnEmber();
                }
            }
            //    int MoarLootz = Random.Range(0, 100);
            //    if (MoarLootz > 86)
            //    {
            //        //transform.position += new Vector3(0, .6f, 0);
            //        int RandNum = Random.Range(1, 19);
            //        GameObject Loot = DetermineType(RandNum);
            //        DetStat1 = DetermineStat(RandNum);
            //        DetStat2 = DetermineStat(RandNum);
            //        string SendName = DetermineName(RandNum, DetStat1, DetStat2);


            //        //Don't Allow Loot to Overlap
            //        Vector3 ObjectPOS = transform.position;
            //        GameObject[] objects = GameObject.FindGameObjectsWithTag("PickUp");
            //        for (int i = 0; i < objects.Length; i++)
            //        {
            //            if (ObjectPOS.x < objects[i].transform.position.x + .3f && ObjectPOS.x > objects[i].transform.position.x - .3f && ObjectPOS.y < objects[i].transform.position.y + .3f && ObjectPOS.y > objects[i].transform.position.y - .3f)
            //            {
            //                //print("Loot Stacked! Deleted Generating Loot.");
            //                return;
            //            }
            //        }


            //        GameObject temp = (GameObject)Instantiate(Loot, transform.position, transform.rotation);
            //        temp.SendMessage("SetName", SendName, SendMessageOptions.DontRequireReceiver);

            //        SendStat.TheStat = (StatType)DetStat1;
            //        SendStat.StatAmount = DetermineStatAmount(RandNum);
            //        temp.SendMessage("SetStat1", SendStat, SendMessageOptions.DontRequireReceiver);

            //        //50% Chance to Generate a Second stat
            //        SecondStat = Random.Range(0, 100);
            //        if (SecondStat > 50)
            //        {
            //            SendStat.TheStat = (StatType)DetStat2;
            //            SendStat.StatAmount = DetermineStatAmount(RandNum);
            //            temp.SendMessage("SetStat2", SendStat, SendMessageOptions.DontRequireReceiver);
            //        }
            //        //temp.SendMessage("SetStat1", )
            //        //transform.position -= new Vector3(0, .6f, 0);
            //    }
        }

    }

    GameObject DetermineType(int randNum)
    {
        if (randNum == 1)
        {
            return EmberFire;
        }
        else if (randNum == 2)
        {
            return EmberLife;
        }
        else if (randNum == 3)
        {
            return EmberWind;
        }
        else if (randNum == 4)
        {
            return EmberFrost;
        }
        else if (randNum == 5)
        {
            return EmberDeath;
        }
        else if (randNum == 6)
        {
            return EmberEarth;
        }
        else if (randNum == 7)
        {
            return AccesOrb;
        }
        else if (randNum == 8)
        {
            return AccesBolt;
        }
        else if (randNum == 9)
        {
            return AccesSnare;
        }
        else if (randNum == 10)
        {
            return AccesBlast;
        }
        else if (randNum == 11)
        {
            return AccesMine;
        }
        else if (randNum == 12)
        {
            return AccesSing;
        }
        else if (randNum == 13)
        {
            return AccesChain;
        }
        else if (randNum == 14)
        {
            return Decoy;
        }
        else if (randNum == 15)
        {
            return Blink;
        }
        else if (randNum == 16)
        {
            return WhirlWind;
        }
        else if (randNum == 17)
        {
            return Trailblazer;
        }
        else if (randNum == 18)
        {
            return Charge;
        }
        print("DETERMINE LOOT ERROR: Returned Chain");
        return AccesChain;
    }

    int DetermineStat(int ItemID)
    {

        //IF (ItemID is an Ember)
        if (ItemID <= 6)
        {
            return 0; //No Stat Boosts
        }

        //IF (ItemID is a spell)
        if (ItemID > 6 && ItemID < 19)
        {
            int ItemStatNum = Random.Range(1, 5);

            if (ItemStatNum == 1)
                return 1;
            if (ItemStatNum == 2)
                return 2;
            if (ItemStatNum == 3)
                return 3;
            if (ItemStatNum == 4)
                return 4;
        }
        return 0;
    }

    int DetermineStatAmount(int ItemIDD)
    {
        if (ItemIDD < 7)
        {
            return 0; //No Stat Boosts
        }
        int StatGen = Random.Range(1, 3);
        RoomGeneration Dunref;
        GameObject Dun = GameObject.FindGameObjectWithTag("Dungeon");
        if (Dun != null)
        {
            Dunref = Dun.GetComponent<RoomGeneration>();
            StatGen += Dunref.currentRoom;
        }

        return StatGen;
    }

    string DetermineName(int randNum, int Attribute1, int Attribute2)
    {
        string name = "Unknown Object";
        int RandomStuff = 0;
        if (randNum == 1)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name = "Fiery Ember";
                        break;
                    }
                case 2:
                    {
                        name = "Burning Ember";
                        break;
                    }
                case 3:
                    {
                        name = "Fire Ember";
                        break;
                    }
            }
        }
        else if (randNum == 2)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name = "Life Ember";
                        break;
                    }
                case 2:
                    {
                        name = "Regenerating Ember";
                        break;
                    }
                case 3:
                    {
                        name = "Rejuvenation Ember";
                        break;
                    }
            }
        }
        else if (randNum == 3)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name = "Gale Ember";
                        break;
                    }
                case 2:
                    {
                        name = "Gusting Ember";
                        break;
                    }
                case 3:
                    {
                        name = "Windy Ember";
                        break;
                    }
            }
        }
        else if (randNum == 4)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name = "Frosty Ember";
                        break;
                    }
                case 2:
                    {
                        name = "Icy Ember";
                        break;
                    }
                case 3:
                    {
                        name = "Frozen Ember";
                        break;
                    }
            }
        }
        else if (randNum == 5)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name = "Death Ember";
                        break;
                    }
                case 2:
                    {
                        name = "Undying Ember";
                        break;
                    }
                case 3:
                    {
                        name = "CorpseExplosion Ember";
                        break;
                    }
            }
        }
        else if (randNum == 6)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name = "Quaking Ember";
                        break;
                    }
                case 2:
                    {
                        name = "Earth Ember";
                        break;
                    }
                case 3:
                    {
                        name = "EarthQuake Ember";
                        break;
                    }
            }
        }
        else if (randNum == 7)
        {
            name = "Light Orb of ";
        }
        else if (randNum == 8)
        {
            name = "Light Bolt of ";
        }
        else if (randNum == 9)
        {
            name = "Ensaring Light of ";
        }
        else if (randNum == 10)
        {
            name = "Light Blast of ";
        }
        else if (randNum == 11)
        {
            name = "Light Mine of ";
        }
        else if (randNum == 12)
        {
            name = "Light Singularity of ";
        }
        else if (randNum == 13)
        {
            name = "Chain Lightning of ";
        }
        else if (randNum == 14)
        {
            name = "Decoy Boots of ";
        }
        else if (randNum == 15)
        {
            name = "Blink Boots of ";
        }
        else if (randNum == 16)
        {
            name = "Whirlwind Boots of ";
        }
        else if (randNum == 17)
        {
            name = "Trailblaizer Boots of ";
        }
        else if (randNum == 18)
        {
            name = "Charging Boots of ";
        }



        if (Attribute1 == 2)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name += "Casting";
                        break;
                    }
                case 2:
                    {
                        name += "Power";
                        break;
                    }
                case 3:
                    {
                        name += "the Mage";
                        break;
                    }
            }
        }
        if (Attribute1 == 1)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name += "Strength";
                        break;
                    }
                case 2:
                    {
                        name += "the Bull";
                        break;
                    }
                case 3:
                    {
                        name += "Attack";
                        break;
                    }
            }
        }
        if (Attribute1 == 3)
        {
            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name += "Fortitude";
                        break;
                    }
                case 2:
                    {
                        name += "Health";
                        break;
                    }
                case 3:
                    {
                        name += "Suvival";
                        break;
                    }
            }
        }
        if (Attribute1 == 4)
        {

            RandomStuff = Random.Range(1, 4);
            switch (RandomStuff)
            {
                case 1:
                    {
                        name += "Shining";
                        break;
                    }
                case 2:
                    {
                        name += "Brightness";
                        break;
                    }
                case 3:
                    {
                        name += "Sight";
                        break;
                    }
            }
        }



        return name;

        //string theName = "";

        //string[] embers = new string[10];
        //embers[1] = "Scorching Ember of ";
        //embers[8] = "Quake Ember of ";
        //embers[2] = "Rejuvenating Ember of ";
        //embers[3] = "Gale Ember of ";
        //embers[4] = "Frost Ember of";
        //embers[5] = "Doom Ember of ";


        //string[] accessories = new string[3];
        //accessories[0] = "Ring of ";
        //accessories[1] = "Bracelet of ";
        //accessories[2] = "Necklace of ";

        //string[] attribute1 = new string[2];
        //attribute1[0] = "Awesome";
        //attribute1[1] = "Stengthening";

        //string[] attribute2 = new string[2];
        //attribute2[0] = "Destruction";
        //attribute2[1] = "Syphoning";

        //if (randNum > 4)
        //{
        //    theName += accessories[Random.Range(0, 2)];
        //}
        //else
        //    theName += embers[randNum];
        //if (Random.value > 0.5f)
        //{

        //    theName += attribute1[Random.Range(0, 1)];
        //}
        //else
        //    theName += attribute2[Random.Range(0, 1)];
    }
}

