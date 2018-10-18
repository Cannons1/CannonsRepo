using UnityEngine;

public class CanvasMenu : MonoBehaviour {

    [SerializeField] GameObject[] canvas;
    [SerializeField] AudioController audioController;
    [SerializeField] GameObject capsuleTapToPlay;
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

    public GameObject CapsuleTapToPlay
    {
        get
        {
            return capsuleTapToPlay;
        }

        set
        {
            capsuleTapToPlay = value;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (IGLevelManager.campaignBtn)
        {
            CapsuleTapToPlay.SetActive(false);
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
            if (Canvas[1].activeInHierarchy && !panelCredits.activeInHierarchy)
            {
                BackSettings();
            }
            else {
                panelCredits.SetActive(false);
                audioController.AudioBtnDef();
            }
            if (Canvas[2].activeInHierarchy && !canvas[3].activeInHierarchy) {
                BackCharacter();
            }
            if (Canvas[3].activeInHierarchy)
            {
                if (!shopController.IsInShop)
                    BackShop();
                else
                {
                    canvas[3].SetActive(false);
                    audioController.AudioBtnBack();
                }
            }
            if (Canvas[4].activeInHierarchy)
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
        audioController.AudioBtnBack();
        CapsuleTapToPlay.SetActive(true);
    }

    private void BackLeaderboard() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        audioController.AudioBtnBack();
    }

    private void BackCharacter() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        audioController.AudioBtnBack();
        CapsuleTapToPlay.SetActive(true);
    }

    private void BackShop() {
        Canvas[3].SetActive(false);
        PrincipalCanvasActive();
        audioController.AudioBtnBack();
        CapsuleTapToPlay.SetActive(true);
    }

    private void BackCampaing() {
        Canvas[4].SetActive(false);
        PrincipalCanvasActive();
        audioController.AudioBtnBack();
        CapsuleTapToPlay.SetActive(true);
    }
}
