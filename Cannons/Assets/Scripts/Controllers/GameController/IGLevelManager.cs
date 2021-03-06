﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;

public delegate IEnumerator Del();

public class IGLevelManager : MonoBehaviour
{
    [SerializeField] string menuScene;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text lvlName = null, lvlNamePaused = null;
    [SerializeField] GameObject animTapToShoot;
    [SerializeField] WinCondition winCondition;
    [SerializeField] Text countDown;

    public GameObject adWatchButton;
    public GameObject reviveButton;
    public Text coinsToRevive;
    public GameObject[] canvas;
    public static bool unpause;
    public static bool campaignBtn;
    public static bool[] wichWorld;

    public static byte unnpause;

    public delegate IEnumerator Stars();
    public Stars delStars;
    public static Del countDownHandler;
    public static Del uIChestAnimated;

    public static bool firstTutotial;
    public static bool tutorialFinished = false;

    private void Start() {
        unnpause = 0;
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        slider.value = 1;
        unpause = true;
        campaignBtn = false;

        wichWorld = new bool[3];//Number of worlds in game except the number one
        for (int i = 0; i < wichWorld.Length; i++) { wichWorld[i] = false; }

        countDownHandler += CountDown;

        lvlNamePaused.text = string.Format("Level {0}", winCondition.level);
        lvlName.text = string.Format("Level {0}", winCondition.level);

        Invoke("SetLvlName", 2f);

        if (SceneManager.GetActiveScene().name == "Lvl1")
        {
            if (tutorialFinished != true)
            {
                animTapToShoot.SetActive(true);
                firstTutotial = true;
                CannonParent.delShoot += SetTapShoot;
            }
            else {
                animTapToShoot.SetActive(false);
            }
        }
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, winCondition.level.ToString());
    }

    private void SetTapShoot() {
        animTapToShoot.SetActive(false);
        tutorialFinished = true;
        firstTutotial = false;
    }

    private void SetLvlName() {
        lvlName.gameObject.SetActive(false);
    }

    public IEnumerator CountDown()
    {
        countDown.gameObject.SetActive(true);
        countDown.GetComponent<Animator>().SetTrigger("trigger");
        float t = 3f;
        while(t > 1)
        {
            t -= Time.unscaledDeltaTime;
            countDown.text = string.Format("{0} ...", Mathf.RoundToInt(t));
            yield return null;
        }
        Time.timeScale = 1;
        Will.will.Revive();
        StartCoroutine(uIChestAnimated());//Starts again the animation of the chest
        countDown.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            unnpause++;
            switch (unnpause)
            {
                case 1:
                    if(canvas[0].activeInHierarchy)
                        PauseButton();
                    break;
                case 2:
                    if(canvas[1].activeInHierarchy)
                        ResumeButton();
                    break;
                default:
                    break;
            }
        }
    }

    private void MenuButton() {
        Singleton.SaveCoins();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex >= 12 && currentScene.buildIndex <= 21)//12 to 21 second World
            wichWorld[0] = true;
        else if (currentScene.buildIndex >= 22 && currentScene.buildIndex <= 31)//22 to 31 third world
            wichWorld[1] = true;
        else if (currentScene.buildIndex >= 32 && currentScene.buildIndex <= 41)//32 to 41 fourth world
            wichWorld[2] = true;
        StartCoroutine(LoadAsynchronously(menuScene));
    }

    public void NextWorld(int _nextWorld) {
        campaignBtn = true;
        Singleton.SaveCoins();
        switch (_nextWorld) {
            case 2:
                wichWorld[0] = true;
                break;
            case 3:
                wichWorld[1] = true;
                break;
            case 4:
                wichWorld[2] = true;
                break;
            default:
                break;
        }
        StartCoroutine(LoadAsynchronously(menuScene));
    }

    public void PauseButton() {
        unpause = false;
        unnpause = 1;

        if (canvas[0].activeInHierarchy) {
            canvas[0].SetActive(false);
            canvas[1].SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void ResumeButton() {
        unnpause = 0;
        StartCoroutine(delStars());
        if (canvas[1].activeInHierarchy) {
            canvas[1].SetActive(false);
            canvas[0].SetActive(true);
        }
        Time.timeScale = 1;
        StartCoroutine(uIChestAnimated());//Starts again the animation of the chest
        Invoke("waitForShoot", 0.1f);
    }

    void waitForShoot() {
        unpause = true;
    }

    public void CampaignButton() {
        campaignBtn = true;
        MenuButton();
    }

    public void NextButton() {
        Scene activeScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsynchronously(activeScene.buildIndex + 1));
    }

    public IEnumerator LoadAsynchronously(string _sceneName) {
        Time.timeScale = 1;
        AudioController.sharedInstance.AudioBtnDef();
        yield return new WaitForSeconds(0.13f);
        loadingScreen.SetActive(true);
        foreach (GameObject a in canvas) {
            a.SetActive(false);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = 1f - progress;
            yield return null;
        }
    }

    public IEnumerator LoadAsynchronously(int index) {
        Time.timeScale = 1;
        AudioController.sharedInstance.AudioBtnDef();
        yield return new WaitForSeconds(0.13f);
        loadingScreen.SetActive(true);
        foreach (GameObject a in canvas)
        {
            a.SetActive(false);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = 1f - progress;
            yield return null;
        }
    }
}