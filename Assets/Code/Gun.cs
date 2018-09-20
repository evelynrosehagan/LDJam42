using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform firePoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot(Vector2 direction)
    {
        GameObject proj = Instantiate(projectile, firePoint.position, Quaternion.identity, null);
        Projectile p  = proj.AddComponent<Projectile>();
        p.setDirection(direction);
        
    }
}
