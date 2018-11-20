using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour {

    [SerializeField] bool activator;

    private void OnTriggerEnter(Collider other)
    {
        if (activator) Physics.IgnoreLayerCollision(9, 10, true);
        else Physics.IgnoreLayerCollision(9, 10, false);
    }
}
