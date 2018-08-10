using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlMgr : MonoBehaviour
{
    [SerializeField] string playButton, menuButton, rouletteScene, lvl1, lvl2;

    public static LvlMgr instance;
    public static bool unpause;

    void Awake()
    {
        unpause = true;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(playButton);
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuButton);
    }

    public void RouletteScene()
    {
        SceneManager.LoadScene(rouletteScene);
    }

    public void PauseButton()
    {
        unpause = false;
        Debug.Log(unpause);
        Time.timeScale = 0;
        CanvasMgr.unnpause = 1;
    }
    
    public void ResumeButton()
    {
        CanvasMgr.unnpause = 0;
        Time.timeScale = 1;        
        Invoke("waitForShoot", 0.1f);
    }

    void waitForShoot()
    {
        unpause = true;
    }

    public void FirstLvl()
    {
        SceneManager.LoadScene(lvl1);
    }

    public void SecondLvl()
    {
        SceneManager.LoadScene(lvl2);
    }
}
