using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    public static SoundPlayer instance;
    public AudioSource coin;
    public AudioSource crateP;
    public AudioSource crateD;
    public AudioSource button;
    public AudioSource menu;
    public AudioSource death;
    public AudioSource cratedamage;
    public AudioSource shoot;
    public AudioSource music;
    public AudioSource vent;
	// Use this for initialization
	void Start () {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string MuteMusic()
    {
        if (!music.mute)
        {
            music.mute = true;
            return "Unmute Music";
        }
        else
        {
            music.mute = false;
            return "Mute Music";
        }
    }
}
