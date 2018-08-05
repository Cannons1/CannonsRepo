using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteCoin : MonoBehaviour, IRouletteCoin
{
    [SerializeField] LvlMgr mLvlMgr;

    public void RoulletteCoinCollected(bool _RouletteCoin)
    {
        StartCoroutine(DieWillAnim());
    }

    IEnumerator DieWillAnim()
    {
        yield return new WaitForSeconds(4);//this will be the time of will's dead animation
        mLvlMgr.RouletteScene();
    }
}
