using UnityEngine;

public class Notifications : MonoBehaviour {

    [SerializeField] GameObject notificationImg;
    [SerializeField] DateTimeController dateTime;
    [SerializeField] DailyGifts dailyGifts;

	void Start () {
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
