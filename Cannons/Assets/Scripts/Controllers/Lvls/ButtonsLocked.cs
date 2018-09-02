using UnityEngine;
using UnityEngine.UI;

public class ButtonsLocked : MonoBehaviour {

    Button mButton;
    Image[] cImg;

	void Start () {
        mButton = GetComponent<Button>();
        cImg = GetComponentsInChildren<Image>();
	}

    public void Locked() {
        cImg[1].enabled = true;
        mButton.enabled = false;
    }

    public void Unlocked() {
        cImg[1].enabled = false;
        mButton.enabled = true;
    }
}
