using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelCompleteWatcher : MonoBehaviour {

    [SerializeField]
    Text levelcomptext;
    [SerializeField]
    Text pressetext;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.GetInstance().mystate == GameController.GameState.LevelEnd)
        {
            levelcomptext.enabled = true;
            pressetext.enabled = true;
        }
        else
        {
            levelcomptext.enabled = false;
            pressetext.enabled = false;
        }
    }
}
