using UnityEngine;

public class VFX : MonoBehaviour {

    private static VFX instance;
    public static VFX Instance { get { return instance; } }
   
    [HideInInspector] public int expIndex;

    public GameObject[] explosion;
    public GameObject wickParticle, enteringCannon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
