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

    public void LandsInCannon() {
        //int clip = Random.Range(1, 3);
        willA.clip = willAudios[2];
        willA.Play();
    }

    public void BeingShot() {
        willA.clip = willAudios[3];
        willA.Play();
    }

    public void FlyingAudio() {
        willA.clip = willAudios[3];
        willA.Play();
    }

    public void HitsAWallAudio() {
        willA.clip = willAudios[4];
        willA.Play();
    }
}
