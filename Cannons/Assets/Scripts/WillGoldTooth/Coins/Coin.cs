using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICoins
{
    [SerializeField] WriteVbles mWriteCoins;
    MeshRenderer mMeshRenderer;
    BoxCollider mBoxCollider;
    [SerializeField] GameObject coinPlus;

    private void Start()
    {
        Singleton.instance.CoinsInGame = 0;
        mMeshRenderer = GetComponent<MeshRenderer>();
        mBoxCollider = GetComponent<BoxCollider>();
    }

    public void CoinsCollected(int _Coin)
    {
        Singleton.instance.Coins += _Coin;
        Singleton.instance.CoinsInGame += _Coin;
        mWriteCoins.WritingNumberOfCoins();//Will write the number of coins in a text
        DeactivatedCoin();//When you pick a coin it will deactivate its meshRenderer and box collider
    }

    public void MinusCoinsInGame()
    {
        Singleton.instance.Coins -= Singleton.instance.CoinsInGame;
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
}
