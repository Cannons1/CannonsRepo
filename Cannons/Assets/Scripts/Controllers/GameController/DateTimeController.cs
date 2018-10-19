﻿using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceDaily;
    DateTime currentTimeDaily;
    [SerializeField] DailyGifts dailyGifts;

    public delegate void Notifications();
    public event Notifications OnNotify;

    TimeSpan resta;

    public TimeSpan Resta
    {
        get
        {
            return resta;
        }
    }

    void Start ()
    {
        #region Daily Gift
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
           // resta = (currentTimeDaily + currentTimeDaily.AddDays(1)) - DateTime.Now;
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (differenceDaily.Days >= 1 && differenceDaily.Days < 2)
            {
                dailyGifts.buttonDaily.interactable =true;
                SaveDateTime();
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
            if (differenceDaily.Days >= 2) {
                dailyGifts.buttonDaily.interactable = true;
                dailyGifts.DeleteKeysAfterTwoDays();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
        }
        #endregion
        if (dailyGifts.buttonDaily.interactable) {
            OnNotify();
        }
    }

    public static void SaveDateTime()
    {
        PlayerPrefs.SetString("Daily", DateTime.Now.ToString());
        Debug.Log("Day starts now");
    }

    public static void SaveDailyCount() {
        PlayerPrefs.SetInt("DailyCount", Singleton.instance.DailyGifts);
    }
}
