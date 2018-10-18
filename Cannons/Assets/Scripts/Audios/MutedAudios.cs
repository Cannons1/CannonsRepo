using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MutedAudios : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Toggle sFxToggle, musicToggle;
    [SerializeField] AudioController audioController;

    byte i = 0;

    private void Start()
    {
        if (Singleton.instance.MusicMuted == false)
            musicToggle.isOn = false;
        else
            musicToggle.isOn = true;

        if (Singleton.instance.SfxMuted == false)
            sFxToggle.isOn = false;
        else
            sFxToggle.isOn = true;


    }

    public void MusicButton()
    {
        i += 1;
        if (Singleton.instance.MusicMuted == true)
        {
            if (i == 1)
            {
                if (audioController.UIAudioSource.isPlaying == true)
                    audioController.UIAudioSource.Stop();
            }
            else
                audioController.AudioBtnDef();
        }
        else
            audioController.AudioBtnDef();
    }

    public void MusicMuted(float _MusicVol)
    {
        Singleton.instance.MusicMuted = true;

        if (Singleton.instance.MusicMuted)
            masterMixer.SetFloat("musicVolume", _MusicVol);

        if (musicToggle.isOn == false)
        {
            Singleton.instance.MusicMuted = false;
            masterMixer.ClearFloat("musicVolume");
        }
    }

    public void FxMuted(float _FxVol)
    {
        Singleton.instance.SfxMuted = true;

        if (Singleton.instance.SfxMuted)
            masterMixer.SetFloat("fxVolume", _FxVol);
            
        if (sFxToggle.isOn == false)
        {
            Singleton.instance.SfxMuted = false;
            masterMixer.ClearFloat("fxVolume");
        }
    }
}
