﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAndV : MonoBehaviour
{
    [SerializeField] Transform target;

    private float speed = 5f;
    private Vector3 start, end, lastPos;

    void Start()
    {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }

        if (transform.position == target.position)
        {
            target.position = (target.position == start) ? end : start;
        }
    }
}
