using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Distance : MonoBehaviour
{
    [SerializeField] Slider sliderDistance;
    [SerializeField] Transform instanceT, winConditionT;
    [SerializeField] Text percentText;
    Transform willTransform;
    Will will;

    private float actualDistance;
    private float initialDistance;
    private float lastDistance;

    float percentOfLevel;

    private void Start()
    {
        will = (Will)FindObjectOfType(typeof(Will));
        initialDistance = Vector3.Distance(instanceT.localPosition, winConditionT.localPosition);
        sliderDistance.maxValue = initialDistance;
        lastDistance = sliderDistance.minValue;

        //Recibe evento de Will
        will.OnProgressLvl += CalulateDistance;
    }

    /// <summary>
    /// Event to update will's position
    /// </summary>
    private void CalulateDistance(Vector3 _Transform)
    {
        actualDistance = Vector3.Distance(instanceT.localPosition, _Transform);
        StartCoroutine(UpdateValue(actualDistance));
        percentOfLevel = (actualDistance*100)/initialDistance;
        percentText.text = percentOfLevel.ToString("0")+ " %";
    }

    IEnumerator UpdateValue(float _actualDistance)
    {
        float i = 0f;
        while (lastDistance < _actualDistance)
        {
            i += Time.deltaTime;
            lastDistance += i;
            sliderDistance.value = lastDistance;
            yield return null;
        }
        lastDistance = _actualDistance;
        sliderDistance.value = _actualDistance;
    }
}
