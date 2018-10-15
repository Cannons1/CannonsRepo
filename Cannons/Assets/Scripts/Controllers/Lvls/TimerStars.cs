using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStars : MonoBehaviour
{
    [SerializeField] Stars stars;
    [SerializeField] Image[] starsImgs;

    float time = 0f;

    bool canDesapearThird = false, canDesapearScnd = false;

    private void Start()
    {
        Invoke("ActiveStars", 2.3f);
    }

    private void ActiveStars()
    {
        foreach (Image star in starsImgs)
        {
            star.enabled = true;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > stars.time3Stars - 3f && time < stars.time3Stars)
        {
            if (!canDesapearThird)
                StartCoroutine(ThirdStar());
            canDesapearThird = true;
        }

        if (time > stars.time2Stars - 3f && time < stars.time2Stars)
        {
            if (!canDesapearScnd)
                StartCoroutine(SecondStar());
            canDesapearScnd = true;
        }
    }

    IEnumerator ThirdStar()
    {
        while (time < stars.time3Stars)
        {
            starsImgs[starsImgs.Length - 1].enabled = false;
            yield return new WaitForSeconds(0.5f);
            starsImgs[starsImgs.Length - 1].enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        starsImgs[starsImgs.Length - 1].enabled = false;
    }

    IEnumerator SecondStar()
    {
        while (time < stars.time2Stars)
        {
            starsImgs[starsImgs.Length - 2].enabled = false;
            yield return new WaitForSeconds(0.5f);
            starsImgs[starsImgs.Length - 2].enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        starsImgs[starsImgs.Length - 2].enabled = false;
    }
}
