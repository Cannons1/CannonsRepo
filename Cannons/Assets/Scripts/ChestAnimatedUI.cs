using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChestAnimatedUI : MonoBehaviour {

    Image image;
    public Sprite[] sprites;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(SpriteAnim());
    }

    WaitForSeconds animSpeed = new WaitForSeconds(0.02f);

    public IEnumerator SpriteAnim()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            image.sprite = sprites[i];
            yield return animSpeed;
        }
        StartCoroutine(SpriteAnim());
    }
}
