using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour {

    public static GameObject explosion, wickParticle;

    private void Start()
    {
        explosion = transform.Find("Explosion").gameObject;
        wickParticle = transform.Find("Wick").gameObject;
    }

}
