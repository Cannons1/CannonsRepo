using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    [SerializeField] private float force;
    public float Force { get { return force; } }
    public bool fadeOut;
    float fadeTime = 3.5f;


    public IEnumerator FadeOut()
    {
        Collider mCollider = GetComponent<Collider>();
        mCollider.enabled = false;
        SpriteRenderer mRenderer = GetComponent<SpriteRenderer>();
        float i = 1;
        yield return new WaitForSeconds(0.6f);
        while(mRenderer.color.a > 0)
        {
            i -= Time.deltaTime * fadeTime;
            mRenderer.color = new Color(1, 1, 1, i);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
