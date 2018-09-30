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
    

    [SerializeField] Shader fadeShader;
    float fadeTime = 3.5f;

    protected virtual void Start()
    {
        mAnimator = transform.GetChild(1).GetComponent<Animator>();
        wick = transform.GetChild(2).gameObject;
        wick.transform.localScale = new Vector3(wick.transform.localScale.x, (wickMaxScale / wickScaleFactor) * wickTime, transform.localScale.z);
        //wickParticle = wick.transform.GetChild(0).gameObject;
        pathWick = wick.transform.GetChild(1).gameObject.GetComponent<PathWick>();       
    }
    protected virtual void Update()
    {

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
        VFX.explosion.GetComponent<Animator>().SetTrigger("explosion");
        //Will.will.GetComponent<WillAudios>().BeingShot();
    }

    public IEnumerator Wick()
    {
        //StartCoroutine(GetComponent<AudioCannons>().Fade());        
        GetComponent<AudioCannons>().AudioWick();
        reference = transform.GetChild(0).gameObject;

        VFX.explosion.transform.SetParent(reference.transform, false);
        VFX.explosion.transform.position = new Vector3(reference.transform.position.x, reference.transform.position.y + 0.1f, -1f);

        StartCoroutine(Tap());
        canShoot = true;
        mRenderer = transform.GetChild(1).GetComponentInChildren<Renderer>();
        wickRenderer = wick.transform.GetComponentInChildren<Renderer>();

        Color startingColor = mRenderer.material.color;

        VFX.wickParticle.transform.SetParent(wick.transform);
        VFX.wickParticle.SetActive(true);
        VFX.wickParticle.transform.position = pathWick.points[pathPoint].position;

        float i = 0;
        while (i < wickTime && Will.will.inCannon)
        {

            i += Time.deltaTime;
            mRenderer.material.color = Color.Lerp(startingColor, Color.red, i / wickTime);
            wickRenderer.material.SetFloat("_fadeFactor", i / wickTime);

            float distance = Vector3.Distance(VFX.wickParticle.transform.position, pathWick.points[pathPoint].position);
            VFX.wickParticle.transform.position = new Vector3(VFX.wickParticle.transform.position.x, VFX.wickParticle.transform.position.y, pathWick.points[pathPoint].position.z - 0.01f);
            VFX.wickParticle.transform.position = Vector3.Lerp(VFX.wickParticle.transform.position, pathWick.points[pathPoint].position, Time.deltaTime / (wickTime / (pathWick.points.Length - 1)));            

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
        yield return new WaitForSeconds(0.1f);
        while (mRenderer.material.GetFloat("_alpha") > 0)
        {
            i -= Time.deltaTime * fadeTime;
            mRenderer.material.SetFloat("_alpha", i);
            yield return null;
        }
        
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
