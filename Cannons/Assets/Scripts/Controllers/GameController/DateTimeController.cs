using UnityEngine;
using System;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceDaily;
    DateTime dateClaimedGift;
    [SerializeField] DailyGifts dailyGifts;
    [SerializeField] TimeLeftToClaim timeLeftToClaim;

    public delegate void Notifications();
    public event Notifications OnNotify;

    TimeSpan timeLeft;

    bool oneDay;

    public bool OneDay {
        get { return oneDay; }
    }

    void Start ()
    {
        oneDay = false;
        #region Daily Gift
        if (PlayerPrefs.HasKey("Daily"))
        {
            dateClaimedGift = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - dateClaimedGift;

            if (differenceDaily.Days >= 1 && differenceDaily.Days < 2)
            {
                oneDay = true;
                dailyGifts.buttonDaily.interactable = true;
                SaveDateTime();
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
            if (differenceDaily.Days >= 2) {
                oneDay = true;
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

    public void GetTime() {

        if (PlayerPrefs.HasKey("Daily"))
        {
            dateClaimedGift = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            timeLeftToClaim.CanWriteTime = true;
        }
    }

    public TimeSpan Timeleft()
    {
        timeLeft = dateClaimedGift.AddDays(1) - DateTime.Now;
        if (timeLeft.TotalSeconds < 0 && !oneDay)
        {
            dailyGifts.buttonDaily.interactable = true;
            timeLeftToClaim.CanWriteTime = false;
            Singleton.instance.DailyGifts++;
            SaveDailyCount();
            print(Singleton.instance.DailyGifts);
            PlayerPrefs.SetInt("ButtonDaily", 1);
            oneDay = true;
        }
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
