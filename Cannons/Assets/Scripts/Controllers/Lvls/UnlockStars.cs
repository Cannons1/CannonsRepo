using UnityEngine;
using UnityEngine.UI;

public class UnlockStars : MonoBehaviour {

    [SerializeField] Image[] imgStarsLocked;

    private void Start()
    {
        if (Stars.threeStars)
        {
            foreach (Image a in imgStarsLocked) {
                a.enabled = false;
            }
        }
        else if (Stars.twoStars) {
            for (int i = 0; i < 2; i++) {
                imgStarsLocked[i].enabled = false;
            }
        }
        else if (Stars.oneStar) {
           imgStarsLocked[0].enabled = false;
        }
    }
}
