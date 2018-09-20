using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponDamageShop : MonoBehaviour {
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
        current.text = "Current Value: " + GameController.GetInstance().AttackDamage;
        cost.text = "Cost: " + Mathf.Round(GameController.GetInstance().AttackDamage*1.4f + 3).ToString();

    }

    public void BuyDamage()
    {
        GameController.GetInstance().BuyAttackDamage();
    }
}
