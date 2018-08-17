using System.Collections;
using UnityEngine;

public class HAndV : CannonParent
{
    Vector3 target1;
    Vector3 target2;

    [SerializeField] private float speed = 5f;
    float time = 0.2f;
    [Range(-180, 180)]
    [SerializeField] int firstRotation;
    [SerializeField] bool initMoving;
    float timer = 0f;

    private void Start()
    {
        target1 = transform.GetChild(2).transform.position;
        target2 = transform.GetChild(3).transform.position;

        cannonType = CannonType.targetCannon;

        if (initMoving)
        {
            StartCoroutine(InitMove());
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator InitMove()
    {
        while (initMoving)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(target1, target2, (Mathf.Sin(speed * timer - (Mathf.PI / 2)) + 1.0f) / 2.0f);
            yield return null;
        }
    }

    public IEnumerator Preparation()
    {

        if (!initMoving)
        {
            StartCoroutine(MoveToFirstTarget()); 
        }

        Vector3 startingRotation = transform.localEulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, startingRotation.z + firstRotation);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(startingRotation, targetRotation, (elapsedTime / time));
            yield return new WaitForFixedUpdate();
        }
        transform.eulerAngles = targetRotation;

        StartCoroutine(Wick());
    }

    IEnumerator MoveToFirstTarget()
    {
        float elapsedTime = 0;
        float targetTime = Vector3.Distance(transform.position, target1) / speed;

        while (elapsedTime < targetTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target1, speed * Time.deltaTime);
            yield return null;
        }
        //transform.position = target1;
        StartCoroutine(Move()); 
    }

    IEnumerator Move()
    {
        while (Will.will.inCannon)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(target1, target2, (Mathf.Sin(speed * timer - (Mathf.PI / 2)) + 1.0f) / 2.0f);
            yield return null;
        }
    }
}