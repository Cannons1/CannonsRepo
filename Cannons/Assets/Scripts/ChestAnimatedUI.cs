using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChestAnimatedUI : MonoBehaviour {

    [SerializeField] Image image;
    public Sprite[] sprites;

    private void Start()
    {
        StartCoroutine(SpriteAnim());
        IGLevelManager.uIChestAnimated += SpriteAnim;
    }

    WaitForSeconds animSpeed = new WaitForSeconds(0.02f);

    public IEnumerator SpriteAnim()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            image.sprite = sprites[i];
            yield return animSpeed;
        }
        if(image.isActiveAndEnabled)
            StartCoroutine(SpriteAnim());
    }
}
