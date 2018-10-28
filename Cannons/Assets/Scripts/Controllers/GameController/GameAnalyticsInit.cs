using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInit : MonoBehaviour {

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public void EnterToShop() {
        GameAnalytics.NewDesignEvent("InShop");
    }
}
