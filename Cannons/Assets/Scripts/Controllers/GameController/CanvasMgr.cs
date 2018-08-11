using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgr : MonoBehaviour {

    public GameObject[] Canvas {
        get { return canvas; }
        set { canvas = value; }
    }

    [SerializeField] GameObject[] canvas;
    [SerializeField] AudioUI mAduioUI;
    public static int unnpause;

    private void Start()
    {
        unnpause = 0;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unnpause ++;
            switch (unnpause) {
                case 1:
                    PauseButton();
                    break;
                case 2:
                    ResumeButton();
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canvas[2].activeInHierarchy) {
            SettingsBack();
            PauseButton();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canvas[3].activeInHierarchy) {
            MenuCancel();
            PauseButton();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canvas[4].activeInHierarchy) {
            canvas[0].SetActive(false);
            canvas[1].SetActive(false);
            unnpause = 3;
            IGLevelManager.unpause = false;
        }
	}

    private void PauseButton()
    {
        if (canvas[0].activeInHierarchy) {
            mAduioUI.AudioButtonDefault();
            canvas[0].SetActive(false);
            canvas[1].SetActive(true);
        }
        IGLevelManager.unpause = false;
        Time.timeScale = 0;
    }

    private void ResumeButton()
    {
        mAduioUI.AudioButtonBack();
        canvas[0].SetActive(true);
        canvas[1].SetActive(false);
        IGLevelManager.unpause = true;
        Time.timeScale = 1;
        unnpause = 0;
    }

    private void SettingsBack() {
        unnpause = 1;
        canvas[2].SetActive(false);
        mAduioUI.AudioButtonBack();
    }

    private void MenuCancel() {
        unnpause = 1;
        canvas[3].SetActive(false);
        mAduioUI.AudioButtonBack();
    }
}
