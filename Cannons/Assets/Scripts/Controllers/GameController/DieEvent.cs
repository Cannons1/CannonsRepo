using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieEvent : MonoBehaviour {

    Collider mCollider;
    [SerializeField] CanvasMgr canvasMgr;
    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        mCollider = GetComponent<Collider>();
    }

    public void ActiveCanvasRetry()
    {
        StartCoroutine(EndDieAudio());
        mCollider.enabled = false;
        canvasMgr.Canvas[0].SetActive(false);
    }

    public void RetryLvl()
    {
        SceneManager.LoadScene(scene.name);
        Singleton.SaveCoins();
    }
    
    WaitForSeconds dieLength = new WaitForSeconds(0.3f);

    IEnumerator EndDieAudio()
    {
        yield return dieLength;
        if (!canvasMgr.Canvas[2].activeInHierarchy) {
            canvasMgr.Canvas[2].SetActive(true);
        }
        Time.timeScale = 0;
    }
}
