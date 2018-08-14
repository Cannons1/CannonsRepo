using UnityEngine;
using UnityEngine.Audio;

public class WillAudios : MonoBehaviour
{
    AudioClip[] willAudios;
    AudioSource willA;
    AudioMixer mixer;
    string _OutputMixer = "Fx";

    private void Start()
    {
        mixer = Resources.Load("Sounds/Master") as AudioMixer;
        willAudios = Resources.LoadAll<AudioClip>("Sounds/Fx/WillGoldTooth");
        willA = GetComponent<AudioSource>();
        willA.outputAudioMixerGroup = mixer.FindMatchingGroups(_OutputMixer)[0];
        willA.playOnAwake = false;
    }

    public void DieAudio() {
        willA.clip = willAudios[0];
        willA.Play();
    }
}
