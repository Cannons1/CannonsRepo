using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour {

    private static VFX instance;
    public static VFX Instance { get { return instance; } }

    //public static GameObject explosion, wickParticle;

    [SerializeField] public GameObject[] explosion;
    [HideInInspector] public int expIndex;
    [SerializeField] public GameObject wickParticle;



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
        //explosion = transform.Find("Explosion").gameObject;
        //wickParticle = transform.Find("Wick").gameObject;
    }

}
