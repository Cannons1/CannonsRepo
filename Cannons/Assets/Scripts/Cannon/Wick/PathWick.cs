using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWick : MonoBehaviour {

    public Transform[] points;

    private void OnDrawGizmos()
    {
        points = GetComponentsInChildren<Transform>();

        for(int i = 1; i < points.Length; i++)
        {
            Gizmos.color = Color.red;
            Vector3 position = points[i].position;
            Vector3 previous = points[i - 1].position;
            Gizmos.DrawLine(previous, position);
            Gizmos.DrawSphere(position, 0.05f);
        }
    }

}
