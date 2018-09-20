using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : InteractTarget
{
    [SerializeField]
    LayerMask PlayerLayer;

    [SerializeField]
    SlidingDoor AirlockDoor;

    [SerializeField]
    SlidingDoor AltAirlockDoor;

    BoxCollider2D coll;

    public void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    public override bool BusyCheck()
    {
        if(AltAirlockDoor !=null)
        {
            return AirlockDoor.GetState() != SlidingDoor.state.RestClose || AltAirlockDoor.GetState() != SlidingDoor.state.RestClose;
        }
        return AirlockDoor.GetState() != SlidingDoor.state.RestClose;
    }

    public void Update()
    {
        if (AirlockDoor.GetState() == SlidingDoor.state.Closing && Physics2D.BoxCast(coll.transform.position, coll.size, 0f, Vector2.zero, .1f, PlayerLayer))
        {
            AirlockDoor.ForceOpen();
        }

    }

    public override void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(coll.transform.position, coll.size, 0f, Vector2.zero, .1f);
        if (BusyCheck()) return;
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == "Debris")
            {
                if(!hits[i].transform.GetComponent<Debris>().launched)
                    GameController.GetInstance().GainPoint(hits[i].transform.GetComponent<Debris>().pointvalue);
                hits[i].transform.GetComponent<Debris>().Launch();


            }
        }
        SoundPlayer.instance.vent.Play();
    }

}
