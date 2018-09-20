using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour {
    [SerializeField]
    Sprite empty;
    [SerializeField]
    Sprite full;

    public void SetEmpty()
    {
        GetComponent<Image>().sprite = empty;
    }
    public void SetFull()
    {
        GetComponent<Image>().sprite = full;
    }
}
