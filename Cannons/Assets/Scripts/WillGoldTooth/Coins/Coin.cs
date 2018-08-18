﻿using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour, ICoins, IExperience
{
    WriteVbles mWriteVbles;
    MeshRenderer mMeshRenderer;
    BoxCollider mBoxCollider;
    [Header("Object when player picks up")]
    [SerializeField] GameObject coinPlus;

    private void Start()
    {
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mMeshRenderer = GetComponent<MeshRenderer>();
        mBoxCollider = GetComponent<BoxCollider>();
    }

    public void CoinsCollected(int _Coin)
    {
        Singleton.instance.Coins += _Coin;
        Singleton.instance.CoinsInGame += _Coin;
        mWriteVbles.WritingNumberOfCoins();//Will write the number of coins in a text
        DeactivatedCoin();//When you pick a coin it will deactivate its meshRenderer and box collider
    }

    public void DeactivatedCoin()
    {
        mMeshRenderer.enabled = false;
        mBoxCollider.enabled = false;
        StartCoroutine(CoinPlus());
    }

    WaitForSeconds lifeTimeCoin = new WaitForSeconds(0.75f);

    IEnumerator CoinPlus()
    {
        GameObject a = Instantiate(coinPlus, transform.position, Quaternion.identity);
        yield return lifeTimeCoin;
        Destroy(a);
    }

    public void EarningExperience(int _Experience)
    {
        Singleton.instance.Experience += _Experience;
        Singleton.instance.ExpInGame += _Experience;
        mWriteVbles.WriteExp();
    }
}
