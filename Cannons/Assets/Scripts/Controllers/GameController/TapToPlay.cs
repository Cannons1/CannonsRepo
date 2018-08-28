﻿using UnityEngine;

public class TapToPlay : MonoBehaviour
{
    CanvasMenu mcanvasMenu;

    private void Start()
    {
        mcanvasMenu = GetComponent<CanvasMenu>();
    }

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
                    mcanvasMenu.Canvas[0].SetActive(false);
                    mcanvasMenu.Canvas[5].SetActive(true);
                    mcanvasMenu.DecorateCannon.SetActive(false);
                    mcanvasMenu.CapusleTapToPlay.SetActive(false);
                }
            }
        }
    }
}