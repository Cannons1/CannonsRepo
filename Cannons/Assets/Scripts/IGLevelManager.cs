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

    private void Start()
    {
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        slider.value = 0;
        unpause = true;
    }

    public void MenuButton()
    {
        StartCoroutine(LoadAsynchronously(menuButton));
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

    IEnumerator LoadAsynchronously(string _sceneName)
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
}