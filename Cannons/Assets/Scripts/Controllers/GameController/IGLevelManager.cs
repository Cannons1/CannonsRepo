using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IGLevelManager : MonoBehaviour
{
    [SerializeField] string menuButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] ChestAnimatedUI chestAnimatedUI;
    [SerializeField] Text lvlName, lvlNamePaused;
    [SerializeField] GameObject txtTapToShoot;
    [SerializeField] WinCondition winCondition;
    [SerializeField] AudioController audioController;

    public GameObject[] canvas;
    public static bool unpause;
    public static bool campaignBtn;

    public static byte unnpause;
    static byte countTxtActive;

    private void Start() {
        unnpause = 0;
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        slider.value = 1;
        unpause = true;
        campaignBtn = false;

        if (SceneManager.GetActiveScene().name == "Lvl1")
            if (countTxtActive < 1)
                StartCoroutine(TimeTxtActive());

        StartCoroutine(LvlName());
        lvlNamePaused.text = "Level " + winCondition.level.ToString();
    }

    IEnumerator LvlName() {
        lvlName.text = "Level " + winCondition.level.ToString();
        yield return new WaitForSeconds(2);
        lvlName.text = " ";
    }

    IEnumerator TimeTxtActive() {
        countTxtActive++;
        txtTapToShoot.SetActive(true);
        yield return new WaitForSeconds(5f);
        txtTapToShoot.SetActive(false);
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

    public void MenuButton() {
        Singleton.SaveCoins();
        StartCoroutine(LoadAsynchronously(menuButton));
    }

    public void PauseButton() {
        unpause = false;
        Time.timeScale = 0;
        unnpause = 1;

        if (canvas[0].activeInHierarchy) {
            canvas[0].SetActive(false);
            canvas[1].SetActive(true);
        }
    }

    public void ResumeButton() {
        unnpause = 0;

        if (canvas[1].activeInHierarchy) {
            canvas[1].SetActive(false);
            canvas[0].SetActive(true);
        }
        Time.timeScale = 1;
        StartCoroutine(chestAnimatedUI.SpriteAnim());
        Invoke("waitForShoot", 0.1f);
    }

    void waitForShoot() {
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

    public IEnumerator LoadAsynchronously(string _sceneName) {
        Time.timeScale = 1;
        audioController.AudioBtnDef();
        yield return new WaitForSeconds(0.13f);
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);
        
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = 1f - progress;
            yield return null;
        }
    }

    public IEnumerator LoadAsynchronously(int index) {
        Time.timeScale = 1;
        audioController.AudioBtnDef();
        yield return new WaitForSeconds(0.13f);
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = 1f - progress;
            yield return null;
        }
    }
}