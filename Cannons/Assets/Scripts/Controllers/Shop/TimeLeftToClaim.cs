using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeLeftToClaim : MonoBehaviour {

    [SerializeField] Text textClaimDaily;
    [SerializeField] DateTimeController dateTimeController;

    bool canWriteTime;
    TimeSpan timeSpan;

    private void OnEnable()
    {
        canWriteTime = true;
    }

    private void OnDisable()
    {
        canWriteTime = false;
    }

    private void Update()
    {
        if (canWriteTime) {
            textClaimDaily.text = dateTimeController.Resta.ToString();
        }
    }
}
