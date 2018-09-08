using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour, ICoins, IExperience
{
    WriteVbles mWriteVbles;
    CapsuleCollider mCapsuleCollider;
    SpriteRenderer spriteRenderer;
    Animator anim;

    Color color;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        color.a = 255f;
        spriteRenderer.color = color;
        anim = GetComponent<Animator>();
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mCapsuleCollider = GetComponent<CapsuleCollider>();
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
        anim.SetBool("Get", true);
        mCapsuleCollider.enabled = false;
        StartCoroutine(CoinPlus());
    }

    IEnumerator CoinPlus()
    {
        while (color.a > 0f) {
            color.a -= Time.time;
            Debug.Log(color.a);
            yield return null;
        }
        spriteRenderer.enabled = false;
    }

    public void EarningExperience(int _Experience)
    {
        Singleton.instance.Experience += _Experience;
        Singleton.instance.ExpInGame += _Experience;
        mWriteVbles.WriteExp();
    }
}
