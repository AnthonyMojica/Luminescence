using UnityEngine;
using System.Collections;

public class SfxVolumeManager : MonoBehaviour {

    AudioSource audioPlayer;
    Options options;

    // Use this for initialization
    void Start()
    {
        options = GameObject.Find("TheOptions").GetComponent<Options>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
        audioPlayer.volume = options.sfxVolume * 0.01f;
    }

    // Update is called once per frame
    void ChangeVolume()
    {
        audioPlayer.volume = options.sfxVolume * 0.01f;

    }
}
