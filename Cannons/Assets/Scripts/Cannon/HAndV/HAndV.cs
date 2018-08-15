using System.Collections;
using UnityEngine;

public class HAndV : CannonParent
{
    Transform target1;
    Transform target2;

    [SerializeField] private float speed = 5f;
    float time = 0.2f;
    private Vector3 start, end, lastPos;
    [Range(-180, 180)]
    [SerializeField] int firstRotation;
    [SerializeField] bool initMoving;

    private void Start()
    {
        cannonType = CannonType.targetCannon;
        target1 = transform.GetChild(2).transform;
        target2 = transform.GetChild(3).transform;
        if (initMoving)
        {
            StartCoroutine(InitMove());
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    //IEnumerator Move()
    //{
    //    canShoot = true;

    //    if (target != null)
    //    {
    //        target.parent = null;
    //        start = transform.position;
    //        end = target.position;
    //    }

    //    while (Will.will.inCannon)
    //    {
    //        if (target != null)
    //        {
    //            float fixedSpeed = speed * Time.deltaTime;
    //            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
    //        }

    //        if (transform.position == target.position)
    //        {
    //            target.position = (target.position == start) ? end : start;
    //        }
    //        yield return null;
    //    }
    //}

    IEnumerator Move()
    {
        target1.SetParent(null);
        target2.SetParent(null);

        while (Will.will.inCannon)
        {
            transform.position = Vector3.Lerp(target1.position, target2.position, Mathf.PingPong(Time.time * speed, 1f));
            yield return null;
        }
    }
    
    IEnumerator InitMove()
    {
        target1.SetParent(null);
        target2.SetParent(null);

        while (initMoving)
        {
            transform.position = Vector3.Lerp(target1.position, target2.position, Mathf.PingPong(Time.time * speed, 1f));
            yield return null;
        }
        yield return null;
    }

    public IEnumerator Preparation()
    {
        initMoving = false;
        target1.SetParent(transform);
        target2.SetParent(transform);
        Vector3 startingRotation = transform.eulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, startingRotation.z + firstRotation);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.eulerAngles = Vector3.LerpUnclamped(startingRotation, targetRotation, (elapsedTime / time));
            yield return new WaitForFixedUpdate();
        }
        transform.eulerAngles = targetRotation;

        StartCoroutine(Move());
        StartCoroutine(Wick());
    }
}
