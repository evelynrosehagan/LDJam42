using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour {
    public List<GameObject> bgs;
    float speed = 8f;
    public int translate = 100;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < bgs.Count; i++)
        {
            bgs[i].transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if(bgs[0].transform.position.x < -50)
        {
            bgs[0].transform.Translate(translate, 0, 0);
            GameObject tmp = bgs[0];
            bgs.RemoveAt(0);
            bgs.Add(tmp);
        }
	}
}
