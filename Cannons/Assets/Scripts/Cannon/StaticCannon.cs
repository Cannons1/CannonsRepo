using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannon : CannonParent
{
    private void Start()
    {
        cannonType = CannonType.staticCannon;
    }

    protected override void Update ()
    {
        base.Update();
    }
}
