using System.Collections;
using UnityEngine;

public class RotatingCannon : CannonParent
{
    [SerializeField] float rotationSpeed, angleRotation;
    Vector3 initialRotation;

    protected override void Start()
    {
        base.Start();
        initialRotation = transform.eulerAngles;     
        cannonType = CannonType.rotatingCannon;
        GetComponentInChildren<SkinnedMeshRenderer>().material = GameManager.Instance.Rotating;
    }

    protected override void Update()
    {
        base.Update();
    }

    public IEnumerator CannonRotate()
    {
        float timeOffset = Time.time;
        float angleOffset = transform.eulerAngles.z;
        canShoot = true;
        StartCoroutine(Wick());

        while (Will.will.inCannon)
        {
            float angle = (Mathf.Sin((Time.time - timeOffset) * rotationSpeed) * angleRotation) + angleOffset;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        transform.eulerAngles = initialRotation;

    }

}
