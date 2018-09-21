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
        for (int i = 0; i < 2; i++)
        {
            imgStarsLocked[i].enabled = false;
        }
    }

    public void OneStar() {
        imgStarsLocked[0].enabled = false;
    }
}