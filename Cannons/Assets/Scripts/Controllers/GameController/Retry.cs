﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {

    [SerializeField] GameObject canvasRetry;
    [SerializeField] WriteVbles mWriteVbles;
    [SerializeField] CanvasMgr mCanvasMgr;
    Scene mScene;
    ExpCoinPoinMgr expCoinPoinMgr;
    private void Start()
    {
        expCoinPoinMgr = (ExpCoinPoinMgr)FindObjectOfType(typeof(ExpCoinPoinMgr));
        mScene = SceneManager.GetActiveScene();
    }

    public void RetryLvl() {
        expCoinPoinMgr.MinusExperienceInGame();
        expCoinPoinMgr.MinusLvl();
        SceneManager.LoadScene(mScene.name);
    }

    public void Restart() {
        expCoinPoinMgr.Mgr();
        SceneManager.LoadScene(mScene.name);
    }

    public void ActiveCanvas() {
        mWriteVbles.WriteCoinInRetry();
        canvasRetry.SetActive(true);
        mCanvasMgr.Canvas[0].SetActive(false);
    }
}
