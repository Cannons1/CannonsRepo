using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgr : MonoBehaviour {

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
                    mAduioUI.AudioButtonDefault();
                    break;
                case 2:
                    ResumeButton();
                    break;
                default:
                    unnpause = 0;
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
	}

    private void PauseButton()
    {
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        LvlMgr.unpause = false;
        Time.timeScale = 0;
    }

    private void ResumeButton()
    {
        mAduioUI.AudioButtonBack();
        canvas[0].SetActive(true);
        canvas[1].SetActive(false);
        LvlMgr.unpause = true;
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
