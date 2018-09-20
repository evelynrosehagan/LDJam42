using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, IDamageable {
    [SerializeField]
    int health = 3;
    [SerializeField]
    public int pointvalue = 1;
    [SerializeField]
    LayerMask playermask;

    public bool startdam = false;

    public bool launched = false;
    float speed = 10f;


    [SerializeField]
    Material damagedMat;

	// Use this for initialization
	void Start () {
        if (startdam)
            StartDamaged();
	}
	
	// Update is called once per frame
	void Update () {
        if (launched)
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
	}

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0 && health + damage > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
            SoundPlayer.instance.cratedamage.Play();
        }
    }

    public bool Destroyed()
    {
        return health <= 0;
    }

    public void StartDamaged()
    {
        health = 0;
        GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            Vector2 size = GetComponent<BoxCollider2D>().size;
            size.x -= .25f * size.x;
            size.y -= .25f * size.y;
            if (Physics2D.OverlapBox(this.transform.position, size, 0, playermask))
            {
                GameController.GetInstance().KillPlayer();
            }
        }
    }

    public void Launch()
    {
        launched = true;
    }
}
