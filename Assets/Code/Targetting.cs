using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour {
    public float maxTime = 3f;
    float currTime = 0;

    [SerializeField]
    public GameObject Debris;
	// Use this for initialization
	void Start () {
        maxTime = GameController.GetInstance().TargettingTime;
	}
	
	// Update is called once per frame
	void Update () {
        currTime += Time.deltaTime;

        if(currTime>maxTime)
        {
            Vector2 pos = transform.position;
            Instantiate(Debris, pos - Debris.GetComponent<BoxCollider2D>().offset, Quaternion.identity, null);
            Destroy(this.gameObject);
        }
	}
}
