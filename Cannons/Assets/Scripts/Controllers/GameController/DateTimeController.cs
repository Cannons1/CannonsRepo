using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeBoostExp, currentTimeDaily;
    ExpBoost mExpBoost;
    DailyGifts dailyGifts;

	void Awake ()
    {
        mExpBoost = GetComponent<ExpBoost>();
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

        #region BoostExp
        if (PlayerPrefs.HasKey("ExpBoostTime"))
        {
            currentTimeBoostExp = Convert.ToDateTime(PlayerPrefs.GetString("ExpBoostTime"));
            differenceExp = DateTime.Now - currentTimeBoostExp;

            if (differenceExp.Hours >= 2)
            {
                mExpBoost.ExpBoostReady();
            }
        }
        #endregion
    }

    public static void SaveDateTime()
    {
        PlayerPrefs.SetString("Daily", DateTime.Now.ToString());
        Debug.Log("Day starts now");
    }

    public static void SaveExpBoostTime()
    {
        PlayerPrefs.SetString("ExpBoostTime", DateTime.Now.ToString());
    }

    public static void SaveDailyCount() {
        PlayerPrefs.SetInt("DailyCount", Singleton.instance.DailyGifts);
    }

    public static void SaveActiveToggles() {
        PlayerPrefs.SetInt("TogglesActive", Singleton.instance.ActiveToggles);
    }
}
