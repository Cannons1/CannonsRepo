using System.Collections;
using UnityEngine;

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
    static float speed = 2f;
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
        transform.SetParent(cannonTriggered.gameObject.transform);  
    }

    void AlredyinCannon()
    {
        switch (cannonTriggered.GetComponent<CannonParent>().cannonType)
        {
            case CannonType.staticCannon:
                StartCoroutine(cannonTriggered.GetComponent<CannonParent>().Wick());
                break;
            case CannonType.targetCannon:
                StartCoroutine(cannonTriggered.GetComponent<HAndV>().Preparation(180));
                break;
            case CannonType.rotatingCannon:
                StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().CannonRotate());
                break;
            default:
                break;
        }
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

        anim.SetBool("InCannon", true);

        yield return new WaitForSeconds(0.001f);
        var playerClip = _anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(playerClip[0].clip.name);
        yield return new WaitForSeconds(playerClip[0].clip.length);

        inCannon = true;

        AlredyinCannon();
    }

    public IEnumerator FlyAnimation()
    {
        anim.SetBool("InCannon", inCannon);
        while (!inCannon)
        {
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
