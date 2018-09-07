using System.Collections;
using UnityEngine;
using EZCameraShake;

//[RequireComponent(typeof(Points))]
[RequireComponent(typeof(AudioCannons))]
public abstract class CannonParent : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float wickTime;
    protected GameObject wick, wickParticle;
    protected PathWick pathWick;
    int pathPoint = 0;
    Vector3 direction;
    [SerializeField] protected float shootForce = 22f;
    Rigidbody willBody;
    GameObject reference;
    [HideInInspector] public CannonType cannonType;
    protected bool canShoot;
    Renderer mRenderer;
    public Animator mAnimator;


    float fadeTime;
    [SerializeField] Shader fadeShader;

    protected virtual void Start()
    {
        mAnimator = transform.GetChild(1).GetComponent<Animator>();
        fadeTime = 3.5f;
        wick = transform.GetChild(2).gameObject;
        wickParticle = wick.transform.GetChild(0).gameObject;
        pathWick = wick.transform.GetChild(1).gameObject.GetComponent<PathWick>();
    }
    protected virtual void Update()
    {
        /*
        if (Input.GetButtonUp("Fire1") && canShoot && IGLevelManager.unpause)
        {
            Shoot();
        }
        */

    }
    public void Shoot()
    {
        GetComponent<AudioCannons>().AudioShoot();
        willBody = Will.will.gameObject.GetComponent<Rigidbody>();
        Will.will.gameObject.transform.SetParent(null);
        willBody.isKinematic = false;
        canShoot = false;
        willBody.velocity = Will.will.transform.up * shootForce;
        CameraShaker.Instance.ShakeOnce(2.6f, 2f, 0.1f, 0.3f);        
        Will.will.inCannon = false;
        //Will.will.cannonTriggered.SetActive(false);
        Will.will.cannonTriggered.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        Will.will.StartCoroutine(Will.will.FlyAnimation());
        //Will.will.GetComponent<WillAudios>().BeingShot();
    }

    public IEnumerator Wick()
    {
        //StartCoroutine(GetComponent<AudioCannons>().Fade());        
        GetComponent<AudioCannons>().AudioWick();
        StartCoroutine(Tap());
        canShoot = true;
        mRenderer = transform.GetChild(1).GetComponentInChildren<Renderer>();
        Color startingColor = mRenderer.material.color;

        wickParticle.SetActive(true);
        wickParticle.transform.position = pathWick.points[pathPoint].position;

        float i = 0;
        while (i < wickTime && Will.will.inCannon)
        {

            i += Time.deltaTime;
            mRenderer.material.color = Color.Lerp(startingColor, Color.red, i / wickTime);

            float distance = Vector3.Distance(wickParticle.transform.position, pathWick.points[pathPoint].position);
            wickParticle.transform.position = Vector3.MoveTowards(wickParticle.transform.position, pathWick.points[pathPoint].position, (Time.deltaTime / wickTime));

            if (distance < 0.1f && pathPoint != pathWick.points.Length - 1)
                pathPoint++;
            yield return null;
        }


        if (Will.will.inCannon)
        {
            Shoot();
        }

        mRenderer.material.shader = fadeShader;
        i = 1;

        while (mRenderer.material.GetFloat("_alpha") > 0)
        {
            i -= Time.deltaTime * fadeTime;
            mRenderer.material.SetFloat("_alpha", i);
            yield return null;
        }

        wick.SetActive(false);

        /*
        mRenderer.material.SetFloat("_Surface", 1);
        i = 0;
        Color currentColor = mRenderer.material.color;
        Color zeroAlpha = mRenderer.material.color;
        zeroAlpha.a = 0f;      
        while (mRenderer.material.color.a > 0f)
        {           
            i += Time.deltaTime;
            material.color = Color.Lerp(currentColor, zeroAlpha, i/fadeTime);
            mRenderer.UpdateGIMaterials();
            DynamicGI.UpdateEnvironment();
            yield return null;
        }
        */
    }

    public IEnumerator Tap()
    {
        while (Will.will.inCannon)
        {
            if (Input.GetButtonUp("Fire1") && canShoot && IGLevelManager.unpause)
            {
                mAnimator.SetTrigger("Shoot");
                Shoot();
            }
            yield return null;
        }
    }
}
