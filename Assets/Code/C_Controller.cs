 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Controller : MonoBehaviour {
    public bool running = false;

    Vector2 direction;
    Rigidbody2D mybody;
    BoxCollider2D bcollider;
    [SerializeField]
    float playerspeed = 3f;
    [SerializeField]
    LayerMask obsticleMask;

    [SerializeField]
    LayerMask dropMask;
    // Use this for initialization

    [SerializeField]
    GameObject Sprite;

    [SerializeField]
    GameObject consumedDebris;
    [SerializeField]
    Gun gun;
	void Start () {
        direction = Vector2.zero;
        mybody = GetComponent<Rigidbody2D>();
        bcollider = GetComponent<BoxCollider2D>();
    }
    float shootTimer = 1000;
    float maxshoot = .25f;
	// Update is called once per frame
	void Update () {
        
        if(running)
        {
            Vector3 mypos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 mousedir = Input.mousePosition - mypos;
            float angle = Mathf.Atan2(mousedir.y, mousedir.x) * Mathf.Rad2Deg - 90;
            float x;
            float y;

            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            bool interact = Input.GetButtonDown("Use");
            bool shoot = Input.GetButtonDown("Fire1");
            bool pickup = Input.GetButtonDown("Pickup");
            bool drop = Input.GetButtonDown("Drop");

            Sprite.transform.rotation = Quaternion.AngleAxis(angle, Sprite.transform.forward);

            //SetPlayerRotation(new Vector2(x, y));
            if (interact)
                Interact();

            if (!checkVerticalMove(y))
                y = 0;
            if (!checkHorizontalMove(x))
                x = 0;
            playerspeed = GameController.GetInstance().SpeedLevel;


            direction = new Vector2(x, y);
            direction *= Time.deltaTime * playerspeed * 100;
            direction.x = Mathf.Round(direction.x) / 100;
            direction.y = Mathf.Round(direction.y) / 100;
            if (interact)
                Interact();
            transform.Translate(direction);

            if (shoot)
            {
                if(shootTimer > maxshoot)
                {
                    SoundPlayer.instance.shoot.Play();
                    gun.Shoot(Sprite.transform.up);
                    shootTimer = 0;
                }

            }

            if (pickup)
            {
                Pickup();
            }
            if (drop)
            {
                Drop();
            }
            shootTimer += Time.deltaTime;
        }

    }
    
    void SetPlayerRotation(Vector2 direction)
    {
        if(!(direction.x ==0 && direction.y ==0))
        {
            
            if(direction.x == 0 && direction.y > 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.x == 0 && direction.y < 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (direction.x > 0 && direction.y == 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (direction.x < 0 && direction.y == 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (direction.x < 0 && direction.y > 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (direction.x > 0 && direction.y > 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 315);
            }
            else if (direction.x < 0 && direction.y < 0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            else if (direction.x > 0 && direction.y <0)
            {
                Sprite.transform.rotation = Quaternion.Euler(0, 0, 225);
            }
        }
    }

    bool checkHorizontalMove(float xaxis)
    {
        bool retval = false;
        if (xaxis > 0)
        {
            Vector2 currpos = transform.position;
            currpos.x += bcollider.size.x / 2;
            currpos.y += bcollider.size.y / 2;

            bool tr = Physics2D.Raycast(currpos, Vector2.right, .05f, obsticleMask);
            currpos.y -= bcollider.size.y / 2;
            bool mr = Physics2D.Raycast(currpos, Vector2.right, .05f, obsticleMask);
            currpos.y -= bcollider.size.y / 2;
            bool br = Physics2D.Raycast(currpos, Vector2.right, .05f, obsticleMask);

            retval = !(br || tr || mr);

        }
        else if (xaxis < 0)
        {
            Vector2 currpos = transform.position;
            currpos.x -= bcollider.size.x / 2;
            currpos.y += bcollider.size.y / 2;

            bool tr = Physics2D.Raycast(currpos, Vector2.left, .05f, obsticleMask);
            currpos.y -= bcollider.size.y / 2;
            bool mr = Physics2D.Raycast(currpos, Vector2.left, .05f, obsticleMask);
            currpos.y -= bcollider.size.y / 2;
            bool br = Physics2D.Raycast(currpos, Vector2.left, .05f, obsticleMask);

            retval = !(br || tr || mr);
        }
        else
            return true;
        return retval;
    }

    bool checkVerticalMove(float yaxis)
    {
        bool retval = false;
        if (yaxis > 0)
        {
            Vector2 currpos = transform.position;
            currpos.x -= bcollider.size.x / 2;
            currpos.y += bcollider.size.y / 2;

            bool tr = Physics2D.Raycast(currpos, Vector2.up, .05f, obsticleMask);
            currpos.x += bcollider.size.x / 2;
            bool mr = Physics2D.Raycast(currpos, Vector2.up, .05f, obsticleMask);
            currpos.x+= bcollider.size.x / 2;
            bool br = Physics2D.Raycast(currpos, Vector2.up, .05f, obsticleMask);

            retval = !(br || tr || mr);
            

        }
        else if (yaxis < 0)
        {
            Vector2 currpos = transform.position;
            currpos.x -= bcollider.size.x / 2;
            currpos.y -= bcollider.size.y / 2;

            bool tr = Physics2D.Raycast(currpos, Vector2.down, .05f, obsticleMask);
            currpos.x += bcollider.size.x / 2;
            bool mr = Physics2D.Raycast(currpos, Vector2.down, .05f, obsticleMask);
            currpos.x += bcollider.size.x / 2;
            bool br = Physics2D.Raycast(currpos, Vector2.down, .05f, obsticleMask);

            retval = !(br || tr || mr);
        }
        else
            return false;
        return retval;
    }

    public void Interact()
    {
        //Debug.Log("Interacting");
        RaycastHit2D[] hits;
        hits = Physics2D.CircleCastAll(transform.position, 2f, Vector2.zero, .1f, obsticleMask);
        IInteractable interactable;
        for (int i = 0; i < hits.Length; i++)
        {
            interactable = hits[i].transform.gameObject.GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact();
                //Debug.Log("Hit");
            }
        }
    }

    void Pickup()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Sprite.transform.up, 2f,obsticleMask);

        if(hit1)
        {
            if(hit1.transform.tag == "Debris")
            {
                if (hit1.transform.GetComponent<IDamageable>() != null && hit1.transform.GetComponent<IDamageable>().Destroyed())
                {
                    if (GameController.GetInstance().CanPickup())
                    {
                        SoundPlayer.instance.crateP.Play();
                        GameController.GetInstance().Pickup(hit1.transform.gameObject);
                        Destroy(hit1.transform.gameObject);
                        return;
                    }
                    
                }
            }
        }

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2, Vector2.zero, .1f, obsticleMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == "Debris")
            {
                if (hits[i].transform.GetComponent<IDamageable>() != null && hits[i].transform.GetComponent<IDamageable>().Destroyed())
                {
                    if (GameController.GetInstance().CanPickup())
                    {
                        GameController.GetInstance().Pickup(hits[i].transform.gameObject);
                        Destroy(hits[i].transform.gameObject);

                    }
                    i = 1000;
                }


            }

        }
    }

    void Drop()
    {
        if(GameController.GetInstance().Holding())
        {
            Vector2 targetPos = Sprite.transform.position + Sprite.transform.up;
            float xadditive = 0;
            if (Sprite.transform.up.x > 0) xadditive = 1; else xadditive = -1;
            float yadditive = 0;
            if (Sprite.transform.up.y > 0) yadditive = 1; else yadditive = -1;
            //if (Sprite.transform.up.x > 0) targetPos.x += .15f; else targetPos.x -= .15f;
            //if (Sprite.transform.up.y > 0) targetPos.y += .15f; else targetPos.y -= .15f;
            targetPos.x = (Mathf.Round(targetPos.x) + .5f * xadditive );
            targetPos.y = (Mathf.Round(targetPos.y) + .5f * yadditive);
            if (!Level.instance.halfoffsetx)
                targetPos.x -= .5f;
            if (!Level.instance.halfoffsety)
                targetPos.y -= .5f;

            
            if(!Physics2D.BoxCast(targetPos, new Vector2(.85f,.85f), 0, Vector2.zero, 0, dropMask))
            {

                SoundPlayer.instance.crateD.Play();
                GameObject tmpcrate = Instantiate(consumedDebris, targetPos, Quaternion.identity, null);
                Inventory.DebrisStructure str = GameController.GetInstance().Drop();
                tmpcrate.GetComponent<Debris>().pointvalue = str.score;
                tmpcrate.GetComponent<SpriteRenderer>().sprite = str.sprite;
                tmpcrate.GetComponent<Debris>().StartDamaged();
            }
            else
            {
                
            }
        }

        
    }
}
