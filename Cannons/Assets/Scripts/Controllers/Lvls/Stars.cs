using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stars : MonoBehaviour {

    [SerializeField] Image[] starsImg;

    [SerializeField] float time3Stars, time2Stars;
    private float currentTime;

    WinCondition mWinCondition;

    private void Start()
    {
        mWinCondition = GetComponent<WinCondition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Will>() != null) {
            currentTime = Time.timeSinceLevelLoad;
            GiveStars(currentTime);
        }
    }

    public void GiveStars(float _currentTime) {
        if (_currentTime < time3Stars) {
            Singleton.instance.Stars[mWinCondition.level - 1] = 3;
            print("Three Stars");
            StartCoroutine(UnlockingStars(3));
        }
        else if (_currentTime < time2Stars) {
            if(Singleton.instance.Stars[mWinCondition.level -1] < 2)
                Singleton.instance.Stars[mWinCondition.level - 1] = 2;
            print("Two Stars");
            StartCoroutine(UnlockingStars(2));
        }
        else if (_currentTime > time2Stars) {
            if (Singleton.instance.Stars[mWinCondition.level - 1] < 1)
                Singleton.instance.Stars[mWinCondition.level - 1] = 1;
            print("One Star");
            StartCoroutine(UnlockingStars(1));
        }
        PlayerPrefsX.SetIntArray("Stars", Singleton.instance.Stars);
    }

    IEnumerator UnlockingStars(int _num) {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < starsImg.Length; i++) {
            if (i < _num)
                starsImg[i].enabled = false;
            yield return new WaitForSeconds(1.4f);
        }
    }
}