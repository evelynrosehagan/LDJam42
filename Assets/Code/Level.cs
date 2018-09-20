using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public static Level instance;
    public float Timer;
    public float TimeMax;
    public bool halfoffsetx = true;
    public bool halfoffsety = true;
    public bool running = false;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if(running)
        {
            Timer += Time.deltaTime;
        }
	}
    
    public void StartTimer()
    {
        Timer = 0;
        running = true;
    }

    public bool LevelComplete()
    {
        if(Timer > TimeMax)
        {
            return true;
        }
        return false;
    }

    public string GetTimeString()
    {
        return Timer.ToString("0.0") + "/" + TimeMax.ToString("0.0");
    }

    
}
