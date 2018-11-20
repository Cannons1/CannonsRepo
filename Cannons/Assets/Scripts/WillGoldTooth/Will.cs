using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(AudioSource))]
public class Will : MonoBehaviour
{
    [HideInInspector] public GameObject cannonTriggered;
    Transform reference;
    //Vector3 referencePos = new Vector3(0.1f, 1.1f, 0f);
    Collider mCollider;
    private Rigidbody m_Rigidbody;
    Vector3 updateVelocity;
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

    public delegate void WillSounds();
    public WillSounds delWillSounds;
    private bool revive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("AnimControllers/PlayerController") as RuntimeAnimatorController;

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody>();
        mCollider = GetComponent<Collider>();
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

    public void FixedUpdate()
    {
        updateVelocity = m_Rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                if(delWillSounds !=null)
                    delWillSounds();//Send audio of bounce to AudioController
                
                Transform reference = collision.gameObject.transform.GetChild(0);
                VFX.Instance.bounce.transform.SetParent(reference, false);
                VFX.Instance.bounce.transform.position = new Vector3(reference.position.x, reference.transform.position.y, -1f);
                VFX.Instance.bounce.GetComponent<Animator>().SetTrigger("VFXbounce");
                collision.gameObject.GetComponent<Animator>().SetTrigger("bounce");

                ContactPoint contactWall = collision.contacts[0];
                Vector3 direction = Vector3.Reflect(updateVelocity.normalized, contactWall.normal);
                Bounce wallBounce = collision.gameObject.GetComponent<Bounce>();
                m_Rigidbody.AddForce(direction * wallBounce.Force, ForceMode.Impulse);
                break;
            case "LaunchPad":
                ContactPoint contactLaunch = collision.contacts[0];
                Bounce bounce = collision.gameObject.GetComponent<Bounce>();
                m_Rigidbody.AddForce(contactLaunch.normal * bounce.Force, ForceMode.Impulse);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        WinCondition winCondition = other.GetComponent<WinCondition>();
        DieEvent dieEvent = other.GetComponent<DieEvent>();
        ICoins iCoins = other.GetComponent<ICoins>();

        if (other.tag == "Cannon")
        {
            cannonTriggered = other.gameObject;
            other.enabled = false;
            StuckOnCannon();
            cannonTriggered.GetComponent<CannonParent>().mAnimator.SetTrigger("Entering");
            OnProgressLvl(transform.position);
        }
        else if (dieEvent != null)
        {
            mCollider.enabled = false;
            transform.position = reference.position;
            transform.eulerAngles = reference.eulerAngles;
            dieEvent.CharacterDie();
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            m_SpriteRenderer.enabled = false;           
        }
        else if (winCondition != null)
        {
            winCondition.Win(m_Rigidbody);
            mCollider.enabled = false;
            m_SpriteRenderer.enabled = false;
        }
        else if (iCoins != null) {
            iCoins.CollectCoins();
        }
    }


    public void Revive()
    {
        revive = true;
        mCollider.enabled = true;
        anim.SetBool("InCannon", true);
        transform.eulerAngles = reference.eulerAngles;
        transform.position = reference.position;

        m_Rigidbody.constraints = RigidbodyConstraints.None;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        m_SpriteRenderer.enabled = true;
    }

    void AlreadyInRespawnCannon()
    {
        DieEvent.ReactivateCollider();
        switch (cannonTriggered.GetComponent<CannonParent>().cannonType)
        {
            case CannonType.staticCannon:
                StartCoroutine(cannonTriggered.GetComponent<StaticCannon>().Wick());
                break;
            case CannonType.targetCannon:
                StartCoroutine(cannonTriggered.GetComponent<HAndV>().Move());
                StartCoroutine(cannonTriggered.GetComponent<HAndV>().Wick());
                break;
            case CannonType.rotatingCannon:
                StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().CannonRotate());
                StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().Wick());
                break;
        }
        revive = false;
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
                StartCoroutine(cannonTriggered.GetComponent<RotatingCannon>().Wick());
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
        if (!revive) AlredyinCannon();
        else AlreadyInRespawnCannon();
        anim.SetBool("InCannon", true);
    }

    public IEnumerator FlyAnimation()
    {
        anim.SetBool("InCannon", inCannon);
        while (!inCannon)
        {
            velocity = (m_Rigidbody.velocity.y < 0 && (Vector3.Dot(cannonTriggered.transform.up, Vector3.down) < 0)) ? velocity = -1 : velocity = 1;         
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
