using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannon : CannonParent
{
    [Range(-180, 180)]
    [SerializeField] int firstRotation;
    float time = 0.2f;

    protected override void Start()
    {
        base.Start();
        cannonType = CannonType.staticCannon;
        GetComponentInChildren<SkinnedMeshRenderer>().material = GameController.Instance.StaticCannon;
    }

    protected override void Update()
    {
        base.Update(); 
    }

    public IEnumerator Preparation()
    {
        Vector3 startingRotation = transform.eulerAngles;
        Vector3 targetRotation = new Vector3(0, 0, startingRotation.z + firstRotation);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.eulerAngles = Vector3.LerpUnclamped(startingRotation, targetRotation, (elapsedTime / time));
            yield return new WaitForFixedUpdate();
        }
        transform.eulerAngles = targetRotation;
        StartCoroutine(Wick());
    }

    public override void SetPosition()
    {
        //transform.position = initialPos;
        //transform.eulerAngles = initialRot;
    }
}
