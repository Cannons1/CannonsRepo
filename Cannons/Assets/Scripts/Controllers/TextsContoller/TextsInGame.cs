using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextsInGame : MonoBehaviour {

    [SerializeField] Text motivationalText;
    [SerializeField] Animator animatorMotivational;

    [Header("Strings with motivational content")]
    [SerializeField] string[] motivations;
    [SerializeField] Distance distance;

    private float plusProb = 0f;

    private void Start()
    {
        distance.delTextInGame += ShowText;
    }

    private void ShowText(float _probabiltyToShow) {

        float prob = Random.Range(0f, 1f);
        plusProb += 0.03f;
        float currentProb = Mathf.Clamp01(_probabiltyToShow + plusProb);
        if (prob <= currentProb)
        {
            string stringToShow = motivations[Random.Range(0, motivations.Length)];
            motivationalText.text = stringToShow;
            animatorMotivational.SetBool("Show", true);
            StartCoroutine(SetFalse());
        }
    }

    IEnumerator SetFalse() {
        yield return null;
        animatorMotivational.SetBool("Show", false);
    }
}