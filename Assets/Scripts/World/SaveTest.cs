using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
 

using System.IO;

public class SaveTest : MonoBehaviour {

	GameObject player;
	public float life;
	public float lightss;
    public bool shouldload = false;
    public GameObject dungeon;
    public RoomGeneration theRooms;
    Options options;
    PlayerStats theStats;
	public PlayerData data;
    public int enemies=0;
    public bool saved = false;
	public bool canyouload=true;
	PlayerEquipment eq;

	

	public void YOUDIED()
	{
		data.canyouload = false;
		File.Delete (Application.persistentDataPath + "/playerinfo.dat");

	//	Save ();
	}
	
	// Use this for initialization
	void Start ()


	{
		data = new PlayerData ();
        options = GameObject.FindObjectOfType<Options>();
        player = GameObject.FindGameObjectWithTag("Player");
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
		if(dungeon!=null)
			theRooms = dungeon.GetComponent<RoomGeneration>();

 
		data.canyouload = true;
		LoadData ();
      
	}
	
	// Update is called once per frame
	void Update () {
		shouldload = options.shouldload;
		if(player==null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		if(player!=null)
			{
			eq=player.GetComponent<PlayerEquipment>();
			theStats = player.GetComponent<PlayerStats>();

			}


		}
 
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (dungeon == null)
        {
            dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            if (dungeon != null)
                theRooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
        }

      //  else
         //   if (theRooms != null)
          //      if (enemies <= 0&&saved==false)
                    
         //           {
       //                 Save();
		//		data.canyousave=true;
       //                 Debug.Log("Saved!");

      //                  saved = true;
      //              }
 

	
	}
 
   public void Save()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			eq = player.GetComponent<PlayerEquipment> ();
			theStats = player.GetComponent<PlayerStats> ();
			
		}
	 

			if (Application.platform == RuntimePlatform.OSXWebPlayer
				|| Application.platform == RuntimePlatform.WindowsWebPlayer) {
				Debug.Log ("OK Save");
			
				PlayerPrefs.SetFloat ("PlayerHealth", player.GetComponent<Health> ().currentHP);
				PlayerPrefs.SetFloat ("PlayerLight", player.GetComponent<PlayerLight> ().currentLight);
				PlayerPrefs.SetFloat ("MeleeMod", theStats.meleeModifier);
				PlayerPrefs.SetFloat ("SpellMod", theStats.spellModifier);
				PlayerPrefs.SetFloat ("LightMod", theStats.maxLightModifier);
				PlayerPrefs.SetFloat ("LifeMod", theStats.maxHPModifier);
				PlayerPrefs.SetString ("EmberName", eq.AccessoryName);
				PlayerPrefs.SetString ("BootName", eq.BootName);
				PlayerPrefs.SetInt ("Spell", (int)eq.equippedAccessory);
				PlayerPrefs.SetFloat ("EmberStat1", eq.AccessoryStat1.StatAmount);
				PlayerPrefs.SetInt ("EmberStat1t", (int)eq.EmberStat1.TheStat);
				PlayerPrefs.SetFloat ("EmberStat2", eq.AccessoryStat2.StatAmount);
				PlayerPrefs.SetInt ("EmberStat2t", (int)eq.EmberStat2.TheStat);
				PlayerPrefs.SetFloat ("BootStat1", eq.BootStat1.StatAmount);
				PlayerPrefs.SetInt ("BootStat1t", (int)eq.BootStat1.TheStat);
				PlayerPrefs.SetFloat ("BootStat2", eq.BootStat2.StatAmount);
				PlayerPrefs.SetInt ("BootStat2t", (int)eq.BootStat2.TheStat);
				PlayerPrefs.SetFloat ("Durability", eq.emberDurability);
	 
				if (options.easyMode == true)
					PlayerPrefs.SetInt ("EasyMode", 1);
				else
					PlayerPrefs.SetInt ("EasyMode", 0);
				PlayerPrefs.SetInt ("Ember", (int)eq.equippedEmber);
				PlayerPrefs.SetInt ("Boot", (int)eq.equippedBoot);
				PlayerPrefs.SetInt ("CurrentRoom", theRooms.currentRoom);

				PlayerPrefs.SetInt ("RoomArrLenght", theRooms.finalRoomInfoArray.Length);
		 

				for (int i = 0; i < theRooms.finalRoomInfoArray.Length; i++) {
					string temp = "Room_" + i.ToString () + "_been there";
					string temps = "RoomID_" + i.ToString ();
					string tempsss = "RoomExitDir_" + i.ToString ();
					string tempss = "RoomEntryDir_" + i.ToString ();
					if (theRooms.finalRoomInfoArray [i].beenThere == true)
						PlayerPrefs.SetInt (temp, 1);
					else
						PlayerPrefs.SetInt (temp, 0);
					PlayerPrefs.SetInt (temps, theRooms.finalRoomInfoArray [i].roomID);
					PlayerPrefs.SetInt (tempss, theRooms.finalRoomInfoArray [i].entranceDir);
					PlayerPrefs.SetInt (tempsss, theRooms.finalRoomInfoArray [i].exitDir);
				

                
				}

			}
		else
		{
				FileStream file;
        
				BinaryFormatter bin = new BinaryFormatter ();
			if (!File.Exists (Application.persistentDataPath + "/playerinfo.dat"))
					file = File.Create (Application.persistentDataPath + "/playerinfo.dat");
				else
					file = File.Open (Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
         
           
				data.health = player.GetComponent<Health> ().currentHP;
				data.theLight = player.GetComponent<PlayerLight> ().currentLight;

				data.easymode = options.easyMode;

				data.maxHPModifier = theStats.maxHPModifier;
				data.maxLightModifier = theStats.maxLightModifier;
				data.meleeModifier = theStats.meleeModifier;
				data.spellModifier = theStats.spellModifier;

				data.equippedember = (int)eq.equippedEmber;
				data.equippedboot = (int)eq.equippedBoot;

				data.embername = eq.AccessoryName;
				Debug.Log (data.embername);

				data.emberstat1 = eq.EmberStat1.StatAmount;
				data.emberstattype1 = (int)eq.EmberStat1.TheStat;
				data.emberstat2 = eq.EmberStat2.StatAmount;
				data.emberstattype2 = (int)eq.EmberStat2.TheStat;
				data.emberdurability = eq.emberDurability;

				data.bootname = eq.BootName;		
				data.bootstat1 = eq.BootStat1.StatAmount;
				data.boottattype1 = (int)eq.BootStat1.TheStat;
				data.bootstat2 = eq.BootStat2.StatAmount;
				data.boottattype2 = (int)eq.BootStat2.TheStat;
				Debug.Log (data.bootname);
				data.currentroom = theRooms.currentRoom;
				data.equippedspell = (int)eq.equippedAccessory;
				CopyRooms (data);
			Debug.Log(data.canyouload);
				bin.Serialize (file, data);
 
				file.Close ();
        
	 
			}



	}

   public void Load()
   {
		player = GameObject.FindGameObjectWithTag("Player");
		if(player!=null)
		{
			eq=player.GetComponent<PlayerEquipment>();
			theStats = player.GetComponent<PlayerStats>();
			
		}
               player.GetComponent<Health>().currentHP         = data.health;
               player.GetComponent<PlayerLight>().currentLight = data.theLight;

               theStats.maxHPModifier    = data.maxHPModifier;
               theStats.maxLightModifier = data.maxLightModifier;
               theStats.meleeModifier    = data.meleeModifier;
               theStats.spellModifier    = data.spellModifier;
				Debug.Log(data.embername);
				Debug.Log(data.bootname);

				eq.equippedEmber          =  		(ember)data.equippedember ;
				eq.equippedBoot           =    		(boot)data.equippedboot;   
				eq.equippedAccessory      = (accessory)data.equippedspell;
				eq.AccessoryName          = 	data.embername;
				eq.EmberStat1.StatAmount  = 	data.emberstat1;
				eq.EmberStat1.TheStat     =(StatType) data.emberstattype1;
				eq.EmberStat2.StatAmount  = 	data.emberstat2;
				eq.EmberStat1.TheStat     =(StatType) data.emberstattype1;
				
				eq.BootName               = 	data.bootname;  
				eq.BootStat1.StatAmount   = 	data.bootstat1;
				eq.BootStat1.TheStat      =(StatType) data.boottattype1;
				
				eq.BootStat2.StatAmount   = 	data.bootstat2; 
				eq.BootStat2.TheStat      =(StatType) data.boottattype2;
				eq.emberDurability        = data.emberdurability;
				Debug.Log (eq.BootName);
				Debug.Log (eq.AccessoryName);

                options.easyMode          = data.easymode;
				if(theRooms!=null)

				{				
					theRooms.currentRoom= data.currentroom;
				theRooms.loadedData= data;
				}
		
     
    
               
           

           
       
   }
    void CopyRooms(PlayerData data)
   {
       data.amountofrooms = theRooms.finalRoomInfoArray.Length;
       data.roominfo = new RoomData[data.amountofrooms];
       for (int i = 0; i < data.amountofrooms; i++)
       {
		   data.roominfo[i]            = new RoomData();
          data.roominfo[i].entranceDir = theRooms.finalRoomInfoArray[i].entranceDir;
          data.roominfo[i].exitDir     = theRooms.finalRoomInfoArray[i].exitDir;
 ;  
           data.roominfo[i].beenThere   = theRooms.finalRoomInfoArray[i].beenThere;
           data.roominfo[i].roomID      = theRooms.finalRoomInfoArray[i].roomID;
       }

   }

	
 


	public void LoadPlayer()
	{
		 
		player = GameObject.FindGameObjectWithTag("Player");
		if(player!=null)
		{
			eq=player.GetComponent<PlayerEquipment>();
			theStats = player.GetComponent<PlayerStats>();
			
		}

			
				player.GetComponent<Health> ().currentHP = data.health;
				player.GetComponent<PlayerLight> ().currentLight = data.theLight;
			
				theStats.maxHPModifier = data.maxHPModifier;
				theStats.maxLightModifier = data.maxLightModifier;
				theStats.meleeModifier = data.meleeModifier;
				theStats.spellModifier = data.spellModifier;
				Debug.Log (data.embername);
				Debug.Log (data.bootname);
			
				eq.equippedEmber = (ember)data.equippedember;
				eq.equippedBoot = (boot)data.equippedboot;   
				eq.equippedAccessory = (accessory)data.equippedspell;
				eq.AccessoryName = data.embername;
				eq.EmberStat1.StatAmount = data.emberstat1;
				eq.EmberStat1.TheStat = (StatType)data.emberstattype1;
				eq.EmberStat2.StatAmount = data.emberstat2;
				eq.EmberStat1.TheStat = (StatType)data.emberstattype1;
			
				eq.BootName = data.bootname;  
				eq.BootStat1.StatAmount = data.bootstat1;
				eq.BootStat1.TheStat = (StatType)data.boottattype1;
			
				eq.BootStat2.StatAmount = data.bootstat2; 
				eq.BootStat2.TheStat = (StatType)data.boottattype2;
				eq.emberDurability = data.emberdurability;
				Debug.Log (eq.BootName);
				Debug.Log (eq.AccessoryName);
			
				options.easyMode = data.easymode;
			 	  
			

			}


	public void LoadDungeon()
	{ 
		theRooms = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomGeneration>();
				options.easyMode = data.easymode;
				theRooms.currentRoom = data.currentroom;
				theRooms.loadedData = data;
	}

	public void LoadData()
	{
		if (Application.platform == RuntimePlatform.OSXWebPlayer
		    || Application.platform == RuntimePlatform.WindowsWebPlayer)
		{
			Debug.Log("OK Load");
			//Loading Hp and Light
			
		  data.health=      PlayerPrefs.GetFloat("PlayerHealth", 100);
		  data.theLight=      PlayerPrefs.GetFloat("PlayerLight", 100);
			//Loading stats
		data.meleeModifier   =	PlayerPrefs.GetFloat("MeleeMod",0 );
		data.spellModifier   =	PlayerPrefs.GetFloat("SpellMod",0 );
		data.maxLightModifier=	PlayerPrefs.GetFloat("LightMod",0 );
		data.maxHPModifier   =	PlayerPrefs.GetFloat("LifeMod", 0 );
		data.embername           = 		PlayerPrefs.GetString("EmberName", "NoName");
        data.emberstat1 = PlayerPrefs.GetFloat("EmberStat1", 0);
        data.emberstat2 = PlayerPrefs.GetFloat("EmberStat1t", 0);
		data.emberstattype1   = 		PlayerPrefs.GetInt("EmberStat2", 0);
		data.emberstattype2=   PlayerPrefs.GetInt("EmberStat2t",0);
		data.bootname                = 		PlayerPrefs.GetString("BootName","NoName");
        data.bootstat1 = PlayerPrefs.GetFloat("BootStat1", 0);
        data.bootstat2 = PlayerPrefs.GetFloat("BootStat1t", 0);
		data.boottattype1     = 		PlayerPrefs.GetInt("BootStat2",  0);
		data.boottattype2=  PlayerPrefs.GetInt("BootStat2t",0);
		data.equippedspell=  PlayerPrefs.GetInt("Spell",0);
		data.emberdurability= PlayerPrefs.GetInt("Durability",8);
			
			
			//Loading boot and embers
			data.equippedember=	PlayerPrefs.GetInt("Ember",0);
			data.equippedboot  =PlayerPrefs.GetInt("Boot",0 );
			data.currentroom= PlayerPrefs.GetInt("CurrentRoom", 0);
			//Loading the amount of rooms in the roomgenerator
			int teemplenght = PlayerPrefs.GetInt("RoomArrLenght", 0);

			data.amountofrooms=teemplenght;
			Debug.Log("This is the lenght of the array ");
			Debug.Log(teemplenght);
			options.easyMode = PlayerPrefs.GetInt("EasyMode",1) == 1 ? true : false; 
			if (teemplenght != 0)
			{
				
				data.roominfo= new RoomData[teemplenght];
				for (int i = 0; i < teemplenght; i++)
				{
					data.roominfo[i]= new RoomData();
					string temp = "Room_" + i.ToString() + "_been there";
					string temps = "RoomID_" + i.ToString();
					string tempsss= "RoomExitDir_"+ i.ToString();
					string tempss= "RoomEntryDir_"+i.ToString();
					
					data.roominfo[i].beenThere = PlayerPrefs.GetInt(temp,0) == 1 ? true : false;
					data.roominfo[i].roomID = PlayerPrefs.GetInt(temps,0);
					data.roominfo[i].entranceDir= PlayerPrefs.GetInt(tempss,0);
					data.roominfo[i].exitDir=PlayerPrefs.GetInt(tempsss,0);
				}
				
				 
				Debug.Log("THIS SHOULD BE LOADING OK?! I AM REALLY UPPERCASY SO YOU CAN SPOT ME EASIER =D");
			}
			
			
		}
		else
		{
			if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
			{
				BinaryFormatter bin = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
				data = (PlayerData)bin.Deserialize(file);			
				options.easyMode = data.easymode;
				file.Close();
				
				
			}
		}


	}
}

[System.Serializable]
public class PlayerData
{
	public float health;
    public float theLight;
    public float meleeModifier;
    public float spellModifier ;
    public float maxHPModifier ;
    public float maxLightModifier ;
    public bool easymode;
	public string embername;
	public string bootname;
	public float bootstat1;
	public float bootstat2;
	public float emberstat1;
	public float emberstat2;
	public int emberdurability;
    public int amountofrooms;
	public int currentroom;
    public RoomData[] roominfo ;
	public int equippedember;
	public int equippedboot;
	public int equippedspell;
	public int emberstattype1;
	public int emberstattype2;
	public int boottattype1;
	public int boottattype2;
	public bool canyousave;
	public bool canyouload;



}

[System.Serializable]
public class RoomData
{
 
 public int entranceDir;
 public int exitDir;
    public bool beenThere;
    public int roomID;
}