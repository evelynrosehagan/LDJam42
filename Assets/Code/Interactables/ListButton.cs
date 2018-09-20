using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : Interactable {

    public List<InteractTarget> targets;

    // Use this for initialization
    void Start()
    {
        Busy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].Interact();
        }
    }

}
