using UnityEngine;

public class Stars : MonoBehaviour {

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
        }
        else if (_currentTime < time2Stars) {
            if(Singleton.instance.Stars[mWinCondition.level -1] < 2)
                Singleton.instance.Stars[mWinCondition.level - 1] = 2;
            print("Two Stars");
        }
        else if (_currentTime > time2Stars) {
            if (Singleton.instance.Stars[mWinCondition.level - 1] < 1)
                Singleton.instance.Stars[mWinCondition.level - 1] = 1;
            print("One Star");
        }
    }
}