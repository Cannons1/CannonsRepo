using UnityEngine;

public class AnimPanels : MonoBehaviour
{
    [SerializeField] Animator panelsAnimator;

    public void AnimPanelShopOpen() {
        panelsAnimator.SetBool("OpenShop", true);
    }

    public void AnimPanelSettings(bool _condition) {
        panelsAnimator.SetBool("OpenSettings", _condition);
    }

    public void AnimPanelCredits(bool _condition) {
        panelsAnimator.SetBool("OpenCredits", _condition);
    }

    public void AnimPanelPause() {
        panelsAnimator.SetBool("OpenPause", true);
    }
}
