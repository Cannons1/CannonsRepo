using UnityEngine;
using UnityEngine.UI;

public class DailyGifts : MonoBehaviour
{
    [SerializeField] WriteVbles mWriteVbles;
    [SerializeField] AudioController audioController;
    [SerializeField] TimeLeftToClaim timeLeftToClaim;
    public Button buttonDaily;

    int activeBtn;

    public delegate void Notifications();
    public event Notifications OnNotifyFalse;

    [SerializeField] Animator coinsLerpAnimator;

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
                buttonDaily.interactable = false;
            else
                buttonDaily.interactable = true;
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
                StartCoroutine(mWriteVbles.CountCoins(50));
                break;
            case 2:
                StartCoroutine(mWriteVbles.CountCoins(150));
                break;
            case 3:
                StartCoroutine(mWriteVbles.CountCoins(300));
                break;
            case 4:
                StartCoroutine(mWriteVbles.CountCoins(500));
                break;
            case 5:
                StartCoroutine(mWriteVbles.CountCoins(600));
                break;
            case 6:
                StartCoroutine(mWriteVbles.CountCoins(900));
                break;
            case 7:
                StartCoroutine(mWriteVbles.CountCoins(1500));
                SeventhDay();
                break;
        }

        timeLeftToClaim.CanWriteTime = true;
        coinsLerpAnimator.SetBool("Claimed", true);
        audioController.SoundClaimGift();
        DateTimeController.SaveDateTime();
        buttonDaily.interactable = false;
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