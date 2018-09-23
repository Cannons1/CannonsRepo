using UnityEngine;
using UnityEngine.UI;

public class MinStarsRequired : MonoBehaviour {
    Text minStarsRequiredTxt;
    UnlockLevels pUnlockLevels;

    private void Start()
    {
        minStarsRequiredTxt = GetComponent<Text>();
        pUnlockLevels = GetComponentInParent<UnlockLevels>();

        if (UnlockLevels.secondWorld)
            minStarsRequiredTxt.text = " ";
        else 
            minStarsRequiredTxt.text = pUnlockLevels.MinStarsWorldOne.ToString() + " stars required";
    }
}
