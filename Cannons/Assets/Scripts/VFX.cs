using System.Collections;
using System.Collections.Generic;
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
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        
    }

    private void Start()
    {
        //enteringParticle = enteringCannon.GetComponent<ParticleSystem>();
    }

}
