using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip buttonDefault = null, buttonBack = null, claimGift = null, coin= null, star = null, openChest =null, music= null, tropicalWin=null, smashSeagull =null, gullSound =null;
    [SerializeField] AudioClip wall = null;
    [SerializeField] AudioSource uIAudioSource, itemAudioSource, musicAudioSource;

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

    public AudioSource ItemAudioSource
    {
        get
        {
            return itemAudioSource;
        }

        set
        {
            itemAudioSource = value;
        }
    }
    Will will;

    public static AudioController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
    }

    private void Start()
    {
        Music();
        will = FindObjectOfType<Will>();
        if (will != null)
            will.delWillSounds += AudioWall;
    }

    public void AudioBtnDef() {
        UIAudioSource.PlayOneShot(buttonDefault);
    }

    public void AudioBtnBack() {
        UIAudioSource.PlayOneShot(buttonBack);
    }

    public void AudioCoins() {
        UIAudioSource.PlayOneShot(coin);
    }

    public void AudioWall() {
        UIAudioSource.PlayOneShot(wall);
    }

    public void AudioStar() {
        ItemAudioSource.PlayOneShot(star);
    }

    public void AudioStar(float _volume) {
        ItemAudioSource.PlayOneShot(star, _volume);
    }

    public void AudioOpenChest() {
        ItemAudioSource.PlayOneShot(openChest);
    }

    public void AudioSmashSeagull() {
        ItemAudioSource.PlayOneShot(smashSeagull);
    }

    public void AudioGullSound(float _timeDelay) {
        StartCoroutine(PlayGullDelayed(_timeDelay));
    }

    IEnumerator PlayGullDelayed(float _timeDelay) {
        WaitForSeconds wait = new WaitForSeconds(_timeDelay);
        yield return wait;
        UIAudioSource.PlayOneShot(gullSound, 0.15f);
    }

    public void SoundClaimGift() {
        UIAudioSource.PlayOneShot(claimGift);
    }

    private void Music() {
        musicAudioSource.clip = music;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void AudioTropicalWin() {
        StartCoroutine(MusicBack(tropicalWin));
    }

    IEnumerator MusicBack(AudioClip _tropicalWin) {
        musicAudioSource.clip = null;
        musicAudioSource.volume = 1f;
        musicAudioSource.PlayOneShot(_tropicalWin);
        yield return new WaitForSeconds(_tropicalWin.length);
        musicAudioSource.volume = 0.084f;
        Music();
    }
}
