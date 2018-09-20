using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeTracker : MonoBehaviour {
    Text text;
    Level level;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.GetInstance().mystate == GameController.GameState.LevelRunning)
        {
            text.text = "Time: " + GameController.GetInstance().GetLevel().GetTimeString();
        }
        else text.text = "";
	}
}
