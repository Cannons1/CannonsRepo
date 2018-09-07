﻿using UnityEngine;

public class CanvasMenu : MonoBehaviour {
    [SerializeField] GameObject[] canvas;
    [SerializeField] AudioUI mAudioUI;
    [SerializeField] GameObject decorateCannon;

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

    public GameObject DecorateCannon
    {
        get
        {
            return decorateCannon;
        }

        set
        {
            decorateCannon = value;
        }
    }

    private void Start()
    {
        if (IGLevelManager.nxtButton)
        {
            DecorateCannon.SetActive(false);
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
        mAudioUI.AudioButtonBack();
        DecorateCannon.SetActive(true);
    }

    private void BackLeaderboard() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();

    }

    private void BackCharacter() {
        Canvas[2].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
        DecorateCannon.SetActive(true);
    }

    private void BackShop() {
        Canvas[3].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
        DecorateCannon.SetActive(true);
    }

    private void BackCampaing() {
        Canvas[4].SetActive(false);
        PrincipalCanvasActive();
        mAudioUI.AudioButtonBack();
        DecorateCannon.SetActive(true);
    }
}
