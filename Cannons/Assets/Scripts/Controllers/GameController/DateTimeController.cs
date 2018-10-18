using UnityEngine;
using System;
using UnityEngine.UI;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceDaily;
    DateTime currentTimeDaily;
    [SerializeField] DailyGifts dailyGifts;

    public TimeSpan DifferenceDaily
    {
        get
        {
            return differenceDaily;
        }
    }

    public delegate void Notifications();
    public event Notifications OnNotify;

	void Start ()
    {
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (DifferenceDaily.Days >= 1 && DifferenceDaily.Days < 2)
            {
                dailyGifts.buttonDaily.GetComponent<Button>().interactable =true;
                SaveDateTime();
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
            if (DifferenceDaily.Days >= 2) {
                dailyGifts.buttonDaily.GetComponent<Button>().interactable = true;
                dailyGifts.DeleteKeysAfterTwoDays();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
        }
        #endregion
        if (dailyGifts.buttonDaily.GetComponent<Button>().interactable) {
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
