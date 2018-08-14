using UnityEngine;

public class AudioCannons : MonoBehaviour {

    [SerializeField] AudioClip shoot, ignite, wick, wickOff;
    [SerializeField] AudioSource cannonsAudioSource;

    public void AudioShoot() {
        cannonsAudioSource.clip = shoot;
        cannonsAudioSource.Play();
    }
}
