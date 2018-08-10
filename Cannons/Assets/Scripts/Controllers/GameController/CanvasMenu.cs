using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour {
    [SerializeField] GameObject[] canvas;
    [SerializeField] AudioUI mAudioUI;
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (canvas[0].activeInHierarchy) {
                Application.Quit();
            }
            if (canvas[1].activeInHierarchy) {
                BackSettings();
            }
            if (canvas[2].activeInHierarchy) {
                BackLeaderboard();
            }
            if (canvas[3].activeInHierarchy)
            {
                BackCharacter();
            }
            if (canvas[4].activeInHierarchy)
            {
                BackShop();
            }
            if (canvas[5].activeInHierarchy)
            {
                BackCampaing();
            }
        }
	}

    private void PrincipalCanvasActive() {
        canvas[0].SetActive(true);
    }

    private void BackSettings() {
        canvas[1].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
    }

    private void BackLeaderboard() {
        canvas[2].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
    }

    private void BackCharacter() {
        canvas[3].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
    }

    private void BackShop() {
        canvas[4].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
    }


    private void BackCampaing() {
        canvas[5].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
    }
}
