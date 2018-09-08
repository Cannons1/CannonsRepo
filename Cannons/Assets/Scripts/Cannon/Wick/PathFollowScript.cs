using UnityEngine;

public class PathFollowScript : MonoBehaviour {

    [SerializeField] PathWick pathWick;
    int currentPoint = 0;

	void Update () {
        float distance = Vector3.Distance(pathWick.points[currentPoint].position , transform.position);

        transform.position = Vector3.MoveTowards(transform.position, pathWick.points[currentPoint].position, Time.deltaTime * 0.5f);


        if (distance < 0.1f && currentPoint != pathWick.points.Length - 2)
            currentPoint++;
        /*
        if(distance <= 0)
        {
            currentPoint++;
        }
        */

        /*
        if (currentPoint >= pathFollow.points.Length)
        {
            currentPoint = 0;
        }
        */
    }
}
