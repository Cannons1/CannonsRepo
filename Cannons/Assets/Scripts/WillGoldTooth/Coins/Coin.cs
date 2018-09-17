using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour, ICoins
{
    WriteVbles mWriteVbles;
    SphereCollider mSphereCollider;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mSphereCollider = GetComponent<SphereCollider>();
    }

    public void CoinsCollected(int _Coin)
    {
        Singleton.instance.Coins += _Coin;
        mWriteVbles.WritingNumberOfCoins();//Will write the number of coins in a text
        DeactivatedCoin();//When you pick a coin it will deactivate its meshRenderer and box collider
    }

    public void DeactivatedCoin()
    {
        anim.SetBool("Get", true);
        mSphereCollider.enabled = false;
        StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
    }

    IEnumerator FadeOut(SpriteRenderer _Sprite)
    {
        Color tmpColor = _Sprite.color;
        while (tmpColor.a > 0f) {
            tmpColor.a -= Time.deltaTime * 2f;
            _Sprite.color = tmpColor;

            if (tmpColor.a <= 0f)
                tmpColor.a = 0f;

            yield return null;
        }
        _Sprite.color = tmpColor;
    }
}
