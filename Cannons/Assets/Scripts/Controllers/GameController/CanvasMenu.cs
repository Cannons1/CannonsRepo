using UnityEngine;

public class CanvasMenu : MonoBehaviour {

    [SerializeField] GameObject[] canvas;
    [SerializeField] ShopController shopController;
    [SerializeField] GameObject panelCredits;

    public GameObject[] Canvas
    {
        get
        {
            return canvas;
        }

        set
        {
            canvas = value;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (IGLevelManager.campaignBtn)
        {
            Canvas[0].SetActive(false);
            Canvas[4].SetActive(true);
        }
        else {
            Canvas[0].SetActive(true);
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (Canvas[0].activeInHierarchy) {
                Application.Quit();
            }
            else if (Canvas[1].activeInHierarchy)
            {
                if (!panelCredits.activeInHierarchy)
                    BackSettings();
                else {
                    panelCredits.SetActive(false);
                    AudioController.sharedInstance.AudioBtnBack();
                }
            }
            else if (Canvas[2].activeInHierarchy && !canvas[3].activeInHierarchy) {
                BackCharacter();
            }
            if (Canvas[3].activeInHierarchy)
            {
                canvas[3].SetActive(false);
                AudioController.sharedInstance.AudioBtnBack();
            }
            else if (Canvas[4].activeInHierarchy)
            {
                BackCampaing();
            }
        }
	}

    private void PrincipalCanvasActive() {
        Canvas[0].SetActive(true);
    }

    private void BackSettings() {
        Canvas[1].SetActive(false);
        PrincipalCanvasActive();
        AudioController.sharedInstance.AudioBtnBack();
    }

    private void BackLeaderboard() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        AudioController.sharedInstance.AudioBtnBack();
    }

    private void BackCharacter() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        AudioController.sharedInstance.AudioBtnBack();
    }

    private void BackCampaing() {
        Canvas[4].SetActive(false);
        PrincipalCanvasActive();
        AudioController.sharedInstance.AudioBtnBack();
    }
}
