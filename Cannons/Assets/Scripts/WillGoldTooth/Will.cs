using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollectingCoinExpPoints))]
[RequireComponent(typeof(Animator))]
public class Will : MonoBehaviour
{
    [HideInInspector] public GameObject cannonTriggered;
    Transform reference;
    private Rigidbody mRigid;
    Animator anim;
    public Animator _anim { get { return anim; } set { anim = value; } }
    public static Will will = null;

    [HideInInspector] public bool inCannon = false;
    float speed = 2;
    float velocity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("AnimControllers/PlayerController") as RuntimeAnimatorController;
        mRigid = GetComponent<Rigidbody>();

        if (will == null)
        {
            will = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        cannonTriggered = other.gameObject;

        if (cannonTriggered.tag == "Cannon")
        {
            inCannon = true;
            anim.SetBool("InCannon", inCannon);
            other.enabled = false;
            StuckOnCannon();
        }

        if (cannonTriggered.GetComponent<DieEvent>() != null)
        {
            DieEvent dieEvent;
            dieEvent = cannonTriggered.GetComponent<DieEvent>();
            dieEvent.ChargeMenuLevel();
        }
    }

    void StuckOnCannon()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        reference = cannonTriggered.transform.GetChild(0).gameObject.transform;
        StartCoroutine(MoveToCannon());
        transform.rotation = Quaternion.FromToRotation(transform.up, cannonTriggered.transform.up);
        transform.SetParent(cannonTriggered.gameObject.transform);
        StartCoroutine(cannonTriggered.GetComponent<CannonParent>().Wick());
        switch(cannonTriggered.GetComponent<CannonParent>().cannonType)
        {
            case CannonType.staticCannon:
                break;
            case CannonType.targetCannon:
                StartCoroutine(cannonTriggered.GetComponent<HAndV>().Move());
                break;
            case CannonType.rotatingCannon:
                StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().CannonRotate());
                break;
            default:
                break;
        }
        StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().CannonRotate());
    }

    IEnumerator MoveToCannon()
    {
        float step = (speed / (transform.position - reference.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, reference.position, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, reference.rotation, t);
            yield return new WaitForFixedUpdate();
        }
        transform.position = reference.position;
    }

    public IEnumerator FlyAnimation()
    {
        while (!inCannon)
        {
            anim.SetBool("InCannon", inCannon);
            velocity = (Mathf.Sign(mRigid.velocity.y) > 0) ? velocity = 1 : velocity = -1;
            anim.SetFloat("Velocity", velocity);
            yield return null;
        }
        if (inCannon)
        {
            anim.SetBool("InCannon", inCannon);
            velocity = 0;
        }
    }
}
