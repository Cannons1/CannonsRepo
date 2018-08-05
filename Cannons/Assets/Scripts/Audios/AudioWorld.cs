using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWorld : MonoBehaviour {

    [SerializeField] AudioClip shoot, ignite, wick, wickOff, hitsAWall, flying;
    [SerializeField] AudioSource worldAudioSource;

    public void AudioShoot() {
        worldAudioSource.clip = shoot;
        worldAudioSource.Play();
    }
}
