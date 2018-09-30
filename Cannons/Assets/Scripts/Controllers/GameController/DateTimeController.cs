using UnityEngine;
using System;
using UnityEngine.UI;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeDaily;
    DailyGifts dailyGifts;

	void Start ()
    {
        dailyGifts = GetComponent<DailyGifts>();
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (differenceDaily.Days >= 1 && differenceDaily.Days < 2)
            {
                dailyGifts.buttonDaily.GetComponent<Button>().interactable =true;
                SaveDateTime();
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
            if (differenceDaily.Days >= 2) {
                dailyGifts.buttonDaily.GetComponent<Button>().interactable = true;
                dailyGifts.DeleteKeysAfterTwoDays();
                PlayerPrefs.SetInt("ButtonDaily", 1);
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


}
