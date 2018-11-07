using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class DieEvent : MonoBehaviour {

    Collider mCollider;
    [SerializeField] IGLevelManager iGLevelManager;
    [SerializeField] WinCondition winCondition;
    [SerializeField] WriteVbles writeVbles;

    [SerializeField] AdMobManager adMobManager;
    public delegate void Revive();
    public static Revive DesactivatePanel;
    public static Revive ReactivateCollider;
    private float coinsToRevive = 15;

    public static bool isDeath;
    Scene scene;

    private void Start()
    {
        iGLevelManager.coinsToRevive.text = coinsToRevive.ToString();
        scene = SceneManager.GetActiveScene();
        mCollider = GetComponent<Collider>();
        DesactivatePanel = delegate ()
        {
            iGLevelManager.canvas[0].SetActive(true);
            iGLevelManager.canvas[2].SetActive(false);
            iGLevelManager.adWatchButton.SetActive(false);
            iGLevelManager.reviveButton.SetActive(true);
        };       
    }

    int i = 0;
    public void CharacterDie()
    {
        print("Character Die llamado" + ++i);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, winCondition.level.ToString());
        StartCoroutine(ShowRetryPanel());
        iGLevelManager.canvas[0].SetActive(false);
        isDeath = true;
        //mCollider.enabled = false;
    }

    public void RetryLvl()
    {
        StartCoroutine(iGLevelManager.LoadAsynchronously(scene.name));
        Singleton.SaveCoins();
    }

    public void ReviveWithCoins()
    {
        if (Singleton.instance.Coins >= coinsToRevive)
        {
            Singleton.instance.Coins -= (int)coinsToRevive;
            Singleton.SaveCoins();

            Time.timeScale = 1;
            DesactivatePanel();
            Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
            Will.will.Revive();
            isDeath = false;

            coinsToRevive *= 1.5f;
            iGLevelManager.coinsToRevive.text = ((int)coinsToRevive).ToString();
        }
    }

    public void WatchAd()
    {
        adMobManager.ShowReviveVideo();
    }
    
    IEnumerator ShowRetryPanel()
    {
        writeVbles.CoinsInRetry();
        yield return new WaitForSeconds(0.3f);
        if (!iGLevelManager.canvas[2].activeInHierarchy) {
            iGLevelManager.canvas[2].SetActive(true);
        }
        //Time.timeScale = 0;       
    }

}
