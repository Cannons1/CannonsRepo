using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerStars : MonoBehaviour
{
    [SerializeField] Stars stars;
    [SerializeField] AudioController audioController;
    [SerializeField] WinCondition winCondition;
    [SerializeField] Animator starsAnimator;

    float time = 0f;
    bool canDesapearThird = true, canDesapearScnd = true;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > stars.time3Stars - 3f && time < stars.time3Stars)
        {
            if (canDesapearThird)
                StartCoroutine(DesapearStar(3, stars.time3Stars));
            canDesapearThird = false;
        }

        if (time > stars.time2Stars - 3f && time < stars.time2Stars)
        {
            if (canDesapearScnd)
                StartCoroutine(DesapearStar(2, stars.time2Stars));
            canDesapearScnd = false;
        }
    }

    WaitForSeconds wait = new WaitForSeconds(0.5f);

    IEnumerator DesapearStar(int _star, float _timeStars)
    {
        starsAnimator.SetInteger("Star", _star);
        while (time < _timeStars && !winCondition.WinBool)
        {
            yield return wait;
            audioController.AudioStar(0.5f);
            yield return wait;
        }
        starsAnimator.SetInteger("Star", 0);
        if (!winCondition.WinBool) {
            audioController.AudioStar(0.5f);
        }
        audioController.ItemAudioSource.volume = 1f;
    }
}
