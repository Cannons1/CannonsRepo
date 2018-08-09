using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgr : MonoBehaviour {

    [SerializeField] GameObject canvasPaused;
    public static byte unnpause;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            unnpause += 1;
            PauseButton();
            if (Input.GetKeyDown(KeyCode.Escape) && unnpause == 2) {
                ResumeButton();
            }
        }
	}

    private void PauseButton()
    {
        canvasPaused.SetActive(true);
        LvlMgr.unpause = false;
        Debug.Log(unnpause);
        Time.timeScale = 0;
    }

    private void ResumeButton()
    {
        canvasPaused.SetActive(false);
        LvlMgr.unpause = true;
        Time.timeScale = 1;
        Invoke("waitForShoot", 0.1f);
    }

    private void waitForShoot()
    {
        unnpause = 0;
    }
}
