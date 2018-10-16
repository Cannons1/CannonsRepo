using UnityEngine;

public class AudioItems : MonoBehaviour
{

    [SerializeField] AudioClip openAChest, starAudio;
    [SerializeField] AudioSource itemsAudioSource;

    public AudioSource ItemsAudioSource
    {
        get
        {
            return itemsAudioSource;
        }

        set
        {
            itemsAudioSource = value;
        }
    }

    private void Start()
    {
        itemsAudioSource.pitch = 1;    
    }

    public void AudioOpenChest()
    {
        ItemsAudioSource.PlayOneShot(openAChest);
    }

    public void AudioStar()
    {
        ItemsAudioSource.PlayOneShot(starAudio);
    }
}
