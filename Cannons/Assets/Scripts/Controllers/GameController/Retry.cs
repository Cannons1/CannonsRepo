using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {
    [SerializeField] GameObject canvasRetry;
    [SerializeField] WriteVbles mWriteVbles;
    Scene mScene;
    private void Start()
    {
        mScene = SceneManager.GetActiveScene();
    }

    public void RetryLvl() {
        Time.timeScale = 1;
        SceneManager.LoadScene(mScene.name);
    }

    public void ActiveCanvas() {
        mWriteVbles.WriteCoinInRetry();
        canvasRetry.SetActive(true);
    }
}
