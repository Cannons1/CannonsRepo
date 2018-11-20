using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform target;

    [SerializeField] float smoothSpeed;
    Vector3 offset;
    float orientation = 1;
    [SerializeField] bool downOrientation;

    private void Start()
    {
        offset = new Vector3(0, 2f * orientation, -10);
        target = Will.will.gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (downOrientation)
        {
            if (Vector3.Dot(target.transform.up, Vector3.down) < 0) orientation = 1f;
            else orientation = -1f;
            offset = new Vector3(0, 2f * orientation, -10f);
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(0, transform.position.y, transform.position.z), new Vector3(0, desiredPosition.y, desiredPosition.z), smoothSpeed);
        transform.position = smoothedPosition;
    }
}
