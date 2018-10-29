using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ave : MonoBehaviour {

    [SerializeField] Transform target;

    Rigidbody mRigid;
    Vector3 initialPosition;
    private void Start()
    {
        mRigid = GetComponent<Rigidbody>();
        initialPosition = transform.localPosition;
        mRigid.AddForce(Vector3.left * 2f, ForceMode.Impulse);
    }

    private void VerifyPosition()
    {
        if (transform.position.x < -4) transform.position = initialPosition;
    }

    /*
    private float speed = 5f;
    private Vector3 start, end, lastPos;

    bool visible = false;

    void Start()
    {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

    //private IEnumerator MoveAve() {
    //    while (transform.position != target.position) {
    //        float fixedSpeed = speed * Time.deltaTime;
    //        transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
    //        yield return null;
    //    }
    //    target.position = (target.position == start) ? end : start;
    //    StartCoroutine(MoveAve());
    //}

    private void FixedUpdate()
    {
        if (visible)
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

    private void OnTriggerEnter(Collider other)
    {
        Will will = other.GetComponent<Will>();

        if (will != null) {
            gameObject.SetActive(false);
        }
    }
    */
}
