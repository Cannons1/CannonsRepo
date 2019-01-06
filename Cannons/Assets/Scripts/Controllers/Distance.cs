using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Distance : MonoBehaviour
{
    [SerializeField] Slider sliderDistance;
    [SerializeField] Transform instanceT = null, winConditionT = null;
    [SerializeField] Text percentText = null;
    Transform willTransform;
    Will will;

    private float actualDistance;
    private float initialDistance;
    private float lastDistance;

    float percentOfLevel;

    public delegate void TextInGame(float _probabilityToShow);
    public TextInGame delTextInGame;

    public delegate IEnumerator Seagull();
    public Seagull delSeagull;
    private bool isActiveSeagulls = false;

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
        percentText.text = percentOfLevel.ToString("0")+ "%";

        if (delSeagull != null)
        {
            if (percentOfLevel > 50f && !isActiveSeagulls)
            {
                StartCoroutine(delSeagull());
                isActiveSeagulls = true;
            } 
        }

        delTextInGame(0.3f);
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
