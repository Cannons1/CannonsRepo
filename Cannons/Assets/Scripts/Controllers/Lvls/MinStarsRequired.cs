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
                if (UnlockLevels.writeStars[0])
                    minStarsRequiredTxt.gameObject.SetActive(false);
                else
                    minStarsRequiredTxt.text = string.Format("{0} stars required",pUnlockLevels.MinStars[0]);
                break;
            case 3:
                if (UnlockLevels.writeStars[1])
                    minStarsRequiredTxt.gameObject.SetActive(false);
                else
                    minStarsRequiredTxt.text = string.Format("{0} stars required", pUnlockLevels.MinStars[1]);
                break;
            case 4:
                if (UnlockLevels.writeStars[2]) minStarsRequiredTxt.gameObject.SetActive(false);
                else minStarsRequiredTxt.text = string.Format("{0} stars required", pUnlockLevels.MinStars[2]);
                break;
            default:
                break;
        }
    }
}
