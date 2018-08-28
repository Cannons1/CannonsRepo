using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollectingCoinExpPoints))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WillAudios))]
[RequireComponent(typeof(AudioSource))]
public class Will : MonoBehaviour
{
    [HideInInspector] public GameObject cannonTriggered;
    Transform reference;
    private Rigidbody mRigid;
    Animator anim;
    public Animator _anim { get { return anim; } set { anim = value; } }
    public static Will will = null;

    [HideInInspector] public bool inCannon = false;
    float velocity;
    float time = 0.1f;

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
            //GetComponent<WillAudios>().DieAudio(); // Will Die Audio
        }

        if (cannonTriggered.GetComponent<WinCondition>() != null) {
            WinCondition winCondition;
            winCondition = cannonTriggered.GetComponent<WinCondition>();
            winCondition.Win();
        }
    }

    void StuckOnCannon()
    {
        //GetComponent<WillAudios>().LandsInCannon(); //this is the audio of will landing in a cannon
        GetComponent<Rigidbody>().isKinematic = true;
        reference = cannonTriggered.transform.GetChild(0).gameObject.transform;
        StartCoroutine(MoveToCannon());
        transform.SetParent(cannonTriggered.transform);  
    }

    void AlredyinCannon()
    {
        switch (cannonTriggered.GetComponent<CannonParent>().cannonType)
        {
            case CannonType.staticCannon:
                StartCoroutine(cannonTriggered.GetComponent<StaticCannon>().Preparation());
                break;
            case CannonType.targetCannon:
                StartCoroutine(cannonTriggered.GetComponent<HAndV>().Preparation());
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
        Vector3 startingRotation = transform.eulerAngles;
        Vector3 targetRotation = reference.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(startingRotation, targetRotation, (elapsedTime / time));
            transform.position = Vector3.Lerp(transform.position, reference.position, (elapsedTime / time));
            yield return new WaitForFixedUpdate();
        }
        transform.eulerAngles = targetRotation;
        transform.position = reference.position;

        inCannon = true;
        AlredyinCannon();

        anim.SetBool("InCannon", true);
        /*
        yield return new WaitForSeconds(0.001f);
        var playerClip = _anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(playerClip[0].clip.name);
        yield return new WaitForSeconds(playerClip[0].clip.length);
        */
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
