using UnityEngine;
using System;
using System.Globalization;

public class PathWick : MonoBehaviour {

    public Transform[] points;

    private void Start()
    {
        points = GetComponentsInChildren<Transform>();
        Array.Reverse(points);
    }

    private void OnDrawGizmos()
    {
        points = GetComponentsInChildren<Transform>();
        Array.Reverse(points);
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.color = Color.red;
            Vector3 position = points[i].position;                      
            Gizmos.DrawSphere(position, 0.05f);
            if (i == 0)
                continue;
            Vector3 previous = points[i - 1].position;
            Gizmos.DrawLine(previous, position);
        }
    }

}
