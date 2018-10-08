using UnityEngine;
using UnityEngine.UI;

public class DailyGifts : MonoBehaviour
{
    [SerializeField] WriteVbles mWriteVbles;
    [SerializeField] AudioUI mAudioUI;
    public GameObject buttonDaily;
    public GameObject[] textAvailable;

    int activeBtn;

    public delegate void Notifications();
    public event Notifications OnNotifyFalse;

    private void Start()
    {
        if (PlayerPrefs.HasKey("DailyCount"))
        {
            Singleton.instance.DailyGifts = PlayerPrefs.GetInt("DailyCount");
        }
        else {
            Singleton.instance.DailyGifts = 1;
            DateTimeController.SaveDailyCount();
        }
        if (PlayerPrefs.HasKey("ButtonDaily")) {
            activeBtn = PlayerPrefs.GetInt("ButtonDaily");

            if (activeBtn == 0)
            {
                buttonDaily.GetComponent<Button>().interactable = false;
                textAvailable[0].SetActive(false);
                textAvailable[1].SetActive(true);
            }
            else {
                buttonDaily.GetComponent<Button>().interactable = true;
                textAvailable[0].SetActive(true);
                textAvailable[1].SetActive(false);
            }
        }
    }

    public void DeleteKeysAfterTwoDays() {
        Singleton.instance.DailyGifts = 1;
        DateTimeController.SaveDailyCount();
        Debug.Log("Han pasado dos días");
    }

    public void GettingGift()
    {
        switch (Singleton.instance.DailyGifts) {
            case 1:
                Singleton.instance.Coins += 50;
                break;
            case 2:
                Singleton.instance.Coins += 150;
                break;
            case 3:
                Singleton.instance.Coins += 300;
                break;
            case 4:
                Singleton.instance.Coins += 500;
                break;
            case 5:
                Singleton.instance.Coins += 600;
                break;
            case 6:
                Singleton.instance.Coins += 900;
                break;
            case 7:
                Singleton.instance.Coins += 1500;
                SeventhDay();
                break;
        }
        mAudioUI.SoundClaimGift();
        DateTimeController.SaveDateTime();
        Singleton.SaveCoins();
        mWriteVbles.WriteOnPurchase();
        buttonDaily.GetComponent<Button>().interactable = false;
        PlayerPrefs.SetInt("ButtonDaily", 0);
        OnNotifyFalse();
    }

    void SeventhDay()
    {
        Debug.Log("Seventh Gift");
        Singleton.instance.DailyGifts = 0;
        DateTimeController.SaveDailyCount();
    }
}
