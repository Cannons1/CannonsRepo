using UnityEngine;
using UnityEngine.UI;

public class MinStarsRequired : MonoBehaviour {
    [SerializeField] Text minStarsRequiredTxt;
    UnlockLevels pUnlockLevels;

    [SerializeField] int world;

    private void Start()
    {
        pUnlockLevels = GetComponentInParent<UnlockLevels>();
        TextMinStars(world);
    }

    private void TextMinStars(int _world) {
        switch (_world) {
            case 2:
                if (UnlockLevels.writeMinStarsWorldTwo)
                    minStarsRequiredTxt.gameObject.SetActive(false);
                else
                    minStarsRequiredTxt.text = pUnlockLevels.MinStarsToWorldTwo.ToString() + " stars required";
                break;
            case 3:
                if (UnlockLevels.writeMinStarsWorldThree)
                    minStarsRequiredTxt.gameObject.SetActive(false);
                else
                    minStarsRequiredTxt.text = pUnlockLevels.MinStarsToWorldThree.ToString() + " stars required";
                break;
            default:
                break;
        }
    }
}
