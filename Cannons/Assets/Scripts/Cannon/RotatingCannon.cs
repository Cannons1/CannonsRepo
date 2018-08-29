using System.Collections;
using UnityEngine;

public class RotatingCannon : CannonParent
{
    [SerializeField] float rotationSpeed, angleRotation;

    protected override void Start()
    {
        base.Start();
        cannonType = CannonType.rotatingCannon;
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

        
    }
}
