using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAndV : CannonParent
{
    Transform target;

    private float speed = 2.5f;
    private Vector3 start, end, lastPos;

    void Start()
    {

        target = transform.GetChild(2).transform;

        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }

        StartCoroutine(Move());
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Move()
    {
        while(true)
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
            yield return null;
        }
    }
}
