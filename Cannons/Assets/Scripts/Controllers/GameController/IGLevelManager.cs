﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IGLevelManager : MonoBehaviour
{
    [SerializeField] string menuButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

    public static bool unpause;
    public static bool campaignBtn;

    private void Start()
    {
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        slider.value = 0;
        unpause = true;
        campaignBtn = false;
    }

    public void MenuButton()
    {
        Singleton.SaveCoins();
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

    public void CampaignButton() {
        campaignBtn = true;
        StartCoroutine(LoadAsynchronously(menuButton));
    }

    public void NextButton() {
        Scene activeScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsynchronously(activeScene.buildIndex + 1));
    }


    public IEnumerator LoadAsynchronously(string _sceneName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

    public IEnumerator LoadAsynchronously(int index)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
