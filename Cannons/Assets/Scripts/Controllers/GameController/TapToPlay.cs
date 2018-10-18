using UnityEngine;

public class TapToPlay : MonoBehaviour
{
    [SerializeField] CanvasMenu mcanvasMenu;
    [SerializeField] AudioController audioController;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject collisioned = hit.collider.gameObject;

                if (collisioned != null) {
                    audioController.AudioBtnDef();
                    mcanvasMenu.Canvas[0].SetActive(false);
                    mcanvasMenu.Canvas[4].SetActive(true);
                    mcanvasMenu.CapsuleTapToPlay.SetActive(false);
                }
            }
        }
    }
}