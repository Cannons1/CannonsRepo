using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudios : MonoBehaviour {
    [SerializeField] AudioSource musicAudiosource;
    [SerializeField] AudioClip menuMusic;

    private void Start()
    {
        MenuMusic();
    }

    public void MenuMusic() {
        musicAudiosource.clip = menuMusic;
        musicAudiosource.Play();
    }

}
