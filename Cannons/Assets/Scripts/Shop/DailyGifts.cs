using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyGifts : MonoBehaviour
{
    [SerializeField] Toggle[] dailyToggles = new Toggle[7];
    [SerializeField] WriteVbles mWriteVbles;
    [SerializeField] AudioUI mAudioUI;

    private void Start()
    {
        foreach (Toggle a in dailyToggles)
        {
            a.interactable = false;
        }

        switch (Singleton.instance.ActiveToggles)
        {
            case 1:
                dailyToggles[0].enabled = false;
                break;
            case 2:
                dailyToggles[1].enabled = false;
                break;
            case 3:
                dailyToggles[2].enabled = false;
                break;
            case 4:
                dailyToggles[3].enabled = false;
                break;
            case 5:
                dailyToggles[4].enabled = false;
                break;
            case 6:
                dailyToggles[5].enabled = false;
                break;
            case 7:
                dailyToggles[6].enabled = false;
                Singleton.instance.ActiveToggles = 0;
                break;
            default:
                break;
        }
        DateTimeController.OnDailyGifts += UnlockingDailyGifts;
        UnlockingDailyGifts();
    }

    public void UnlockingDailyGifts()
    {
        switch (Singleton.instance.DailyGifts)
        {
            case 0:
                dailyToggles[0].interactable = true;
                break;
            case 1:
                dailyToggles[1].interactable = true;
                break;
            case 2:
                dailyToggles[2].interactable = true;
                break;
            case 3:
                dailyToggles[3].interactable = true;
                break;
            case 4:
                dailyToggles[4].interactable = true;
                break;
            case 5:
                dailyToggles[5].interactable = true;
                break;
            case 6:
                dailyToggles[6].interactable = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// This is an Onclick event for daily toggles
    /// </summary>
    public void Interacting()
    {

        switch (Singleton.instance.DailyGifts)
        {
            case 0:
                GiftFirstToggle();
                break;
            case 1:
                GiftSecondToggle();
                break;
            case 2:
                GiftThirdToggle();
                break;
            case 3:
                GiftFourthToggle();
                break;
            case 4:
                GiftFifthToggle();
                break;
            case 5:
                GiftSixthToggle();
                break;
            case 6:
                GiftSeventhToggle();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// This function is present in all toggles gifts
    /// </summary>
    public void GettingGift()
    {
        mAudioUI.SoundClaimGift();
        Singleton.instance.ActiveToggles++;
        DateTimeController.SaveDateTime();
        dailyToggles[Singleton.instance.DailyGifts].interactable = false;
    }

    #region Gifts of Toggles
    void GiftFirstToggle()
    {
        GettingGift();
        Singleton.instance.Coins += 5;
        mWriteVbles.WritingNumberOfCoins();
        Debug.Log("First Gift");
    }

    void GiftSecondToggle()
    {
        GettingGift();
        Debug.Log("Second Gift");
    }

    void GiftThirdToggle()
    {
        GettingGift();
        Debug.Log("Third Gift");
    }

    void GiftFourthToggle()
    {
        GettingGift();
        Debug.Log("Fourth Gift");
    }

    void GiftFifthToggle()
    {
        GettingGift();
        Debug.Log("Fifth Gift");
    }

    void GiftSixthToggle()
    {
        GettingGift();
        Debug.Log("Sixth Gift");
    }

    void GiftSeventhToggle()
    {
        GettingGift();
        Debug.Log("Seventh Gift");
    }
    #endregion
}
