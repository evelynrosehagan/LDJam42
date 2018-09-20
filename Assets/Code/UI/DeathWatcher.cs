using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathWatcher : MonoBehaviour {
    public Text text;
    public Image image;
    public Text text2;

    public Text Score;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.GetInstance().mystate == GameController.GameState.Death)
        {
            text.enabled = true;
            text2.enabled = true;
            image.enabled = true;
            string tmp = "";
            if(GameController.GetInstance().levelnum >1)
            {
                tmp = "s";
            }
            Score.text = "You Got " + GameController.GetInstance().Points + " points and survived " + GameController.GetInstance().levelnum + " level" + tmp + "!";
            Score.enabled = true;
           
        }
        else
        {
            text.enabled = false;
            text2.enabled = false;
            image.enabled = false;
            Score.enabled = false;
        }
           
	}
}
