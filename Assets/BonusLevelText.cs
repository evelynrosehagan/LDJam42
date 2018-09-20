using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusLevelText : MonoBehaviour {

    public Text text;
    public Image image;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.GetInstance().mystate == GameController.GameState.BonusLevel)
        {
            text.enabled = true;
            image.enabled = true;

            if(Input.GetButtonDown("Use"))
            {
                GameController.GetInstance().StartCountdown();
            }
        }
        else
        {
            text.enabled = false;
            image.enabled = false;
        }
	}
}
