using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisInfo : MonoBehaviour {
    public List<GameObject> DebrisList;
    public static DebrisInfo instance;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
}
