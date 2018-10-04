using UnityEngine;
using UnityEngine.UI;

public class DailyGifts : MonoBehaviour
{
    WriteVbles mWriteVbles;
    [SerializeField] AudioUI mAudioUI;
    public GameObject buttonDaily;

    int activeBtn;

    public delegate void Notifications();
    public event Notifications OnNotifyFalse;

    private void Start()
    {
        mWriteVbles = GetComponent<WriteVbles>();

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
                buttonDaily.GetComponent<Button>().interactable = false;
            else
                buttonDaily.GetComponent<Button>().interactable = true;
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
                Singleton.instance.Coins += 10;
                break;
            case 2:
                Singleton.instance.Coins += 15;
                break;
            case 3:
                Singleton.instance.Coins += 20;
                break;
            case 4:
                Singleton.instance.Coins += 30;
                break;
            case 5:
                Singleton.instance.Coins += 50;
                break;
            case 6:
                Singleton.instance.Coins += 100;
                break;
            case 7:
                Singleton.instance.Coins += 500;
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
