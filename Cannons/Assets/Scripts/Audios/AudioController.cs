﻿using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip buttonDefault, buttonBack, claimGift, coin, star, openChest, music, tropicalWin;
    [SerializeField] AudioSource uIAudioSource, itemAudioSource, musicAudioSource;

    public AudioSource UIAudioSource
    {
        get
        {
            return uIAudioSource;
        }

        set
        {
            uIAudioSource = value;
        }
    }

    public AudioSource ItemAudioSource
    {
        get
        {
            return itemAudioSource;
        }

        set
        {
            itemAudioSource = value;
        }
    }

    private void Start()
    {
        Music();
    }

    public void AudioBtnDef() {
        UIAudioSource.PlayOneShot(buttonDefault);
    }

    public void AudioBtnBack() {
        UIAudioSource.PlayOneShot(buttonBack);
    }

    public void AudioCoins() {
        UIAudioSource.PlayOneShot(coin);
    }

    public void AudioStar() {
        ItemAudioSource.PlayOneShot(star);
    }

    public void AudioStar(float _volume) {
        ItemAudioSource.PlayOneShot(star, _volume);
    }

    public void AudioOpenChest() {
        ItemAudioSource.PlayOneShot(openChest);
    }

    public void SoundClaimGift() {
        UIAudioSource.PlayOneShot(claimGift);
    }

    private void Music() {
        musicAudioSource.clip = music;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void AudioTropicalWin() {
        StartCoroutine(MusicBack(tropicalWin, musicAudioSource.volume));
    }

    IEnumerator MusicBack(AudioClip _tropicalWin,float _currentVolume) {
        musicAudioSource.clip = _tropicalWin;
        musicAudioSource.volume = 1f;
        musicAudioSource.Play();
        yield return new WaitForSeconds(_tropicalWin.length);
        musicAudioSource.volume = _currentVolume;
        Music();
    }
}