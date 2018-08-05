using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeBoostExp, currentTimeDaily;

    public delegate void DateTimeDelegate();
    public static event DateTimeDelegate OnExpBoostReady, OnDailyGifts;

	void Start ()
    {
        //currentTime = DateTime.Now;
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (differenceDaily.Days >= 1)
            {
                Debug.Log("one day has passed");
                Singleton.instance.dailyGifts++;
                OnDailyGifts();
            }
        }
        else
        {
            Debug.Log("Can't load DateTime");
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
                else {
                    Debug.Log("IDK");
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
        Debug.Log("DateTime saved");
    }
}
