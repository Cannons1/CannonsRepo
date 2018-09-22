using UnityEngine;
using UnityEngine.UI;

public class UnlockStars : MonoBehaviour
{
    [SerializeField] Image[] imgStarsLocked;

    public void ThreeStars()
    {
        foreach (Image a in imgStarsLocked)
        {
            a.enabled = false;
        }
    }

    public void TwoStars() {
        imgStarsLocked[0].enabled = false;
        imgStarsLocked[1].enabled = false;
    }

    public void OneStar() {
        imgStarsLocked[0].enabled = false;
    }
}