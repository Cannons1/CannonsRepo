using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;
using Assets.SimpleAndroidNotifications;
using System;

public class DailyGifts : MonoBehaviour
{
    [SerializeField] DateTimeController dateTime;
    public Button buttonDaily;

    int activeBtn;

    public int ActiveBtn {
        get { return activeBtn; }
        set { activeBtn = value; }
    }

    public delegate void Notifications();
    public event Notifications OnNotifyFalse;

    [SerializeField] Animator[] coinsLerpAnimator;

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
        if (PlayerPrefs.HasKey("ButtonDaily"))
        {
            ActiveBtn = PlayerPrefs.GetInt("ButtonDaily");
        }
        else {
            ActiveBtn = 1;
            PlayerPrefs.SetInt("ButtonDaily", activeBtn);
        }

        if (ActiveBtn == 0)
            buttonDaily.interactable = false;
        else
            buttonDaily.interactable = true;
    }

    public void DeleteKeysAfterTwoDays() {
        Singleton.instance.DailyGifts = 1;
        DateTimeController.SaveDailyCount();
        Debug.Log("Han pasado dos días");
    }

    public void GettingGift()
    {
        NotificationManager.Send(TimeSpan.FromHours(24), "Daily gift!", "Daily gift available", Color.white);
        GameAnalytics.NewDesignEvent("DailyGift");
        switch (Singleton.instance.DailyGifts) {
            case 1:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(50));
                break;
            case 2:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(150));
                break;
            case 3:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(300));
                break;
            case 4:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(500));
                break;
            case 5:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(600));
                break;
            case 6:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(900));
                break;
            case 7:
                StartCoroutine(WriteVbles.sharedInstance.CountCoins(1500));
                SeventhDay();
                break;
        }

        AudioController.sharedInstance.SoundClaimGift();
        DateTimeController.SaveDateTime();
        dateTime.GetTime();
        PlayerPrefs.SetInt("ButtonDaily", 0);
        OnNotifyFalse();
        buttonDaily.interactable = false;
        StartCoroutine(AnimCoin());
    }

    IEnumerator AnimCoin() {
        foreach (Animator a in coinsLerpAnimator)
        {
            a.SetBool("Amount", true);
            a.SetBool("Claimed", true);
        }
        yield return new WaitForSeconds(2f);
        foreach (Animator a in coinsLerpAnimator) {
            a.SetBool("Claimed", false);
            a.SetBool("Amount", false);
        }
    }

    void SeventhDay()
    {
        Debug.Log("Seventh Gift");
        Singleton.instance.DailyGifts = 0;
        DateTimeController.SaveDailyCount();
    }
}