using UnityEngine;

public class MusicAudios : MonoBehaviour {
    [SerializeField] AudioSource musicAudiosource;
    [SerializeField] AudioClip menuMusic;

    private void Start()
    {
        MenuMusic();
    }

    public void MenuMusic() {
        if (!musicAudiosource.isPlaying)
        {
            musicAudiosource.clip = menuMusic;
            musicAudiosource.Play();
            musicAudiosource.loop = true;
        }
    }

}
