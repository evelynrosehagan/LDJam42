using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable {

	// Use this for initialization
	void Start () {
        Busy = false;
	}
	
	// Update is called once per frame
	void Update () {
        Busy = interactTarget.BusyCheck();
    }

    public override void Interact()
    {
        if (!Busy)
        {
            SoundPlayer.instance.button.Play();
            interactTarget.Interact();
        }
    }
}
