using System.Collections;
using UnityEngine;

public class DieEvent : MonoBehaviour {
    [SerializeField] Retry mRetry;
    Collider mCollider;

    private void Start()
    {
        mCollider = GetComponent<Collider>();
    }

    public void ChargeMenuLevel()
    {
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
