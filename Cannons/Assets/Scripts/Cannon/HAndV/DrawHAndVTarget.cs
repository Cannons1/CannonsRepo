﻿
using UnityEngine;

public class DrawHAndVTarget : MonoBehaviour
{
    public Transform from;
    public Transform to;

    private void OnDrawGizmosSelected()
    {
        if (from != null && to != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, to.position);
        }
    }
}
