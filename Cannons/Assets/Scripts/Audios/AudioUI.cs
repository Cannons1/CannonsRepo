using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] AudioSource uIAudioSource;
    [SerializeField] AudioClip buttonDefault, buttonBack, claimAGift, coin;

    int i = 0;
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
    public void SoundClaimGift() {
        uIAudioSource.PlayOneShot(claimAGift);
    }
    public void AudioButtonDefault()
    {
        uIAudioSource.PlayOneShot(buttonDefault);
    }
    public void AudioButtonBack()
    {
        uIAudioSource.PlayOneShot(buttonBack);
    }

    public void AudioCoins() {
        uIAudioSource.PlayOneShot(coin);
    }
}
