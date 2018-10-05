using UnityEngine;
using System.Collections;

public class AudioCannons : MonoBehaviour {

    [SerializeField] AudioClip shoot, wick;
    [SerializeField] AudioSource cannonsAudioSource;

    public void AudioShoot() {
        cannonsAudioSource.volume = 1f;
        cannonsAudioSource.clip = shoot;
        cannonsAudioSource.Play();
        cannonsAudioSource.loop = false;
    }

    public void AudioWick() {
        cannonsAudioSource.clip = wick;
        cannonsAudioSource.Play();
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        cannonsAudioSource.loop = true;
        cannonsAudioSource.volume = 0.05f;
        while (cannonsAudioSource.volume < 0.55f) {
            cannonsAudioSource.volume += Time.deltaTime / 10f;
            yield return null;
        }
    }
}
