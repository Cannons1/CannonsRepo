using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider slider;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        loadingScreen.SetActive(false);
        slider.value = 0;
        videoPlayer.loopPointReached += LoadMenu;
    }

    private void LoadMenu(VideoPlayer videoPlayer)
    {
        StartCoroutine(LoadAsynchronously());
    }

    public IEnumerator LoadAsynchronously()
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
