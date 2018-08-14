using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WillAudios : MonoBehaviour
{
    [SerializeField] AudioClip expressionDie, expressionBeingShootAndFlying, expressionLands, hitsAWall, flying;
    [SerializeField] AudioSource willAudioSource;

    public void DieAudio() {
        willAudioSource.clip = expressionDie;
        willAudioSource.Play();
    }
}
