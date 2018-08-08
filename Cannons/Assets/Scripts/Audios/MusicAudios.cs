using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudios : MonoBehaviour {
    [SerializeField] AudioSource musicAudiosource;
    [SerializeField] AudioClip menuMusic, atmosphereMenu;

    private void Start()
    {
        MenuMusic();
    }

    public void MenuMusic() {

        //musicAudiosource.clip = menuMusic;
        //musicAudiosource.Play();
      
        if (!musicAudiosource.isPlaying)
        {
            musicAudiosource.clip = atmosphereMenu;
            musicAudiosource.Play();
            musicAudiosource.loop = true;
        }
    }

}
