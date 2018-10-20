using UnityEngine;
using UnityEngine.UI;

public class TimeLeftToClaim : MonoBehaviour {

    [SerializeField] Text leftTimeTxt;
    [SerializeField] DateTimeController dateTimeController;
    [SerializeField] DailyGifts dailyGifts;

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
        if (dailyGifts.ActiveBtn == 0 && !dateTimeController.OneDay)
            CanWriteTime = true;
    }

    private void OnDisable()
    {
        CanWriteTime = false;
    }

    private void Update()
    {
        if (CanWriteTime)
        {
            leftTimeTxt.text = "Time left " + dateTimeController.Timeleft().Hours.ToString("00h") +
                dateTimeController.Timeleft().Minutes.ToString(":00m") +
                dateTimeController.Timeleft().Seconds.ToString(":00s");
        }
        else {
            leftTimeTxt.text = "Claim daily gift";
        }
    }
}
