using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerStars : MonoBehaviour
{
    [SerializeField] Stars stars;
    [SerializeField] Image[] starsImgs;
    [SerializeField] AudioItems audioItems;
    [SerializeField] WinCondition winCondition;

    float time = 0f;

    bool canDesapearThird = false, canDesapearScnd = false;

    private void Start()
    {
        Invoke("ActiveStars", 2.3f);
        winCondition.StopCoroutines += StopAllCoroutines;
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
                StartCoroutine(DesapearStar(starsImgs.Length -1, stars.time3Stars));
            canDesapearThird = true;
        }

        if (time > stars.time2Stars - 3f && time < stars.time2Stars)
        {
            if (!canDesapearScnd)
                StartCoroutine(DesapearStar(starsImgs.Length - 2, stars.time2Stars));
            canDesapearScnd = true;
        }
    }

    IEnumerator DesapearStar(int _star, float _timeStars)
    {
        audioItems.ItemsAudioSource.pitch = 1;
        while (time < _timeStars && !winCondition.WinBool)
        {
            starsImgs[_star].enabled = false;
            yield return new WaitForSeconds(0.5f);
            audioItems.AudioStar();
            starsImgs[_star].enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        if (!winCondition.WinBool) {
            audioItems.ItemsAudioSource.pitch = 2;
            audioItems.AudioStar();
        }
        starsImgs[_star].enabled = false;
    }
}
