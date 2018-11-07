using System.Collections;
using UnityEngine;
using EZCameraShake;

//[RequireComponent(typeof(Points))]

//[ExecuteInEditMode]
[RequireComponent(typeof(AudioCannons))]
public abstract class CannonParent : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float wickTime;
    float wickScaleFactor = 10f, wickMaxScale = 1.5f;
    protected GameObject wick, wickParticle;
    protected PathWick pathWick;
    int pathPoint = 0;
    Vector3 direction;
    [SerializeField] protected float shootForce = 22f;
    Rigidbody willBody;
    GameObject reference;
    [HideInInspector] public CannonType cannonType;
    protected bool canShoot;
    [SerializeField]
    Renderer mRenderer, wickRenderer;
    public Animator mAnimator;
    private AudioCannons m_AudioCannons;

    [SerializeField] Shader fadeShader;
    float fadeTime = 3.5f;
    WaitForSecondsRealtime littleWait;
    protected Vector3 initialRot;

    protected virtual void Start()
    {
        littleWait = new WaitForSecondsRealtime(0.1f);
        initialRot = transform.eulerAngles;
        mAnimator = transform.GetChild(1).GetComponent<Animator>();
        wick = transform.GetChild(2).gameObject;
        wick.transform.localScale = new Vector3(wick.transform.localScale.x, (wickMaxScale / wickScaleFactor) * wickTime, transform.localScale.z);
        //wickParticle = wick.transform.GetChild(0).gameObject;
        pathWick = wick.transform.GetChild(1).gameObject.GetComponent<PathWick>();
        m_AudioCannons = GetComponent<AudioCannons>();
    }
    protected virtual void Update()
    {
        if (Input.GetButtonUp("Fire1") && canShoot && IGLevelManager.unpause)
        {
            mAnimator.SetTrigger("Shoot");
            Shoot();
        }
    }
    public void Shoot()
    {
        m_AudioCannons.AudioShoot();
        willBody = Will.will.Rigidbody;
        Will.will.gameObject.transform.SetParent(null);
        willBody.isKinematic = false;
        canShoot = false;
        willBody.velocity = Will.will.transform.up * shootForce;
        CameraShaker.Instance.ShakeOnce(2.6f, 2f, 0.1f, 0.3f);        
        Will.will.inCannon = false;
        //Will.will.cannonTriggered.SetActive(false);
        Will.will.cannonTriggered.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        Will.will.StartCoroutine(Will.will.FlyAnimation());
        VFX.Instance.explosion[VFX.Instance.expIndex].GetComponent<Animator>().SetTrigger("explosion");
        //Will.will.GetComponent<WillAudios>().BeingShot();
    }

    public IEnumerator Wick()
    {
        //StartCoroutine(GetComponent<AudioCannons>().Fade());        
        m_AudioCannons.AudioWick();
        reference = transform.GetChild(0).gameObject;

        VFX.Instance.expIndex = (VFX.Instance.expIndex < 1) ? VFX.Instance.expIndex + 1 : 0 ;
        VFX.Instance.explosion[VFX.Instance.expIndex].transform.SetParent(reference.transform, false);
        VFX.Instance.explosion[VFX.Instance.expIndex].transform.position = new Vector3(reference.transform.position.x, reference.transform.position.y + 0.4f, -1f);

        canShoot = true;
        mRenderer = transform.GetChild(1).GetComponentInChildren<Renderer>();
        wickRenderer = wick.transform.GetComponentInChildren<Renderer>();

        Color startingColor = mRenderer.material.color;

        VFX.Instance.wickParticle.transform.SetParent(wick.transform, false);
        VFX.Instance.wickParticle.SetActive(true);
        VFX.Instance.wickParticle.transform.position = pathWick.points[pathPoint].position;

        float i = 0;
        while (i < wickTime && Will.will.inCannon)
        {

            i += Time.deltaTime;
            mRenderer.material.color = Color.Lerp(startingColor, Color.red, i / wickTime);
            wickRenderer.material.SetFloat("_fadeFactor", i / wickTime);

            float distance = Vector3.Distance(VFX.Instance.wickParticle.transform.position, pathWick.points[pathPoint].position);
            VFX.Instance.wickParticle.transform.position = new Vector3(VFX.Instance.wickParticle.transform.position.x, VFX.Instance.wickParticle.transform.position.y, pathWick.points[pathPoint].position.z - 0.01f);
            VFX.Instance.wickParticle.transform.position = Vector3.Lerp(VFX.Instance.wickParticle.transform.position, pathWick.points[pathPoint].position, Time.deltaTime / (wickTime / (pathWick.points.Length - 1)));            

            if (distance < 0.1f && pathPoint != pathWick.points.Length - 1)
                pathPoint++;
            yield return null;
        }


        if (Will.will.inCannon)
        {
            Shoot();
        }
        wick.SetActive(false);
        mRenderer.material.shader = fadeShader;
        i = 1;

        yield return littleWait;
        while (mRenderer.material.GetFloat("_alpha") > 0)
        {
            i -= Time.deltaTime * fadeTime;
            mRenderer.material.SetFloat("_alpha", i);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.3f);
        SetPosition();
    }

    public abstract void SetPosition();

    public void Reactivate()
    {
        pathPoint = 0;
        Will.will.cannonTriggered.transform.GetComponent<Collider>().enabled = true;
        wickRenderer.material.SetFloat("_fadeFactor", 0);
        wick.SetActive(true);
        VFX.Instance.wickParticle.transform.position = pathWick.points[0].position;
        mRenderer.material = GameManager.Instance.StaticCannon;
    }

    //public IEnumerator Tap()
    //{
    //    while (Will.will.inCannon)
    //    {
    //        if (Input.GetButtonUp("Fire1") && canShoot && IGLevelManager.unpause)
    //        {               
    //            mAnimator.SetTrigger("Shoot");
    //            Shoot();               
    //        }
    //        yield return null;
    //    }
    //}
}
