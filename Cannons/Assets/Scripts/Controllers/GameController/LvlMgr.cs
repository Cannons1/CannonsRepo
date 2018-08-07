using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlMgr : MonoBehaviour
{
    [SerializeField] string playButton, menuButton, rouletteScene;

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
        Time.timeScale = 0;
    }
    
    public void ResumeButton()
    {
        Time.timeScale = 1;        
        Invoke("waitForShoot", 0.1f);
    }

    void waitForShoot()
    {
        unpause = true;
    }
}
