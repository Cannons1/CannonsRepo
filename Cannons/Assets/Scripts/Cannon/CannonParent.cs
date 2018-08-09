using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Points))]
public abstract class CannonParent : MonoBehaviour
{
    [SerializeField] float wickTime;
    Vector3 direction;
    static float shootForce = 20f;
    Rigidbody willBody;
    GameObject reference;

    protected virtual void Update()
    {
        if (Input.GetButtonUp("Fire1") && Will.will.inCannon && LvlMgr.unpause)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        willBody = Will.will.gameObject.GetComponent<Rigidbody>();
        Will.will.gameObject.transform.SetParent(null);
        willBody.isKinematic = false;
        willBody.velocity = Will.will.cannonTriggered.transform.up * shootForce;
        Will.will.inCannon = false;
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
