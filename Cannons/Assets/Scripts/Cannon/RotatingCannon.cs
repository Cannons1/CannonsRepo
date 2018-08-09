using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotatingCannon : CannonParent
{
    [SerializeField] float rotationSpeed, angleRotation;
    AnimatorClipInfo[] playerClip;
    protected override void Update()
    {
        base.Update();
    }

    public IEnumerator CannonRotate()
    {
        yield return new WaitForSeconds(0.001f);
        var playerClip = Will.will._anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(playerClip[0].clip.name);
        yield return new WaitForSeconds(playerClip[0].clip.length);

        float timeOffset = Time.time;
        float angleOffset = transform.eulerAngles.z;

        while (Will.will.inCannon)
        {
            float angle = (Mathf.Sin((Time.time - timeOffset) * rotationSpeed) * angleRotation) + angleOffset;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }
    }
}
