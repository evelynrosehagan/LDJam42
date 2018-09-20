using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    BoxCollider2D coll;

    Vector2 direction;

    static int damage = 1;

    float speed = 8f;

	// Use this for initialization
	void Start () {
        damage = GameController.GetInstance().AttackDamage;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
        
	}

    public void setDirection(Vector2 idirection)
    {
        direction = idirection;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Debris")
        {
            IDamageable damageable = col.transform.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.Damage(damage);
        }
        if(col.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
