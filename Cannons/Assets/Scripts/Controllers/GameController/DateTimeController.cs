using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeBoostExp, currentTimeDaily;

    public delegate void DateTimeDelegate();
    public static event DateTimeDelegate OnExpBoostReady;

	void Awake ()
    {
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (differenceDaily.Days >= 1)
            {
                Debug.Log("one day has passed");
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                Debug.Log(Singleton.instance.DailyGifts);
                SaveDateTime();
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
                Debug.Log("The boost for Exp is over");
                if (OnExpBoostReady != null)
                {
                    OnExpBoostReady();
                }
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
