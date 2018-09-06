using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Options : MonoBehaviour
{
    private static Options _instance;


    public static Options instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Options>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }
    }

    public int sfxVolume;
    public int musicVolume;

    int numMelee;
    int numDash;
    int numEnemiesKilled;
    int numAttempts;

    public bool[] achievements;
    public bool shouldload = false;
    [HideInInspector]
    public GameObject savedPlayer;
    GameObject player;
    bool beenAssigned;

    [HideInInspector]
    public bool easyMode;

    public bool watchIntro;

    float timer;

    public AudioClip achvSound;
    AudioSource aPlayer;

    //public PlayerEquipment tutEquip;
    //bool tutEquipAssigned;
    //bool tutPlayerAssigned;

    void Start()
    {
        timer = 0;
        watchIntro = false;
        achievements = new bool[24];

        //Page 1
        achievements[0] = false; // Defeat 100 enemies
        achievements[1] = false; // Melee
        achievements[2] = false; // Chest
        achievements[3] = false; // Dash
        achievements[4] = false; // Complete Tutorial
        achievements[5] = false; // Defeat Dethros
        achievements[6] = false; // Defeat Lorne
        achievements[7] = false; // Defeat Morrius

        achievements[8] = false; // Attempt 3 playthroughs
        achievements[9] = false; // Defeat a boss with full hp remaining
        achievements[10] = false; // Watch credits
        achievements[11] = false; // Fire ember
        achievements[12] = false; // wind ember
        achievements[13] = false; // frost ember
        achievements[14] = false; // death ember
        achievements[15] = false; // life ember

        achievements[16] = false; // earth ember
        achievements[17] = false; // ping
        achievements[18] = false; // die
        achievements[19] = false; // intro cutscene
        achievements[20] = false; // 
        achievements[21] = false; //
        achievements[22] = false; //
        achievements[23] = false; //


        Load();
        DontDestroyOnLoad(gameObject);

        beenAssigned = false;
        numMelee = 0;
        numEnemiesKilled = 0;
        numDash = 0;
        numAttempts = 0;

        aPlayer = GetComponent<AudioSource>();
        aPlayer.volume = sfxVolume * 0.01f;

        //tutEquip = null;
        //tutEquipAssigned = false;
        //tutPlayerAssigned = false;
    }




    public void AddToEnemy()
    {
        numEnemiesKilled++;
        if (numEnemiesKilled >= 100 && achievements[0] == false)
        {
            GameObject.Find("EnemyAchv").GetComponent<Image>().enabled = true;
            achievements[0] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void AddToMelee()
    {
        numMelee++;
        if (numMelee >= 50 && achievements[1] == false)
        {
            GameObject.Find("MeleeAchv").GetComponent<Image>().enabled = true;
            achievements[1] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void OpenChest()
    {
        if (achievements[2] == false)
        {
            GameObject.Find("ChestAchv").GetComponent<Image>().enabled = true;
            achievements[2] = true;
            aPlayer.PlayOneShot(achvSound);

            Save();
        }
    }

    public void AddToDash()
    {
        numDash++;
        if (numDash >= 50 && achievements[3] == false)
        {
            GameObject.Find("DashAchv").GetComponent<Image>().enabled = true;
            achievements[3] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void CompleteTutorial()
    {
        if (achievements[4] == false)
        {
            GameObject.Find("TutorialAchv").GetComponent<Image>().enabled = true;

            achievements[4] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }
    public void DethrosDie()
    {
        if (achievements[5] == false)
        {
            GameObject.Find("DethrosAchv").GetComponent<Image>().enabled = true;
            achievements[5] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }


    public void LorneDie()
    {
        if (achievements[6] == false)
        {
            GameObject.Find("LorneAchv").GetComponent<Image>().enabled = true;
            achievements[6] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void MorriusDie()
    {
        if (achievements[7] == false)
        {
            GameObject.Find("MorriusAchv").GetComponent<Image>().enabled = true;
            achievements[7] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void StartAttempt()
    {
        numAttempts++;
        if (numAttempts >= 3 && achievements[8] == false)
        {
            GameObject.Find("AttemptAchv").GetComponent<Image>().enabled = true;
            achievements[8] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void FullHp()
    {
        if (achievements[9] == false)
        {
            GameObject.Find("FullhpAchv").GetComponent<Image>().enabled = true;
            achievements[9] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void WatchCredits()
    {
        if (achievements[10] == false)
        {
            GameObject.Find("TheAchv").GetComponent<Image>().enabled = true;
            achievements[10] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void FireAchv()
    {
        if (achievements[11] == false)
        {
            GameObject.Find("FireAchv").GetComponent<Image>().enabled = true;
            achievements[11] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void WindAchv()
    {
        if (achievements[12] == false)
        {
            GameObject.Find("WindAchv").GetComponent<Image>().enabled = true;
            achievements[12] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void FrostAchv()
    {
        if (achievements[13] == false)
        {
            GameObject.Find("FrostAchv").GetComponent<Image>().enabled = true;
            achievements[13] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void DeathAchv()
    {
        if (achievements[14] == false)
        {
            GameObject.Find("DeathAchv").GetComponent<Image>().enabled = true;
            achievements[14] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void LifeAchv()
    {
        if (achievements[15] == false)
        {
            GameObject.Find("LifeAchv").GetComponent<Image>().enabled = true;
            achievements[15] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void EarthAchv()
    {
        if (achievements[16] == false)
        {
            GameObject.Find("EarthAchv").GetComponent<Image>().enabled = true;
            achievements[16] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void PingAchv()
    {
        if (achievements[17] == false)
        {
            GameObject.Find("PingAchv").GetComponent<Image>().enabled = true;
            achievements[17] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void DieAchv()
    {
        if (achievements[18] == false)
        {
            GameObject.Find("DieAchv").GetComponent<Image>().enabled = true;
            achievements[18] = true;
            aPlayer.PlayOneShot(achvSound);
            Save();
        }
    }

    public void IntroAchv()
    {
        if (achievements[19] == false)
        {
            GameObject.Find("IntroAchv").GetComponent<Image>().enabled = true;
            achievements[19] = true;
            aPlayer.PlayOneShot(achvSound);
            watchIntro = false;
            Save();
        }
    }


    void Update()
    {
        if (Screen.currentResolution.height != 720 || Screen.currentResolution.width != 1280)
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }

        if (watchIntro)
        {
            timer += Time.deltaTime;
            if (timer >= 5.0f)
            {
                IntroAchv();
            }
        }

        if (!beenAssigned && Application.loadedLevelName == "Game")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            beenAssigned = true;
        }

        //if (!tutPlayerAssigned && Application.loadedLevelName == "Tutorial")
        //{
        //    player = GameObject.FindGameObjectWithTag("Player");
        //    tutPlayerAssigned = true;
        //}
        //
        //if (player && Application.loadedLevelName == "Tutorial")
        //{
        //    tutEquip = player.GetComponent<PlayerEquipment>();
        //}
        //
        //if (!tutEquipAssigned && player && Application.loadedLevelName == "Game")
        //{
        //    player.GetComponent<PlayerEquipment>().SetToTutEquip(tutEquip);
        //    tutEquipAssigned = true;
        //}

#if UNITY_WEBPLAYER
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1280, 720, false);
            Screen.fullScreen = false;
        }
#endif
    }

    public void sfxIncrease()
    {
        sfxVolume += 5;
        if (sfxVolume > 100)
        {
            sfxVolume = 100;
        }
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }

        aPlayer.volume = sfxVolume * 0.01f;

    }

    public void sfxDecrease()
    {
        sfxVolume -= 5;
        if (sfxVolume < 0)
        {
            sfxVolume = 0;
        }
        
        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }

        aPlayer.volume = sfxVolume * 0.01f;

    }

    public void musicIncrease()
    {
        print("mIncrease");
        musicVolume += 5;
        if (musicVolume > 100)
        {
            musicVolume = 100;
        }


        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void musicDecrease()
    {
        print("mDecrease");

        musicVolume -= 5;
        if (musicVolume < 0)
        {
            musicVolume = 0;
        }

        Save();

        GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].BroadcastMessage("ChangeVolume", SendMessageOptions.DontRequireReceiver);
        }
    }


    public void Save()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
   || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            PlayerPrefs.SetInt("PlayerSFX", sfxVolume);
            PlayerPrefs.SetInt("PlayerMusic", musicVolume);

        }
        else
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/optioninfo.dat");
            OptionData data = new OptionData();
            data.sfxVolume = sfxVolume;
            data.musicVolume = musicVolume;
            data.achieve = achievements[0];
            data.achieve1 = achievements[1];
            data.achieve2 = achievements[2];
            data.achieve3 = achievements[3];
            data.achieve4 = achievements[4];
            data.achieve5 = achievements[5];
            data.achieve6 = achievements[6];
            data.achieve7 = achievements[7];
            data.achieve8 = achievements[8];
            data.achieve9 = achievements[9];
            data.achieve10 = achievements[10];
            data.achieve11 = achievements[11];
            data.achieve12 = achievements[12];
            data.achieve13 = achievements[13];
            data.achieve14 = achievements[14];
            data.achieve15 = achievements[15];
            data.achieve16 = achievements[16];
            data.achieve17 = achievements[17];
            data.achieve18 = achievements[18];
            data.achieve19 = achievements[19];
            data.achieve20 = achievements[20];
            data.achieve21 = achievements[21];
            data.achieve22 = achievements[22];
            data.achieve23 = achievements[23];
            bin.Serialize(file, data);
            file.Close();
        }


    }
    public void Load()
    {
        if (Application.platform == RuntimePlatform.OSXWebPlayer
|| Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            sfxVolume = PlayerPrefs.GetInt("PlayerSFX", 100);
            musicVolume = PlayerPrefs.GetInt("PlayerMusic", 100);

        }
        else
        {
            if (File.Exists(Application.persistentDataPath + "/optioninfo.dat"))
            {
                BinaryFormatter bin = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/optioninfo.dat", FileMode.Open);
                OptionData data = (OptionData)bin.Deserialize(file);
                sfxVolume = data.sfxVolume;
                musicVolume = data.musicVolume;
                achievements[0] = data.achieve;
                achievements[1] = data.achieve1;
                achievements[2] = data.achieve2;
                achievements[3] = data.achieve3;
                achievements[4] = data.achieve4;
                achievements[5] = data.achieve5;
                achievements[6] = data.achieve6;
                achievements[7] = data.achieve7;
                achievements[8] = data.achieve8;
                achievements[9] = data.achieve9;
                achievements[10] = data.achieve10;
                achievements[11] = data.achieve11;
                achievements[12] = data.achieve12;
                achievements[13] = data.achieve13;
                achievements[14] = data.achieve14;
                achievements[15] = data.achieve15;
                achievements[16] = data.achieve16;
                achievements[17] = data.achieve17;
                achievements[18] = data.achieve18;
                achievements[19] = data.achieve19;
                achievements[20] = data.achieve20;
                achievements[21] = data.achieve21;
                achievements[22] = data.achieve22;
                achievements[23] = data.achieve23;

                file.Close();

            }
        }
    }
}

[System.Serializable]
public class OptionData
{
    public int sfxVolume;
    public int musicVolume;
    public bool achieve;
    public bool achieve1;
    public bool achieve2;
    public bool achieve3;
    public bool achieve4;
    public bool achieve5;
    public bool achieve6;
    public bool achieve7;
    public bool achieve8;
    public bool achieve9;
    public bool achieve10;
    public bool achieve11;
    public bool achieve12;
    public bool achieve13;
    public bool achieve14;
    public bool achieve15;
    public bool achieve16;
    public bool achieve17;
    public bool achieve18;
    public bool achieve19;
    public bool achieve20;
    public bool achieve21;
    public bool achieve22;
    public bool achieve23;


}