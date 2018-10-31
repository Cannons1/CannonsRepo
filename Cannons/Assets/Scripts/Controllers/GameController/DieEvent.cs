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

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        mCollider = GetComponent<Collider>();
        DesactivatePanel = delegate ()
        {
            iGLevelManager.canvas[0].SetActive(true);
            iGLevelManager.canvas[2].SetActive(false);
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

    public void WatchAd()
    {
        adMobManager.ShowReviveVideo();
        //AdMobManager.Instance.ShowReviveVideo();
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
