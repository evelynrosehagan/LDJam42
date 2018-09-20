using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountdownTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.GetInstance().mystate == GameController.GameState.Countdown)
        {
            GetComponent<Text>().enabled = true;
            GetComponent<Text>().text = Mathf.Round(GameController.GetInstance().countDownTime + .5f).ToString();
        }
        else
        {
            GetComponent<Text>().enabled = false;
        }
	}
}
