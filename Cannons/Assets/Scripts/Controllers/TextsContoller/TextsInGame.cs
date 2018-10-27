using UnityEngine;
using UnityEngine.UI;

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
        plusProb += 0.04f;
        print(string.Format("{0} prob, {1} probToShow", prob, _probabiltyToShow + plusProb));
        if (prob <= _probabiltyToShow + plusProb)
        {
            string stingToShow = motivations[Random.Range(0, motivations.Length)];
            motivationalText.text = stingToShow;
            animatorMotivational.SetBool("Show", true);
            Invoke("SetFalse", 0.5f);
        }
    }

    private void SetFalse() {
        animatorMotivational.SetBool("Show", false);
    }
}
