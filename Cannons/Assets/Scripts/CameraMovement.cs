﻿using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform target;

    [SerializeField] float smoothSpeed;
    Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, 2, -10);
        target = Will.will.gameObject.transform;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(0, transform.position.y, transform.position.z), new Vector3(0, desiredPosition.y, desiredPosition.z), smoothSpeed);
        transform.position = smoothedPosition;
    }
}
