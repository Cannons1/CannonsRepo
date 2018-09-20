using UnityEngine;

public class Stars : MonoBehaviour {

    [SerializeField] float time3Stars, time2Stars, time1Star;
    private float currentTime;

    public static bool threeStars, twoStars, oneStar;

    private void Start()
    {
        threeStars = false;
        twoStars = false;
        oneStar = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentTime = Time.timeSinceLevelLoad;
        GiveStars(currentTime);
    }

    public void GiveStars(float _currentTime) {
        if (_currentTime < time3Stars) {
            threeStars = true;
            print("Three Stars");
        }
        else if (_currentTime < time2Stars) {
            twoStars = true;
            print("Two Stars");
        }
        else if (_currentTime > time2Stars) {
            oneStar = true;
            print("One Star");
        }
    }
}
