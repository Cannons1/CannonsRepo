using UnityEngine;

public class WillAudios : MonoBehaviour
{
    AudioClip[] willAudios;

    private void Start()
    {
        willAudios = Resources.LoadAll<AudioClip>("Sounds/Fx/WillGoldTooth");
    }

    public void DieAudio() {
        GetComponent<AudioSource>().clip = willAudios[0];
        GetComponent<AudioSource>().Play();
    }
}
