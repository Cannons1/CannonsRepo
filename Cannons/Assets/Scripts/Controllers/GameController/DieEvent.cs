using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class DieEvent : MonoBehaviour {

    Collider mCollider;
    [SerializeField] IGLevelManager iGLevelManager;
    [SerializeField] WinCondition winCondition;
    [SerializeField] AdMobManager adMobManager;
    public delegate void CancelRevive();
    public static CancelRevive DesactivatePanel;
    private float coinsToRevive = 15;

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

    public void CharacterDie()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, winCondition.level.ToString());
        StartCoroutine(EndDieAudio());
        mCollider.enabled = false;
        iGLevelManager.canvas[0].SetActive(false);
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
            DieEvent.DesactivatePanel();
            Will.will.Revive();
            Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
            Time.timeScale = 1;
            coinsToRevive *= 1.5f;
            iGLevelManager.coinsToRevive.text = "" + (int)coinsToRevive;
        }
    }

    public void WatchAd()
    {
        adMobManager.ShowReviveVideo();
    }
    
    IEnumerator EndDieAudio()
    {
        yield return new WaitForSeconds(0.3f);
        if (!iGLevelManager.canvas[2].activeInHierarchy) {
            iGLevelManager.canvas[2].SetActive(true);
        }
        Time.timeScale = 0;
    }

}
