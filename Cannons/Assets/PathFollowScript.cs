using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFollowScript : MonoBehaviour {

    [SerializeField] PathWick pathWick;
    int currentPoint = 0;

	void Start () {
		
	}
	
	void Update () {


        
        float distance = Vector3.Distance(pathWick.points[currentPoint].position , transform.position);

        transform.position = Vector3.MoveTowards(transform.position, pathWick.points[currentPoint].position, Time.deltaTime * 10);

        if(distance <= 0)
        {
            currentPoint++;
        }
        
        /*
        if (currentPoint >= pathFollow.points.Length)
        {
            currentPoint = 0;
        }
        */
	}
}
