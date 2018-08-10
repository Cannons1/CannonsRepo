using System.Collections;
using UnityEngine;

public class HAndV : CannonParent
{
    Transform target;

    private float speed = 2.5f;
    float time = 0.5f;
    private Vector3 start, end, lastPos;

    private void Start()
    {
        cannonType = CannonType.targetCannon;

        target = transform.GetChild(2).transform;

    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Move()
    {
        canShoot = true;

        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }

        while (Will.will.inCannon)
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

    public IEnumerator Preparation(int _rotation)
    {
        Vector3 startingRotation = transform.eulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, startingRotation.z + _rotation);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.eulerAngles = Vector3.LerpUnclamped(startingRotation, targetRotation, (elapsedTime / time));
            yield return new WaitForFixedUpdate();
        }
        transform.eulerAngles = targetRotation;

        StartCoroutine(Wick());
        StartCoroutine(Move());
    }
}
