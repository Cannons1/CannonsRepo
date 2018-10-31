using UnityEngine;
using UnityEngine.UI;

public class TimeLeftToClaim : MonoBehaviour {

    [SerializeField] Text leftTimeTxt;
    [SerializeField] Image gift;
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

    private void OnEnable()
    {
        dailyGifts.OnNotifyFalse += SetActiveFalse;
        dateTimeController.OnNotify += SetActive;
        if (!dailyGifts.buttonDaily.interactable && !dateTimeController.OneDay)
        {
            gift.enabled = false;
            CanWriteTime = true;
        }
        else {
            CanWriteTime = false;
            gift.enabled = true;
        }
    }

    private void SetActive() {
        gift.enabled = true;
    }

    private void SetActiveFalse() {
        gift.enabled = false;
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
            if(leftTimeTxt.text != " ")
                leftTimeTxt.text = " ";
        }
    }
}
