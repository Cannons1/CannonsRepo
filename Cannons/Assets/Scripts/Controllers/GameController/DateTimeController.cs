using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeBoostExp, currentTimeDaily;
    DailyGifts dailyGifts;

	void Awake ()
    {
        dailyGifts = GetComponent<DailyGifts>();
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            Debug.Log("Tengo kayDaily");
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (PlayerPrefs.HasKey("DailyCount"))
            {
                Singleton.instance.DailyGifts = PlayerPrefs.GetInt("DailyCount");
            }

            if (differenceDaily.Days >= 1 )
            {
                Debug.Log("un día ha pasado");
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
