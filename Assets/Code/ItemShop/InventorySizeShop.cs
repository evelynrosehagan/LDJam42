using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySizeShop : MonoBehaviour {
    public Text information;
    public Text cost;
    public Text current;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        current.text = "Current Value: " + GameController.GetInstance().InventorySize;
        cost.text = "Cost: " +Mathf.Round(GameController.GetInstance().InventorySize / 2 + 1).ToString();

    }
    public void BuyInventorySize()
    {
        GameController.GetInstance().BuyInventorySize();
    }
}
