using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour {

    [SerializeField] bool activator;
    [SerializeField] bool cameraDownOrientation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Will>() != null)
        {
            if (activator) Physics.IgnoreLayerCollision(9, 10, true);
            else Physics.IgnoreLayerCollision(9, 10, false);

            if (cameraDownOrientation) CameraMovement.downOrientation = true;
            gameObject.SetActive(false);
        }
    }
}
