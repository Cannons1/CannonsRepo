using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DailyGifts : MonoBehaviour
{
    [SerializeField] WriteVbles mWriteVbles;
    [SerializeField] AudioController audioController;
    public Button buttonDaily;

    int activeBtn;

    public delegate void Notifications();
    public event Notifications OnNotifyFalse;

    //This is for animation when a gift is claimed
    //[SerializeField] RectTransform[] points;
    //[SerializeField] RectTransform[] coinsToLerp;


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
        //StartCoroutine(LerpCoins());
    }

    /*IEnumerator LerpCoins() {
        float time = 2f;
        float t = 0f;
        int n = 0;

        while (n <= coinsToLerp.Length) {
            t += Time.deltaTime;
            foreach (RectTransform a in coinsToLerp) {
                a.position = Vector2.Lerp(points[0].position, points[1].position, t/time);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }*/


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