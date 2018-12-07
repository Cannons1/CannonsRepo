using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour , ICoins{
    [SerializeField] SphereCollider mSphereCollider;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;

    public void CollectCoins()
    {
        Singleton.instance.Coins += 1;
        WriteVbles.sharedInstance.WritingNumberOfCoins();//Will write the number of coins in a text
        anim.SetBool("Get", true);
        DeactivatedCoin();//When you pick a coin it will deactivate its meshRenderer and box collider
        AudioController.sharedInstance.AudioCoins();
    }

    public void DeactivatedCoin()
    {
        mSphereCollider.enabled = false;
        StartCoroutine(FadeOut(spriteRenderer));
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
