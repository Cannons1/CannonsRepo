﻿using UnityEngine;

public class CanvasMenu : MonoBehaviour {
    [SerializeField] GameObject[] canvas;
    [SerializeField] AudioUI mAudioUI;
    [SerializeField] GameObject capsuleTapToPlay;

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

    public AudioUI MAudioUI
    {
        get
        {
            return mAudioUI;
        }

        set
        {
            mAudioUI = value;
        }
    }

    private void Start()
    {
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
            if (Canvas[1].activeInHierarchy) {
                BackSettings();
            }
            if (Canvas[2].activeInHierarchy) {
                BackCharacter();
            }
            if (Canvas[3].activeInHierarchy)
            {
                BackShop();
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
        MAudioUI.AudioButtonBack();
        CapsuleTapToPlay.SetActive(true);
    }

    private void BackLeaderboard() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        MAudioUI.AudioButtonBack();

    }

    private void BackCharacter() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        MAudioUI.AudioButtonBack();
        CapsuleTapToPlay.SetActive(true);
    }

    private void BackShop() {
        Canvas[3].SetActive(false);
        PrincipalCanvasActive();
        MAudioUI.AudioButtonBack();
        CapsuleTapToPlay.SetActive(true);
    }

    private void BackCampaing() {
        Canvas[4].SetActive(false);
        PrincipalCanvasActive();
        MAudioUI.AudioButtonBack();
        CapsuleTapToPlay.SetActive(true);
    }
}
