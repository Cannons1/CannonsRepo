using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEvent : MonoBehaviour {

    [SerializeField] LvlMgr mlvlMgr;
    [SerializeField] WillAudios willAudios;

    public void ChargeMenuLevel()
    {
        StartCoroutine(EndDieAudio());
    }

    WaitForSeconds dieLength = new WaitForSeconds(0.5472562f);

    IEnumerator EndDieAudio()
    {
        willAudios.DieAudio();
        yield return dieLength;
        mlvlMgr.MenuButton();
    }

}
