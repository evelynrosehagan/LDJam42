using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public bool DeleteOnStarte = false;
    public enum GameState
    {
        Menu,
        UpgradeMenu,
        Countdown,
        LevelRunning,
        LevelEnd,
        Tutorial,
        BonusLevel,
        Death
    }

    public GameState mystate;

    [SerializeField]
    GameObject DebrisSpawner;
    [SerializeField]
    GameObject Level;
    [SerializeField]
    GameObject Player;
    public int Points;
    public float TargettingTime = 3;

    public float SpeedLevel = 3;
    public int AttackDamage = 1;
    public int InventorySize = 5;
    public int InventoryCurrent = 0;
    public int Coins = 0;

    public int levelnum = 1;
    

    static GameController instance;
	// Use this for initialization

        void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) => GatherReferences();
    }
	void Start () {
        instance = this;
        if(DeleteOnStarte)
        {
            Destroy(this);
        }
        mystate = GameState.Menu;

        countDownTime = 3;
        DontDestroyOnLoad(this.gameObject);
	}

    void Update()
    {
        
        switch(mystate)
        {
            case GameState.Menu:
                Menu();
                break;
            case GameState.Tutorial:
                Tutorial();
                break;
            case GameState.Countdown:
                Countdown();
                break;
            case GameState.LevelRunning:
                LevelRunning();
                break;
            case GameState.LevelEnd:
                LevelEnd();
                break;
            case GameState.Death:
                Death();
                break;
            case GameState.BonusLevel:
                BonusLevel();
                break;
            case GameState.UpgradeMenu:
                UpgradeMenu();
                break;
        }
    }

	public static GameController GetInstance()
    {
        return instance;
    }

    public bool CanPickup()
    {
        return InventoryCurrent < InventorySize;
    }
    public void Pickup(GameObject debris)
    {
        InventoryCurrent++;
        Inventory.instance.AddToInventory(debris);
        Inventory.instance.UpdateInventory();
    }
    public bool Holding()
    {
        return InventoryCurrent > 0;
    }
    
    public void Death()
    {
        if(Input.GetButtonDown("Use"))
        {
            LevelLoader.instance.LoadMainMenu();
        }
    }

    public Inventory.DebrisStructure Drop()
    {
        if(InventoryCurrent>0)
        {
            InventoryCurrent--;
            Inventory.instance.UpdateInventory();
            return Inventory.instance.PopFromInventory();
        }
        return null;
    }
    public void GainPoint()
    {
        Points++;
    }
    public void GainPoint(int points)
    {
        Points+=points;
    }

    public Level GetLevel()
    {
        return Level.GetComponent<Level>();
    }

    void StartLevel()
    {
        DebrisSpawner.GetComponent<DebrisSpawner>().running = true;
        Player.GetComponent<C_Controller>().running = true;
        Level.GetComponent<Level>().running = true;
        Level.GetComponent<Level>().StartTimer();

        SetTargettingTime();
        SetupSpawnTime();
        
        GatherReferences();
    }

    void EndLevel()
    {

        DebrisSpawner.GetComponent<DebrisSpawner>().running = false;
        Player.GetComponent<C_Controller>().running = false;
        Level.GetComponent<Level>().running = false;
    }

    void PauseLevel()
    {
        DebrisSpawner.GetComponent<DebrisSpawner>().running = false;
        Player.GetComponent<C_Controller>().running = false;
        Level.GetComponent<Level>().running = false;
    }


    public float countDownTime = 3;
    void Countdown()
    {
        InventoryCurrent = 0;
        Inventory.instance.Purge();
        Debug.Log("ref " + referencedGathered);
        if (!referencedGathered) GatherReferences();
        countDownTime -= Time.deltaTime;
        if (countDownTime <= 0)
        {
            mystate = GameState.LevelRunning;
            StartLevel();
        }
    }   

    public void KillPlayer()
    {
        if(mystate == GameState.LevelRunning)
        {
            if(bonuslevel)
            {
                Points +=Mathf.RoundToInt( Level.GetComponent<Level>().Timer / 2f);
            }
            mystate = GameState.Death;
            EndLevel();
        }

    }

    public void LevelRunning()
    {
        if(Level.GetComponent<Level>().LevelComplete())
        {
            mystate = GameState.LevelEnd;
            EndLevel();
        }
    }

    public void LevelEnd()
    {
        if(Input.GetButtonDown("Use"))
        {
            levelnum++;
            mystate = GameState.UpgradeMenu;
            LevelLoader.instance.LoadUpgradeShop();
        }
    }

    public void SetTargettingTime()
    {
        switch(levelnum)
        {
            case 1: TargettingTime = 3;
                break;
            case 2: TargettingTime = 2.5f;

                break;
            case 3: TargettingTime = 2.25f;
                break;
            case 4: TargettingTime = 2f;
                break;
            case 5: TargettingTime = 1.5f;
                break;
            case 6:
                TargettingTime = 1.25f;
                break;

            case 7:
                TargettingTime = 1f;
                break;
            case 8:
            case 9:
                TargettingTime = .75f;
                break;
            default:TargettingTime = .5f;
                break;
        }
    }
    void SetupSpawnTime()
    {
        switch (levelnum)
        {
            case 1:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 3f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = 1.5f;
                break;
            case 2:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 2.5f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = 2f;

                break;
            case 3:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 2.25f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = 1.25f;
                break;
            case 4:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 2f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = 1.5f;
                break;
            case 5:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 1.75f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = 1.25f;
                break;
            case 6:
            case 7:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 1.5f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = 1f;
                break;
            case 8:
            case 9:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 1f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = .5f;
                break;
            default:
                DebrisSpawner.GetComponent<DebrisSpawner>().MaxSpawnTime = 1f;
                DebrisSpawner.GetComponent<DebrisSpawner>().MinSpawnTime = .2f;
                break;
        }
    }

    bool referencedGathered = false;
    public void StartCountdown()
    {
        
        referencedGathered = false;
        Debug.Log("Reset");
        countDownTime = 3;
        mystate = GameState.Countdown;
    }

    bool tutorialStart = false;
    
    public void Menu()
    {
       
    }
    bool bonuslevel = false;
    public void BonusLevel()
    {
        bonuslevel = true;

    }

    public void MenuClick()
    {
        tutorialStart = false;
        LevelLoader.instance.StartTutorial();
    }
    public void UpgradeMenu()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!scene.name.Contains("menu"))
        {
            GatherReferences();
        }
    }

    public void GatherReferences()
    {
        instance = this;
        referencedGathered = true;
        Player = GameObject.Find("Player");
        DebrisSpawner = GameObject.Find("DebrisSpawner");
        Level = GameObject.Find("Level");
        
    }

    public void BuyMoveSpeed()
    {
        if (Coins >= Mathf.Round(GameController.GetInstance().SpeedLevel * 1.25f + 1))
        {
            Coins -= Mathf.RoundToInt(GameController.GetInstance().SpeedLevel * 1.25f + 1);
            SpeedLevel += .5f;
        }
    }
    public void BuyAttackDamage()
    {
        if (Coins >= Mathf.Round(GameController.GetInstance().AttackDamage * 1.4f + 3))
        {
            Coins -= Mathf.RoundToInt(GameController.GetInstance().AttackDamage * 1.4f + 3);
            AttackDamage += 1;
        }
    }
    public void BuyInventorySize()
    {
        if (Coins >= Mathf.Round(GameController.GetInstance().InventorySize / 2 + 1))
        {
            Coins -= Mathf.RoundToInt(GameController.GetInstance().InventorySize / 2 + 1);
            InventorySize += 1;
        }
    }

    void Tutorial()
    {
        if (!tutorialStart)
        {
            Player.GetComponent<C_Controller>().running = true;
            tutorialStart = true;
        }
    }

    
}
