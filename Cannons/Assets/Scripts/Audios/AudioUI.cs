using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] AudioSource uIAudioSource;
    [SerializeField] AudioClip buttonDefault, buttonBack, buttonPlay, claimAGift;

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
}
