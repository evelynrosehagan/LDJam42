using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveSpeedShop : MonoBehaviour
{
    public Text information;
    public Text cost;
    public Text current;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        current.text = "Current Value: " + GameController.GetInstance().SpeedLevel;
        cost.text = "Cost: " + Mathf.Round(GameController.GetInstance().SpeedLevel * 1.25f + 1).ToString();

    }

    public void BuyMoveSpeed()
    {
        GameController.GetInstance().BuyMoveSpeed();
    }
}
