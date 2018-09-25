using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeBoostExp, currentTimeDaily;
    DailyGifts dailyGifts;

	void Start ()
    {
        dailyGifts = GetComponent<DailyGifts>();
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (differenceDaily.Days >= 1 )
            {
                SaveDateTime();
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                Debug.Log(Singleton.instance.DailyGifts);
            }
            if (differenceDaily.Days >= 2) {
                dailyGifts.DeleteKeysAfterTwoDays();
            }
        }
        #endregion
    }

    public static void SaveDateTime()
    {
        PlayerPrefs.SetString("Daily", DateTime.Now.ToString());
        Debug.Log("Day starts now");
    }

    public static void SaveDailyCount() {
        PlayerPrefs.SetInt("DailyCount", Singleton.instance.DailyGifts);
    }

    public static void SaveActiveToggles() {
        PlayerPrefs.SetInt("TogglesActive", Singleton.instance.ActiveToggles);
    }
}
