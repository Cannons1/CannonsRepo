using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IGLevelManager : MonoBehaviour
{
    [SerializeField] string menuButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    ExpCoinPoinMgr expCoinMgr;

    public static bool unpause;
    public static bool nxtButton;

    private void Start()
    {
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        slider.value = 0;
        unpause = true;
        nxtButton = false;
        expCoinMgr = (ExpCoinPoinMgr)FindObjectOfType(typeof(ExpCoinPoinMgr));
    }

    public void MenuButton()
    {
        expCoinMgr.Mgr();
        StartCoroutine(LoadAsynchronously(menuButton));
    }
    public void PauseButton()
    {
        unpause = false;
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

    public void NxtButton() {
        nxtButton = true;
        StartCoroutine(LoadAsynchronously(menuButton));
    }

    public IEnumerator LoadAsynchronously(string _sceneName)
    {
        loadingScreen.SetActive(true);
        //if(_sceneName == "Menu")
        //{
        //    Debug.Break();
        //}
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
