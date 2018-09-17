using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {

    [SerializeField] GameObject canvasRetry;
    [SerializeField] WriteVbles mWriteVbles;
    [SerializeField] CanvasMgr mCanvasMgr;
    Scene mScene;

    private void Start()
    {
        mScene = SceneManager.GetActiveScene();
    }

    public void RetryLvl() {
        SceneManager.LoadScene(mScene.name);
        Singleton.SaveCoins();
    }

    public void ActiveCanvas() {
        mWriteVbles.WriteCoinInRetry();
        canvasRetry.SetActive(true);
        mCanvasMgr.Canvas[0].SetActive(false);
    }
}
