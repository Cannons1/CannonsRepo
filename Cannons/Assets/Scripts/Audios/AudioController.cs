using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip buttonDefault, buttonBack, claimGift, coin, star, openChest, music;
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
}
