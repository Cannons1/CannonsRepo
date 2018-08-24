using System.Collections;
using UnityEngine;
using EZCameraShake;

//[RequireComponent(typeof(Points))]
[RequireComponent(typeof(AudioCannons))]
public abstract class CannonParent : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float wickTime;
    Vector3 direction;
    [SerializeField] protected float shootForce = 22f;
    Rigidbody willBody;
    GameObject reference;
    [HideInInspector] public CannonType cannonType;
    protected bool canShoot;
    Renderer mRenderer;

    protected virtual void Update()
    {
        if (Input.GetButtonUp("Fire1") && canShoot && IGLevelManager.unpause)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GetComponent<AudioCannons>().AudioShoot();
        willBody = Will.will.gameObject.GetComponent<Rigidbody>();
        Will.will.gameObject.transform.SetParent(null);
        willBody.isKinematic = false;     
        canShoot = false;
        willBody.velocity = Will.will.transform.up * shootForce;
        CameraShaker.Instance.ShakeOnce(1f, 1.5f, 0.1f, 0.3f);
        Will.will.inCannon = false;
        Will.will.cannonTriggered.SetActive(false);
        Will.will.StartCoroutine(Will.will.FlyAnimation());
        //Will.will.GetComponent<WillAudios>().BeingShot();
    }
  
    public IEnumerator Wick()
    {
        canShoot = true;
        mRenderer = transform.GetChild(1).GetComponentInChildren<Renderer>();
        Color startingColor = mRenderer.material.color;
        float i = 0;
        while (i < wickTime && Will.will.inCannon)
        {
            i += Time.deltaTime;
            mRenderer.material.color = Color.Lerp(startingColor, Color.red, i / wickTime);
            yield return null;
        }
        if(Will.will.inCannon)
        {
            Shoot();
        }
    }
}
