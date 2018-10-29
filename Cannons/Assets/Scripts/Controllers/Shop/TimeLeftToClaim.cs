using UnityEngine;
using UnityEngine.UI;

public class TimeLeftToClaim : MonoBehaviour {

    [SerializeField] Text leftTimeTxt;
    [SerializeField] DateTimeController dateTimeController;
    [SerializeField] DailyGifts dailyGifts;
    [SerializeField] GameObject[] gameObjects;

    bool canWriteTime;
    public bool CanWriteTime
    {
        get
        {
            return canWriteTime;
        }

        set
        {
            canWriteTime = value;
        }
    }

    string dailyGift = "Claim daily gift";

    private void OnEnable()
    {
        if (!dailyGifts.buttonDaily.interactable && !dateTimeController.OneDay)
        {
            CanWriteTime = true;
        }
        else
            CanWriteTime = false;
    }

    private void OnDisable()
    {
        foreach (GameObject a in gameObjects) {
            a.SetActive(false);
        }
    }

    private void Update()
    {
        if (CanWriteTime)
        {
            leftTimeTxt.text = string.Format("Time left: {0}h {1}m {2}s", dateTimeController.Timeleft().Hours, dateTimeController.Timeleft().Minutes, dateTimeController.Timeleft().Seconds);
        }
        else {
            if (leftTimeTxt.text != dailyGift)
                leftTimeTxt.text = dailyGift;
        }
    }
}
