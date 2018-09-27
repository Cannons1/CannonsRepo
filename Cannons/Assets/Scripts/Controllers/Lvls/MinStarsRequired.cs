using UnityEngine;
using UnityEngine.UI;

public class MinStarsRequired : MonoBehaviour {
    Text minStarsRequiredTxt;
    UnlockLevels pUnlockLevels;

    [SerializeField] int world;

    private void Start()
    {
        minStarsRequiredTxt = GetComponent<Text>();
        pUnlockLevels = GetComponentInParent<UnlockLevels>();

        TextMinStars(world);
    }

    private void TextMinStars(int _world) {
        switch (_world) {
            case 2:
                if (UnlockLevels.writeMinStarsWorldTwo)
                    minStarsRequiredTxt.text = " ";
                else
                    minStarsRequiredTxt.text = pUnlockLevels.MinStarsToWorldTwo.ToString() + " stars required";
                break;
            case 3:
                if (UnlockLevels.writeMinStarsWorldThree)
                    minStarsRequiredTxt.text = " ";
                else
                    minStarsRequiredTxt.text = pUnlockLevels.MinStarsToWorldThree.ToString() + " stars required";
                break;
            default:
                break;
        }
    }
}
