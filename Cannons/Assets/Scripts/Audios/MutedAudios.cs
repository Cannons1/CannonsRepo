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
        if (Singleton.instance.musicMuted == false)
            musicToggle.isOn = false;
        else
            musicToggle.isOn = true;

        if (Singleton.instance.sFxMuted == false)
            sFxToggle.isOn = false;
        else
            sFxToggle.isOn = true;
    }

    public void MusicMuted(float _MusicVol)
    {
        Singleton.instance.musicMuted = true;

        if (Singleton.instance.musicMuted)
            masterMixer.SetFloat("musicVolume", _MusicVol);

        if (musicToggle.isOn == false)
        {
            Singleton.instance.musicMuted = false;
            masterMixer.ClearFloat("musicVolume");
        }
    }

    public void FxMuted(float _FxVol)
    {
        Singleton.instance.sFxMuted = true;

        if (Singleton.instance.sFxMuted)
            masterMixer.SetFloat("fxVolume", _FxVol);
            
        if (sFxToggle.isOn == false)
        {
            Singleton.instance.sFxMuted = false;
            masterMixer.ClearFloat("fxVolume");
        }
    }
}
