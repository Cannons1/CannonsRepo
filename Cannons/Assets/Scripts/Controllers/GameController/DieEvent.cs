using System.Collections;
using UnityEngine;

public class DieEvent : MonoBehaviour {
    [SerializeField] Retry mRetry;
    Collider mCollider;
    ExpCoinPoinMgr expCoinPoinMgr;

    private void Start()
    {
        expCoinPoinMgr = (ExpCoinPoinMgr)FindObjectOfType(typeof(ExpCoinPoinMgr));
        mCollider = GetComponent<Collider>();
    }

    public void ChargeMenuLevel()
    {
        expCoinPoinMgr.Mgr();
        StartCoroutine(EndDieAudio());
        mCollider.enabled = false;
    }
    WaitForSeconds dieLength = new WaitForSeconds(0.5472562f);
    IEnumerator EndDieAudio()
    {
        yield return dieLength;
        mRetry.ActiveCanvas();
        Time.timeScale = 0;
        //mlvlMgr.MenuButton();
    }
}
