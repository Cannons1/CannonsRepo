using UnityEngine;
using System;
using UnityEngine.UI;

public class DateTimeController : MonoBehaviour
{
    TimeSpan differenceExp, differenceDaily;
    DateTime currentTimeDaily;
    [SerializeField] DailyGifts dailyGifts;

    public delegate void Notifications();
    public event Notifications OnNotify;

	void Start ()
    {
        #region dailyChallenge
        if (PlayerPrefs.HasKey("Daily"))
        {
            currentTimeDaily = Convert.ToDateTime(PlayerPrefs.GetString("Daily"));
            differenceDaily = DateTime.Now - currentTimeDaily;

            if (differenceDaily.Days >= 1 && differenceDaily.Days < 2)
            {
                dailyGifts.buttonDaily.GetComponent<Button>().interactable =true;
                dailyGifts.textAvailable[0].SetActive(true);
                dailyGifts.textAvailable[1].SetActive(false);
                SaveDateTime();
                Singleton.instance.DailyGifts++;
                SaveDailyCount();
                PlayerPrefs.SetInt("ButtonDaily", 1);
            }
            if (differenceDaily.Days >= 2) {
                dailyGifts.buttonDaily.GetComponent<Button>().interactable = true;
                dailyGifts.DeleteKeysAfterTwoDays();
                PlayerPrefs.SetInt("ButtonDaily", 1);
                dailyGifts.textAvailable[0].SetActive(true);
                dailyGifts.textAvailable[1].SetActive(false);
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
