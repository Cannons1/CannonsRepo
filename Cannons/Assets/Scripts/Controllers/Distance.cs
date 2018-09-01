using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Distance : MonoBehaviour
{

    [SerializeField] Slider sliderDistance;
    [SerializeField] Transform instanceT, winConditionT;
    Transform willTransform;
    Will will;

    private float actualDistance;
    private float initialDistance;
    private float lastDistance;

    private void Start()
    {
        will = (Will)FindObjectOfType(typeof(Will));
        //Recibe evento de Will
        initialDistance = Vector3.Distance(instanceT.localPosition, winConditionT.localPosition);
        sliderDistance.maxValue = initialDistance;
        lastDistance = sliderDistance.minValue;

        will.OnProgressLvl += CalulateDistance;
    }

    /// <summary>
    /// Event to update will's position
    /// </summary>
    private void CalulateDistance(Vector3 _Transform)
    {
        actualDistance = Vector3.Distance(instanceT.localPosition, _Transform);
        StartCoroutine(UpdateValue(actualDistance));
    }

    IEnumerator UpdateValue(float _actualDistance)
    {
        float i = 0;
        while (lastDistance < _actualDistance)
        {
            i += Time.time;
            lastDistance += i / _actualDistance;
            sliderDistance.value = lastDistance;
            yield return null;
        }
        lastDistance = _actualDistance;
        sliderDistance.value = _actualDistance;
    }
}
