using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEvent : MonoBehaviour {
    [SerializeField] WillAudios willAudios;
    [SerializeField] Retry mRetry;

    public void ChargeMenuLevel()
    {
        StartCoroutine(EndDieAudio());
    }
    WaitForSeconds dieLength = new WaitForSeconds(0.5472562f);
    IEnumerator EndDieAudio()
    {
        willAudios.DieAudio();
        yield return dieLength;
        mRetry.ActiveCanvas();
        Time.timeScale = 0;
        //mlvlMgr.MenuButton();
    }
}
