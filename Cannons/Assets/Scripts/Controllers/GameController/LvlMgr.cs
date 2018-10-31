using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LvlMgr : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] AudioController audioController;
    [SerializeField] GameObject background;
    [SerializeField] CanvasMenu canvasMenu;

    public void Levels(string levelName)
    {
        StartCoroutine(LoadAsynchronously(levelName));
    }

    IEnumerator LoadAsynchronously(string _sceneName)
    {
        Time.timeScale = 1;
        audioController.AudioBtnDef();
        yield return new WaitForSeconds(0.13f);
        background.SetActive(false);
        canvasMenu.Canvas[4].SetActive(false);
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = 1f - progress;
            yield return null;
        }
    }
}
