using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    [SerializeField] Canvas principalCanvas;
    [SerializeField] AudioUI mAudioUI;

    [SerializeField] LvlMgr mLvlMgr;

    void Update ()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            principalCanvas.enabled = false;
           // mAudioUI.AudioPlayButton();
            Debug.Log("Empecé");
            mLvlMgr.PlayButton();
        }
	}
}
