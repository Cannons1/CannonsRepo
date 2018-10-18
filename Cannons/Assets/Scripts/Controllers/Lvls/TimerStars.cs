using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerStars : MonoBehaviour
{
    [SerializeField] Stars stars;
    [SerializeField] Image[] starsImgs;
    [SerializeField] AudioController audioController;
    [SerializeField] WinCondition winCondition;

    float time = 0f;

    bool canDesapearThird = false, canDesapearScnd = false;

    private void Start()
    {
        StartCoroutine(ActiveStars());
    }

    IEnumerator ActiveStars() {
        yield return new WaitForSeconds(2.3f);
        foreach (Image star in starsImgs)
        {
            star.enabled = true;
            yield return null;
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
        //audioController.ItemsAudioSource.pitch = 1;
        while (time < _timeStars && !winCondition.WinBool)
        {
            starsImgs[_star].enabled = false;
            yield return new WaitForSeconds(0.5f);
            audioController.AudioStar();
            starsImgs[_star].enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        if (!winCondition.WinBool) {
            audioController.AudioStar();
        }
        starsImgs[_star].enabled = false;
    }
}
