using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public static LevelLoader instance;
	// Use this for initialization
	void Start () {
        instance = this;
        DontDestroyOnLoad(this);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
        GameController.GetInstance().StartCountdown();
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        GameController.GetInstance().mystate = GameController.GameState.Tutorial;
    }

    public void LoadNext()
    {
        if(GameController.GetInstance().levelnum == 11)
        {
            SceneManager.LoadScene("BonusLevel"); GameController.GetInstance().mystate = GameController.GameState.BonusLevel; return;
        }
        switch(((GameController.GetInstance().levelnum-1)%5) +1)
        {
            case 1: SceneManager.LoadScene("Level 1"); break;
            case 2: SceneManager.LoadScene("Level 2"); break;
            case 3: SceneManager.LoadScene("Level 3"); break;
            case 4: SceneManager.LoadScene("Level 4"); break;
            case 5: SceneManager.LoadScene("Level 5"); break;
            case 11: SceneManager.LoadScene("BonusLevel"); GameController.GetInstance().mystate = GameController.GameState.BonusLevel; return; break;
            default: SceneManager.LoadScene("Level 2"); break;

        }
        GameController.GetInstance().StartCountdown();
    }

    public void LoadUpgradeShop()
    {
        SceneManager.LoadScene("UpgradeMenu");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(SoundPlayer.instance.gameObject);
        GameObject target = GameController.GetInstance().gameObject;
        Destroy(GameController.GetInstance());
        Destroy(target);
        Destroy(this.gameObject);
    }

}
