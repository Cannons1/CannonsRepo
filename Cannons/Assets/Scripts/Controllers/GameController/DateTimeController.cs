using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceDaily;
    DateTime dateClaimedGift;
    [SerializeField] DailyGifts dailyGifts;

    public delegate void Notifications();
    public event Notifications OnNotify;

    TimeSpan timeLeft;

    void Start ()
    {
        #region Daily Gift
        if (PlayerPrefs.HasKey("Daily"))
        {
            dateClaimedGift = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - dateClaimedGift;
            Timeleft();

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

    public TimeSpan Timeleft()
    {
        if (PlayerPrefs.HasKey("Daily")) {
            dateClaimedGift = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
        }
        timeLeft = dateClaimedGift.AddDays(1) - DateTime.Now;
        return timeLeft;
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
