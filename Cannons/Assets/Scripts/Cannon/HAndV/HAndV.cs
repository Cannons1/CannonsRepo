using System.Collections;
using UnityEngine;

public class HAndV : CannonParent
{
    Vector3 target1, target2;

    [SerializeField] private float speed = 5f;
    float time = 0.2f;
    [Range(-180, 180)]
    [SerializeField] int firstRotation;
    [SerializeField] bool initMoving;
    [Tooltip("False for horizontal, true for vertical")]
    [SerializeField] bool verticalOrHorizontal;
    [Range(-2.3f, 2.3f)]
    [SerializeField] float horizontalTarget1, horizontalTarget2;
    [SerializeField] float verticalTarget1, verticalTarget2;
    float timer = 0f;


    protected override void Start()
    {
        base.Start();
        if(!verticalOrHorizontal)
        {
            target1 = new Vector3(horizontalTarget1, transform.localPosition.y, 0);
            target2 = new Vector3(horizontalTarget2, transform.localPosition.y, 0);
        }
        else
        {
            target1 = new Vector3(transform.localPosition.x, verticalTarget1, 0);
            target2 = new Vector3(transform.localPosition.x, verticalTarget2, 0);
        }

        Vector3[] positions = { target1, target2 };

        GetComponent<LineRenderer>().SetPositions(positions);
        GetComponentInChildren<SkinnedMeshRenderer>().material = GameManager.Instance.Handv;
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
            timer += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(target1, target2, (Mathf.Sin(speed * timer - (Mathf.PI / 2)) + 1.0f) / 2.0f);
            yield return new WaitUntil(delegate { return IGLevelManager.unpause == true; });
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
            elapsedTime += Time.fixedDeltaTime;
            transform.eulerAngles = Vector3.Lerp(startingRotation, targetRotation, (elapsedTime / time));
            yield return new WaitForFixedUpdate();
        }
        transform.eulerAngles = targetRotation;

        StartCoroutine(Wick());
    }

    public IEnumerator MoveToFirstTarget()
    {
        if (Vector3.Distance(transform.localPosition, target1) > 0.5f)
        {
            float t = ((2 * Mathf.PI) / (speed * 2)) / 2;
            iTween.MoveTo(gameObject, iTween.Hash("position", target1, "time", t, "easetype", iTween.EaseType.easeOutSine));
            yield return new WaitForSeconds(t);
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", target1, "time", 0.15f, "easetype", iTween.EaseType.easeOutSine));
            yield return new WaitForSeconds(0.15f);
        }
        StartCoroutine(Move());  
    }

    public IEnumerator Move()
    {
        while (Will.will.inCannon)
        {
            timer += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(target1, target2, (Mathf.Sin(speed * timer - (Mathf.PI / 2)) + 1.0f) / 2.0f);
            yield return new WaitUntil(delegate { return IGLevelManager.unpause == true; });
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!verticalOrHorizontal)
        {
            target1 = new Vector3(horizontalTarget1, transform.localPosition.y, 0);
            target2 = new Vector3(horizontalTarget2, transform.localPosition.y, 0);
        }
        else
        {
            target1 = new Vector3(transform.localPosition.x, verticalTarget1, 0);
            target2 = new Vector3(transform.localPosition.x, verticalTarget2, 0);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(target1, target2);
    }

    public override void SetPosition()
    {
        StopAllCoroutines();
    }
}