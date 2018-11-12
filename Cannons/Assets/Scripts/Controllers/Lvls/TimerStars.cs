using System.Collections;
using UnityEngine;

public class TimerStars : MonoBehaviour
{
    [SerializeField] Stars stars;
    [SerializeField] AudioController audioController;
    [SerializeField] WinCondition winCondition;
    [SerializeField] IGLevelManager iGLevelManager;
    [SerializeField] Animator starsAnimator;

    float time = 0f;
    bool canDesapearThird = true, canDesapearScnd = true;

    int currentStar = 0;

    private void Start()
    {
        iGLevelManager.delStars += VerifyActiveStars;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > stars.time3Stars - 3f && time < stars.time3Stars)
        {
            if (canDesapearThird)
            {
                StartCoroutine(DesapearStar(3, stars.time3Stars));
                canDesapearThird = false;
            }
        }

        if (time > stars.time2Stars - 3f && time < stars.time2Stars)
        {
            if (canDesapearScnd) {
                StartCoroutine(DesapearStar(2, stars.time2Stars));
                canDesapearScnd = false;
            }
        }
    }

    private IEnumerator VerifyActiveStars() {
        yield return null;
        switch (currentStar)
        {
            case 3:
                starsAnimator.SetInteger("Star", 3);
                break;
            case 2:
                starsAnimator.SetInteger("Star", 2);
                break;
            case -3:
                starsAnimator.SetInteger("Star",-3);
                break;
            case -2:
                starsAnimator.SetInteger("Star",-2);
                break;
            default:
                break;
        }
    }

    WaitForSeconds wait = new WaitForSeconds(0.5f);

    IEnumerator DesapearStar(int _star, float _timeStars)
    {
        if (!winCondition.WinBool)
        {
            currentStar = _star; 
            if(starsAnimator.gameObject.activeSelf)
                starsAnimator.SetInteger("Star", _star);
        }
        while (time < _timeStars && !winCondition.WinBool)
        {
            yield return wait;
            audioController.AudioStar(0.5f);
            yield return wait;
        }
        if (!winCondition.WinBool)
        {
            audioController.AudioStar(0.5f);
            if (_star == 3)
            {
                starsAnimator.SetInteger("Star", -3);
                currentStar = -3;
            }
            else if (_star == 2)
            {
                starsAnimator.SetInteger("Star", -2);
                currentStar = -2;
            }
        }   
        audioController.ItemAudioSource.volume = 1f;
    }
}
