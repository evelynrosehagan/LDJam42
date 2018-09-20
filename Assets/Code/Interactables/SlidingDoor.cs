using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : InteractTarget {
    [SerializeField]
    GameObject Top;
    [SerializeField]
    GameObject Bottom;

    [SerializeField]
    LayerMask hitmask;

    bool busy;

    float currDist = 0;

    [SerializeField]
    float maxDist;
    [SerializeField]
    float maxTime;

    BoxCollider2D coll;

    float currTime;
    public enum state
    {
        Opening,
        Closing,
        RestOpen,
        RestClose
    }
    public state mystate;

    public void Start()
    {
        mystate = state.RestClose;
        coll = GetComponent<BoxCollider2D>();
    }
    public override void Interact()
    {
        if (mystate == state.RestClose) { mystate = state.Opening; currDist = 0; currTime = 0; }
        
        else if (mystate == state.RestOpen) {mystate = state.Closing; currDist = maxDist; currTime = 0; }
    }
   
    public void Update()
    {
        switch(mystate)
        {
            case state.Closing: Close(); busy = true; ClosingCast();
                 break;
            case state.Opening: Open(); busy = true; break;
            default: busy = false;break;
        }
    }

    public override bool BusyCheck()
    {
        return busy;
    }

    void Open()
    {
        Vector2 toppos = Top.transform.localPosition;
        Vector2 botpos = Bottom.transform.localPosition;
        toppos.y = Mathf.Lerp(maxDist / 2, maxDist + maxDist / 2, currTime / maxTime);
        botpos.y = Mathf.Lerp(-maxDist / 2, -maxDist - maxDist / 2, currTime / maxTime);

        if(currTime> maxTime)
        {
            toppos.y = maxDist + maxDist / 2;
            botpos.y = -maxDist - maxDist / 2;
            mystate = state.RestOpen;
        }

        Top.transform.localPosition = toppos;
        Bottom.transform.localPosition = botpos;

        currTime += Time.deltaTime;

    }

    void Close()
    {
        Vector2 toppos = Top.transform.localPosition;
        Vector2 botpos = Bottom.transform.localPosition;
        toppos.y = Mathf.Lerp(maxDist + maxDist / 2, maxDist / 2, currTime / maxTime);
        botpos.y = Mathf.Lerp(-maxDist - maxDist / 2 , - maxDist / 2, currTime / maxTime);

        if (currTime > maxTime)
        {
            toppos.y = maxDist / 2;
            botpos.y = -maxDist / 2;
            mystate = state.RestClose;
        }

        Top.transform.localPosition = toppos;
        Bottom.transform.localPosition = botpos;

        currTime += Time.deltaTime;
    }

    void ClosingCast()
    {
        Vector2 size = coll.size;
        Vector2 off = coll.offset;
        if(transform.rotation !=Quaternion.identity)
        {
            float tmp = size.x;
            size.x = size.y;
            size.y = tmp;
            tmp = off.x;
            off.x = off.y;
            off.y = tmp;


        }
        Vector2 position = transform.position;
        RaycastHit2D[] hit = Physics2D.BoxCastAll(position  + off, size, 0, Vector2.right, .01f, hitmask);
        
        for(int i = 0; i < hit.Length; i++)
        {
            
            if (hit[i].transform != Top.transform && hit[i].transform != Bottom.transform && hit[i].transform.tag != "Door" && hit[i].transform.tag != "WorldGeo")
            {
                Debug.Log("Name: " + hit[i].transform.name);
                mystate = state.Opening;
                currTime = maxTime - currTime;
                i = 1000;
            }
        }
    }
    public void ForceOpen()
    {
        mystate = state.Opening;
        currTime = maxTime - currTime;
    }
    public state GetState()
    {
        return mystate;
    }
   
}
