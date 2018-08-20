using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] VideoClip videoLogo;
    [SerializeField] VideoPlayer videoPlayerLogo;
    [SerializeField] GameObject Canvas;
    static bool init;
    WaitForSeconds videoLenght;
    float t = 0;

    private void Start()
    {
        videoLenght = new WaitForSeconds((float)videoLogo.length);
        if (!init) {
            Canvas.SetActive(false);
            StartCoroutine(VideoPlayer());
        }
        else
            StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        Canvas.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
    

    IEnumerator VideoPlayer() {
        videoPlayerLogo.Play();
        init = true;
        yield return videoLenght;
        StartCoroutine(LoadAsynchronously());
    }
}
