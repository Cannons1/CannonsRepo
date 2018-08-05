using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioItems : MonoBehaviour
{
    [SerializeField] AudioClip coin, rouletteCoin;
    [SerializeField] AudioSource itemsAudioSource;

    public void AudioCoin()
    {
        itemsAudioSource.clip = coin;
        itemsAudioSource.Play();
    }

    public void AudioRouletteCoin()
    {

    }
}
