using UnityEngine;

public class AudioItems : MonoBehaviour
{
    [SerializeField] AudioClip coin;
    [SerializeField] AudioSource itemsAudioSource;

    public void AudioCoin()
    {
        itemsAudioSource.clip = coin;
        itemsAudioSource.Play();
    }
}
