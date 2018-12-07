using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class Stars : MonoBehaviour {

    [SerializeField] Image[] starsImg;
    [SerializeField] WinCondition mWinCondition;

    public float time3Stars, time2Stars;

    private float currentTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Will>() != null) {
            currentTime = Time.timeSinceLevelLoad;
            GiveStars(currentTime);
        }
    }

    public void GiveStars(float _currentTime) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, mWinCondition.level.ToString(), (int)_currentTime);//Sends a event to GameAnalytics with the time the user needed to complete the level
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
        float pitch = AudioController.sharedInstance.ItemAudioSource.pitch;
        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < starsImg.Length; i++) {
            if (i < _num) {
                starsImg[i].enabled = false;
                AudioController.sharedInstance.AudioStar();
                AudioController.sharedInstance.ItemAudioSource.pitch += 1;
            }
            yield return new WaitForSeconds(0.15f);
        }
        AudioController.sharedInstance.ItemAudioSource.pitch = pitch;
    }
}