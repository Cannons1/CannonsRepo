using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] AudioSource uIAudioSource;
    [SerializeField] AudioClip buttonDefault, buttonBack, claimAGift, coin, openAChest, starAudio;

    int i = 0;

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

    public void MusicButton()
    {
        i += 1;
        if (Singleton.instance.MusicMuted == true)
        {
            if (i == 1)
            {
                if (UIAudioSource.isPlaying == true)
                {
                    UIAudioSource.Stop();
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
        UIAudioSource.PlayOneShot(claimAGift);
    }
    public void AudioButtonDefault()
    {
        UIAudioSource.PlayOneShot(buttonDefault);
    }
    public void AudioButtonBack()
    {
        UIAudioSource.PlayOneShot(buttonBack);
    }
    public void AudioCoins() {
        UIAudioSource.PlayOneShot(coin);
    }

    public void AudioOpenChest() {
        UIAudioSource.PlayOneShot(openAChest);
    }

    public void AudioStar() {
        UIAudioSource.PlayOneShot(starAudio);
    }
}
