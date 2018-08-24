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
    [SerializeField] GameObject canvas;

    private void Start()
    {
        videoPlayerLogo.Play();
        videoPlayerLogo.loopPointReached += LoadScene;
    }

    IEnumerator LoadAsynchronously()
    {
        canvas.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

    void LoadScene(VideoPlayer videoPlayer)
    {
        StartCoroutine(LoadAsynchronously());
    }
}
