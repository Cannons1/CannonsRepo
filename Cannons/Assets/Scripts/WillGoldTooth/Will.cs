using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CollectingCoinExpPoints))]
[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(WillAudios))]
[RequireComponent(typeof(AudioSource))]
public class Will : MonoBehaviour
{
    [HideInInspector] public GameObject cannonTriggered;
    Transform reference;
    private Rigidbody m_Rigidbody;
    public Rigidbody Rigidbody
    {
        get
        {
            return m_Rigidbody;
        }
    }
    Animator anim;
    public Animator _anim { get { return anim; } set { anim = value; } }
    public static Will will = null;
    private SpriteRenderer m_SpriteRenderer;

    [HideInInspector] public bool inCannon = false;
    float velocity;
    float time = 0.1f;

    public delegate void WillDelegate(Vector3 _mTransform);
    public event WillDelegate OnProgressLvl;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("AnimControllers/PlayerController") as RuntimeAnimatorController;

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;

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
            cannonTriggered.GetComponent<CannonParent>().mAnimator.SetTrigger("Entering");
            OnProgressLvl(transform.position);
        }

        if (cannonTriggered.GetComponent<DieEvent>() != null)
        {
            DieEvent dieEvent;
            dieEvent = cannonTriggered.GetComponent<DieEvent>();
            dieEvent.ActiveCanvasRetry();
            //GetComponent<WillAudios>().DieAudio(); // Will Die Audio
        }

        if (cannonTriggered.GetComponent<WinCondition>() != null) {
            WinCondition winCondition;
            winCondition = cannonTriggered.GetComponent<WinCondition>();
            m_SpriteRenderer.enabled = false;
            winCondition.Win(m_Rigidbody);
        }
    }

    void StuckOnCannon()
    {
        //GetComponent<WillAudios>().LandsInCannon(); //this is the audio of will landing in a cannon
        

        m_Rigidbody.isKinematic = true;
        reference = cannonTriggered.transform.GetChild(0).gameObject.transform;

        //VFX.Instance.enteringCannon.transform.SetParent(reference.transform, false);
        //VFX.Instance.enteringCannon.transform.position = new Vector3(reference.transform.position.x, reference.transform.position.y, -5f);
        //VFX.Instance.enteringCannon.GetComponent<Animator>().SetTrigger("entering");

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
            velocity = (Mathf.Sign(m_Rigidbody.velocity.y) > 0) ? velocity = 1 : velocity = -1;
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
