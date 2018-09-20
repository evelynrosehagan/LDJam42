using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicMuter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public SoundPlayer sp;
    public Text text;
    public void MuteMusic()
    {
        text.text = sp.MuteMusic();
    }
}
