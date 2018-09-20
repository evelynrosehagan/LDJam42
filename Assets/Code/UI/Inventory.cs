using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public class DebrisStructure
    {
        public int score;
        public Sprite sprite;
    }

    List<DebrisStructure> inventorylist;

    public static Inventory instance;
    List<GameObject> items;
    [SerializeField]
    GameObject ItemPrefab;
	// Use this for initialization
	void Start () {
        inventorylist = new List<DebrisStructure>();
        instance = this;
        items = new List<GameObject>();
        for(int i = 0; i < 5; i++)
        {
            GameObject pref = Instantiate(ItemPrefab, this.transform);
            pref.GetComponent<RectTransform>().anchoredPosition = new Vector2(-135 + i * 25, 0);
            items.Add(pref);
        }
        UpdateInventory();
	}
	
	// Update is called once per frame
	public void UpdateInventory()
    {
        for(int i = 0; i < GameController.GetInstance().InventorySize; i++)
        {
            if(i >= items.Count)
            {
                GameObject pref = Instantiate(ItemPrefab, this.transform);
                pref.GetComponent<RectTransform>().anchoredPosition = new Vector2(-135 + i * 25, 0);
                items.Add(pref);
            }
            if(i < GameController.GetInstance().InventoryCurrent)
            {
                items[i].GetComponent<InventoryItem>().SetFull();
            }
            else
            {
                items[i].GetComponent<InventoryItem>().SetEmpty();
            }
        }
    }

    public void AddToInventory(GameObject debris)
    {
        DebrisStructure debrisStructure = new DebrisStructure();
        Debris debnfo = debris.GetComponent<Debris>();
        debrisStructure.score = debnfo.pointvalue;
        debrisStructure.sprite = debnfo.GetComponent<SpriteRenderer>().sprite;
        inventorylist.Add(debrisStructure);
    }

    public DebrisStructure PopFromInventory()
    {
        if (inventorylist.Count == 0)
            return null;
        DebrisStructure str = inventorylist[inventorylist.Count - 1];
        inventorylist.RemoveAt(inventorylist.Count - 1);
        return str;
    }

    public void Purge()
    {
        while (inventorylist.Count > 0)
            inventorylist.RemoveAt(0);
        while (items.Count > 0)
            items.RemoveAt(0);
        for (int i = 0; i < GameController.GetInstance().InventorySize; i++)
        {
            GameObject pref = Instantiate(ItemPrefab, this.transform);
            pref.GetComponent<RectTransform>().anchoredPosition = new Vector2(-135 + i * 25, 0);
            items.Add(pref);
        }
        UpdateInventory();
        
    }
}
