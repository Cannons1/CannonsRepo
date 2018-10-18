using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stars : MonoBehaviour {

    [SerializeField] Image[] starsImg;
    private float currentTime;
    [SerializeField] WinCondition mWinCondition;
    [SerializeField] AudioController audioController;

    public float time3Stars, time2Stars;

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
            mWinCondition.CoinsMgr(Random.Range(20, 31));
            StartCoroutine(UnlockingStars(3));
        }
        else if (_currentTime < time2Stars) {
            if(Singleton.instance.Stars[mWinCondition.level -1] < 2)
                Singleton.instance.Stars[mWinCondition.level - 1] = 2;
            print("Two Stars");
            mWinCondition.CoinsMgr(Random.Range(10, 21));
            StartCoroutine(UnlockingStars(2));
        }
        else if (_currentTime > time2Stars) {
            if (Singleton.instance.Stars[mWinCondition.level - 1] < 1)
                Singleton.instance.Stars[mWinCondition.level - 1] = 1;
            print("One Star");
            mWinCondition.CoinsMgr(Random.Range(1, 11));
            StartCoroutine(UnlockingStars(1));
        }
        PlayerPrefsX.SetIntArray("Stars", Singleton.instance.Stars);
    }

    IEnumerator UnlockingStars(int _num) {
        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < starsImg.Length; i++) {
            if (i < _num) {
                starsImg[i].enabled = false;
                audioController.AudioStar();
                //audioController.ItemsAudioSource.pitch += 1;
            }
            yield return new WaitForSeconds(0.15f);
        }
        //audioController.ItemsAudioSource.pitch = 1;
    }
}