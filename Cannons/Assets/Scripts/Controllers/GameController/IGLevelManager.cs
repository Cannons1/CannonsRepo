using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class IGLevelManager : MonoBehaviour
{
    [SerializeField] string menuScene;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] ChestAnimatedUI chestAnimatedUI;
    [SerializeField] Text lvlName = null, lvlNamePaused = null;
    [SerializeField] GameObject txtTapToShoot;
    [SerializeField] WinCondition winCondition;
    [SerializeField] AudioController audioController;
    [SerializeField] Distance distance;

    public GameObject adWatchButton;
    public GameObject reviveButton;
    public Text coinsToRevive;
    public GameObject[] canvas;
    public static bool unpause;
    public static bool campaignBtn;
    public static bool isSecondWorld;
    public static bool isThirdWorld;

    public static byte unnpause;

    public delegate IEnumerator Stars();
    public Stars delStars;

    private void Start() {
        unnpause = 0;
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        slider.value = 1;
        unpause = true;
        campaignBtn = false;
        isSecondWorld = false;
        isThirdWorld = false;

        lvlNamePaused.text = string.Format("Level {0}", winCondition.level);
        lvlName.text = string.Format("Level {0}", winCondition.level);

        Invoke("SetLvlName", 2f);

        if (SceneManager.GetActiveScene().name == "Lvl1") {
            txtTapToShoot.SetActive(true);
            distance.delTxtTapShoot += SetTapShoot;
        }
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, winCondition.level.ToString());
    }

    private void SetTapShoot() {
        txtTapToShoot.SetActive(false);
    }

    private void SetLvlName() {
        lvlName.gameObject.SetActive(false);
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
            isSecondWorld = true;
        else if (currentScene.buildIndex >= 22 && currentScene.buildIndex <= 31)//22 to 31 third world
            isThirdWorld = true;
        StartCoroutine(LoadAsynchronously(menuScene));
    }

    public void NextWorld(int _nextWorld) {
        campaignBtn = true;
        Singleton.SaveCoins();
        switch (_nextWorld) {
            case 2:
                isSecondWorld = true;
                break;
            case 3:
                isThirdWorld = true;
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

        StartCoroutine(PauseGame());
    }

    IEnumerator PauseGame() {
        WaitForSeconds wait = new WaitForSeconds(0.25f);
        yield return wait;
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
        StartCoroutine(chestAnimatedUI.SpriteAnim());
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
        audioController.AudioBtnDef();
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
        audioController.AudioBtnDef();
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