using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {
    public AudioClip[] clips;
    GameObject dungeon;
  public  RoomGeneration theRooms;
    GameObject player;
  public AudioSource audioPlayer;
    bool beenAssigned;
   // public int volume;
	bool once=true;


    private static BGM _instance;

    public static BGM instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BGM>();
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
	// Use this for initialization
	void Start () {
        audioPlayer = gameObject.GetComponent<AudioSource>();
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        DontDestroyOnLoad(this.gameObject);
        beenAssigned = false;
	
	}
	
	// Update is called once per frame
    void Update()
    {
        //***************************************************************   
        //This volume code was breaking the pause menu, the volume is managaed entirely in the MusicVolumeManager Script
        //*************************************************************

        if(Application.loadedLevelName=="Credits")
       {
			audioPlayer.Stop();
//volume = GameObject.FindObjectOfType<Options>().musicVolume;
  //         audioPlayer.volume = 0;
       }
        //if (Application.loadedLevelName == "LoadScreen")
        //{
        //    volume = GameObject.FindObjectOfType<Options>().musicVolume;
        //    audioPlayer.volume = 0;
        //}
        //if(Application.loadedLevelName=="MainMenu"&&audioPlayer.volume==0 )
        //{
        //    audioPlayer.Stop();
        //   audioPlayer.volume = volume*0.01f;
        //    audioPlayer.clip = clips[4];
        //    audioPlayer.Play();
        //}
        //if (Application.loadedLevelName == "IntroCutscene"&&audioPlayer.volume==0)
        //{
		 
        //    volume = GameObject.FindObjectOfType<Options>().musicVolume;
        //    audioPlayer.volume = volume*0.01f;
		 
		 
        //}


        if (Application.loadedLevelName=="MainMenu")
        {
         if(!audioPlayer.isPlaying)
			   {
                audioPlayer.Stop();
                audioPlayer.clip = clips[4];
                audioPlayer.Play();
			}
            

        }
        if (!beenAssigned && Application.loadedLevelName == "Game")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            beenAssigned = true;
        }
        if (Application.loadedLevelName == "LoadScreen")
        {
            audioPlayer.Stop();
        }
        if (Application.loadedLevelName == "IntroCutscene")
        {
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.Play();
                
            }


        }

        if( dungeon==null)
        {
            dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            if (dungeon != null)
            {
                theRooms = dungeon.GetComponent<RoomGeneration>();
            }
        }
        if(player!=null)
        {
            if (!player.GetComponent<Health>().playerDead)
            {
                if (theRooms != null)
                {


                  
                    //if (volume != audioPlayer.volume)
                    //{
                    //    GameObject.FindObjectOfType<Options>().musicVolume = volume;
                    //    audioPlayer.volume = volume * 0.01f;
                    
                     
                    //}

                    if (!GameObject.FindObjectOfType<Options>().easyMode)
                    {
                       

                        if (theRooms.currentRoom < 8 && audioPlayer.clip != clips[0])
                        {
                            audioPlayer.Stop();
                            audioPlayer.clip = clips[0];
							once=true;
							
                            audioPlayer.Play();


                        }
                        else if ((theRooms.currentRoom == 8 || theRooms.currentRoom == 17 )&& audioPlayer.clip != clips[2]&&once==true)
                        {
                            audioPlayer.Stop();
                            audioPlayer.clip = clips[2];
                            audioPlayer.Play();
							once=false;

                        }
                        else if (theRooms.currentRoom > 8 && audioPlayer.clip != clips[1]&&theRooms.currentRoom!=8&&theRooms.currentRoom!=17&&theRooms.currentRoom!=26)
                        {
                            audioPlayer.Stop();
							once=true;						
                            audioPlayer.clip = clips[1];
                            audioPlayer.Play();


                        }
                        else if (theRooms.currentRoom == 26 && audioPlayer.clip != clips[3]&&once==true)
                        {
                            audioPlayer.Stop();
                            audioPlayer.clip = clips[3];
							once=false;
                            audioPlayer.Play();
                        }

                    }
                    else
                    {
                        if (theRooms.currentRoom < 10 && audioPlayer.clip != clips[0])
                        {
                            audioPlayer.Stop();
                            audioPlayer.clip = clips[0];
							once=true;
                            audioPlayer.Play();


                        }
                        else if ((theRooms.currentRoom == 10 || theRooms.currentRoom == 21) && audioPlayer.clip != clips[2]&&once==true)
                        {
                            audioPlayer.Stop();
                            audioPlayer.clip = clips[2];
                            audioPlayer.Play();
                            once = false;

                        }
                        else if (theRooms.currentRoom > 10 && audioPlayer.clip != clips[1]&&theRooms.currentRoom!=10&&theRooms.currentRoom!=21&&theRooms.currentRoom!=32)
                        {
                            audioPlayer.Stop();
							once=true;
                            audioPlayer.clip = clips[1];
                            audioPlayer.Play();


                        }
                        else if (theRooms.currentRoom == 32 && audioPlayer.clip != clips[3]&&once==true)
                        {
                            audioPlayer.Stop();
							once=false;
                            audioPlayer.clip = clips[3];
                            audioPlayer.Play();
                        }

                    }
                }
            }

   }
        else
        {
            beenAssigned = false;
        }



    }
	public void bossmusic()
	{
		audioPlayer.Stop();
		Debug.Log ("Trying");
		audioPlayer.clip = clips[2];
		Debug.Log ("Changed");
		audioPlayer.Play();
		Debug.Log ("Playing");

	}
	public void finalbossmusic()
	{

		audioPlayer.Stop();
		audioPlayer.clip = clips[3];
		audioPlayer.Play();
	}
 void SetToMenu()
    {
        audioPlayer.Stop();
        audioPlayer.clip = clips[4];
        audioPlayer.Play();
    }
    
}
