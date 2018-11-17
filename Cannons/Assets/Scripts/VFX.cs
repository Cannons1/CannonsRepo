using UnityEngine;

public class VFX : MonoBehaviour {

    private static VFX instance;
    public static VFX Instance { get { return instance; } }
   
    [HideInInspector] public int explosionIndex;

    public GameObject[] explosion;
    public GameObject wickParticle, enteringCannon;
    public GameObject bounce;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
