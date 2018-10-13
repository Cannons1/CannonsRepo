using UnityEngine;

public class PanelShop : MonoBehaviour {

    [SerializeField] Animator panelShopAnimator;

    public void AnimPanelShopOpen() {
        panelShopAnimator.SetBool("OpenShop", true);
    }

    public void AnimPanelShopClose() {
        panelShopAnimator.SetBool("OpenShop", false);
    }
}
