using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour {

    public LayerMask obsticleMask;

    public BoxCollider2D FirstSpawner;

    public BoxCollider2D SecondSpawner;

    public bool running = false;

    public GameObject debris;
    public GameObject coin;
    public GameObject targetting;

    public float MaxSpawnTime = 1f;
    public float MinSpawnTime = .5f;

    public float currSpawnTime = 3f;
    public float currDuration = 0;

	// Use this for initialization
	void Start () {
		
	}
    int DebrisLevel;

    void GetDebrisLevel()
    {
        switch(GameController.GetInstance().levelnum)
        {
            case 1:
                DebrisLevel = 1;
                break;
            case 2:
                DebrisLevel = 2;
                break;
            case 3:
            case 4:
                DebrisLevel = 4;
                break;
            case 5:
                DebrisLevel = 6;
                break;
            default: DebrisLevel = 6;
                break;
        }
    }

	// Update is called once per frame
	void Update () {
        if(running)
        {
            GetDebrisLevel();
            currDuration += Time.deltaTime;

            if (currDuration > currSpawnTime)
            {
                SpawnDebris();
                currSpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
                currDuration = 0;
            }
        }
	}

    public void SpawnDebris()
    {

        bool done = false;
        int attempts = 0;
        while (!done && attempts < 50)
        {
            debris = DebrisInfo.instance.DebrisList[Random.Range(0, DebrisLevel)];
            Vector2 position = FirstSpawner.transform.position;
            position.x -= FirstSpawner.size.x / 2;
            position.y -= FirstSpawner.size.y / 2;

            position.x += Mathf.RoundToInt(Random.Range(0, FirstSpawner.size.x - 1)) + .5f;
            position.y += Mathf.RoundToInt(Random.Range(0, FirstSpawner.size.y - 1)) + .5f;
            
            Vector2 offset = debris.GetComponent<BoxCollider2D>().offset;
            Vector2 size = debris.GetComponent<BoxCollider2D>().size;
            size.x -= .5f;
            size.y -= .5f;
            if (!Physics2D.BoxCast(position + offset, size, 0, Vector2.zero, .1f, obsticleMask))
            {
                if(Random.Range(1, 11) == 10)
                {

                    Instantiate(coin, position, Quaternion.identity, null);
                    return;
                }
                else
                {
                    GameObject target = Instantiate(targetting, position + offset, Quaternion.identity, null);
                    Targetting tg = target.GetComponent<Targetting>();
                    tg.Debris = debris;
                    size.x += .5f;
                    size.y += .5f;
                    tg.transform.localScale = size;
                }
                done = true;
            }
                //        Vector2 position = FirstSpawner.transform.position;
                //        position.x -= FirstSpawner.size.x / 2;
                //        position.y -= FirstSpawner.size.y / 2;

                //        position.x += Mathf.RoundToInt( Random.Range(0, FirstSpawner.size.x-1))+.5f;
                //        position.y += Mathf.RoundToInt(Random.Range(0, FirstSpawner.size.y-1))+.5f;

                //        if (!Physics2D.BoxCast(position, new Vector2(.5f ,.5f), 0, Vector2.zero, .1f, obsticleMask ))
                //        {
                //            done = true;
                //            if(Random.Range(1,11) == 10)
                //            {
                //                Instantiate(coin, position, Quaternion.identity, null);
                //            }
                //            else
                //                Instantiate(debris, position, Quaternion.identity, null);
                //            //Debug.Log(position);
                //            currSpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
                //        }
                attempts++;
        }
    }
}
