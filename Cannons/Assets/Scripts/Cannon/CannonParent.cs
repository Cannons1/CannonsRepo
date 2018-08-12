using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Points))]
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


    protected virtual void Update()
    {
        if (Input.GetButtonUp("Fire1") && canShoot && IGLevelManager.unpause)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        willBody = Will.will.gameObject.GetComponent<Rigidbody>();
        Will.will.gameObject.transform.SetParent(null);
        willBody.isKinematic = false;     
        willBody.velocity = Will.will.transform.up * shootForce;
        Will.will.inCannon = false;
        canShoot = false;
        Will.will.cannonTriggered.SetActive(false);
        Will.will.StartCoroutine(Will.will.FlyAnimation());
    }
  
    public IEnumerator Wick()
    {
        float i = 0;
        while (i < wickTime && Will.will.inCannon)
        {
            i += Time.deltaTime;
            yield return null;
        }
        if(Will.will.inCannon)
        {
            Shoot();
        }
    }
}
