using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialTextControl : MonoBehaviour {

    public static TutorialTextControl instance;

    Image image;

    public Text text;

	// Use this for initialization
	void Awake () {
        instance = this;
        image = GetComponent<Image>();
        DisableTextBox();

        Debug.Log(text.text);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
            DisableTextBox();
	}

    public void EnableTextBox(string value)
    {
        GameObject.Find("Player").GetComponent<C_Controller>().running = false;
        image.enabled = true;
        text.enabled = true;
        string newval = "";
        string[] tmp = value.Split(';');
        for(int i = 0; i < tmp.Length; i++)
        {
            newval += tmp[i] + Environment.NewLine;
        }
        text.text = newval + "Press E to continue";
    }
    public void DisableTextBox()
    {
        GameObject.Find("Player").GetComponent<C_Controller>().running = true;
        image.enabled = false;
        text.enabled = false;
    }
}
