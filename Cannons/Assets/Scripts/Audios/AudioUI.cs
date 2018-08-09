using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] AudioSource uIAudioSource;
    [SerializeField] AudioClip buttonDefault, buttonBack, buttonPlay, claimAGift;
    [SerializeField] LvlMgr mLvlMgr;
    [SerializeField] Coin mCoinsInGame;
    [SerializeField] ExpCoinPoinMgr mExperience;
    int i = 0;
    public void SoundClaimGift() {
        uIAudioSource.clip = claimAGift;
        uIAudioSource.Play();
    }
    public void MusicButton()
    {
        i += 1;
        if (Singleton.instance.MusicMuted == true)
        {
            if (i == 1)
            {
                if (uIAudioSource.isPlaying == true)
                {
                    uIAudioSource.Stop();
                }
            }
            else
            {
                AudioButtonDefault();
            }
        }
        else
        {
            AudioButtonDefault();
        }

    }
    public void AudioPlayButton()
    {
        uIAudioSource.clip = buttonPlay;
        uIAudioSource.Play();
        //StartCoroutine(AudioPlayFinished());
    }
    IEnumerator AudioPlayFinished()
    {
        yield return new WaitForSeconds(buttonPlay.length); //After sound of the play button will continue to next level
        mLvlMgr.PlayButton();
    }
    public void AudioButtonDefault()
    {
        uIAudioSource.clip = buttonDefault;
        uIAudioSource.Play();
    }
    public void AudioButtonBack()
    {
        uIAudioSource.clip = buttonBack;
        uIAudioSource.Play();
    }
    public void AudioMenuButton()
    {
        Time.timeScale = 1;//Unnpause the game
        uIAudioSource.clip = buttonBack;
        uIAudioSource.Play();
        if (mExperience.SavedLastExp)
        {
            Singleton.instance.Experience = mExperience.Saved;
            mExperience.MinusLvl();
            Debug.Log(mExperience.Saved + " Esto es lo que cargaré");
        }
        else {
            mExperience.MinusExperienceInGame();//If the user press menu in a middle of a game, the experience wont count
        }
        mCoinsInGame.MinusCoinsInGame();//If the user press menu in a middle of a game, the coins wont count
        mLvlMgr.MenuButton();//Returns to menu
    }
}
