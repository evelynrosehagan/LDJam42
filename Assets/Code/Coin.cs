using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{



    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            SoundPlayer.instance.coin.Play();
            GameController.GetInstance().Coins++;
            Destroy(this.gameObject);
        }
    }
}
