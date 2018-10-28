using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class DieEvent : MonoBehaviour {

    Collider mCollider;
    [SerializeField] IGLevelManager iGLevelManager;
    [SerializeField] WinCondition winCondition;
    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        mCollider = GetComponent<Collider>();
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
    
    IEnumerator EndDieAudio()
    {
        yield return new WaitForSeconds(0.3f);
        if (!iGLevelManager.canvas[2].activeInHierarchy) {
            iGLevelManager.canvas[2].SetActive(true);
        }
        Time.timeScale = 0;
    }
}
