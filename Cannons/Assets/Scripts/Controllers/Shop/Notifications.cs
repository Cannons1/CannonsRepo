using UnityEngine;

public class Notifications : MonoBehaviour {

    [SerializeField] GameObject notificationImg;
    DateTimeController dateTime;
    DailyGifts dailyGifts;

	void Start () {
        dateTime = GetComponent<DateTimeController>();
        dailyGifts = GetComponent<DailyGifts>();
        notificationImg.SetActive(false);
        dateTime.OnNotify += ActiveNotifications;
        dailyGifts.OnNotifyFalse += SetFalseNotifications;
	}

    private void ActiveNotifications() {
        notificationImg.SetActive(true);
    }

    private void SetFalseNotifications() {
        notificationImg.SetActive(false);
    }
}
