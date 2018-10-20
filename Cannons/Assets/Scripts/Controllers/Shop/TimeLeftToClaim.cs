using UnityEngine;
using UnityEngine.UI;

public class TimeLeftToClaim : MonoBehaviour {

    [SerializeField] Text leftTimeTxt;
    [SerializeField] DateTimeController dateTimeController;

    bool canWriteTime;
    int activeBtn = 0;

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
        if (PlayerPrefs.HasKey("ButtonDaily"))
        {
            activeBtn = PlayerPrefs.GetInt("ButtonDaily");

            if (activeBtn == 0)
                CanWriteTime = true;
        }
    }

    private void OnDisable()
    {
        CanWriteTime = false;
    }

    private void Update()
    {
        if (CanWriteTime) {
            leftTimeTxt.text = "Time left " + dateTimeController.Timeleft().Hours.ToString("00h") + 
                dateTimeController.Timeleft().Minutes.ToString(":00m") + 
                dateTimeController.Timeleft().Seconds.ToString(":00s");
        }
    }
}
