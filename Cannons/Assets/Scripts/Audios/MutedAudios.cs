using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MutedAudios : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Toggle sFxToggle, musicToggle;

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
