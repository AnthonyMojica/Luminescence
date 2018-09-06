using UnityEngine;
using System.Collections;

public class MusicVolumeManager : MonoBehaviour {

    AudioSource audioPlayer;
    Options options;
    public AudioClip[] clips;
    GameObject dungeon;
    RoomGeneration theRooms;

    // Use this for initialization
    void Start()
    {
        options = GameObject.Find("TheOptions").GetComponent<Options>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
        audioPlayer.volume = options.musicVolume * 0.01f;
    
    }

    // Update is called once per frame
    void ChangeVolume()
    {
        audioPlayer.volume = options.musicVolume * 0.01f;

    }
  
}
