using UnityEngine;
using UnityEngine.UI;

public class UnlockStars : MonoBehaviour
{
    [SerializeField] Image[] imgStarsLocked;

    public void StarsToUnlock(int _num) {
        for (int i = 0; i < imgStarsLocked.Length; i++) {
            if (i < _num)
                imgStarsLocked[i].enabled = false;
        }
    }
}