using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LvlMgr : MonoBehaviour
{
    [SerializeField] string playButton, menuButton, rouletteScene, lvl1, lvl2;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

    public void PlayButton()
    {
        StartCoroutine(LoadAsynchronously(playButton));
    }

    public void RouletteScene()
    {
        StartCoroutine(LoadAsynchronously(rouletteScene)); ;
    }

    public void FirstLvl()
    {
        StartCoroutine(LoadAsynchronously(lvl1));
    }

    public void SecondLvl()
    {
        StartCoroutine(LoadAsynchronously(lvl2));
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
