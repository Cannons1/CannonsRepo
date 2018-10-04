using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsActive : MonoBehaviour {

    [SerializeField] GameObject[] backButtons;
    [SerializeField] ShopController shopController;

    void OnEnable () {
        if (shopController.IsInShop)
        {
            backButtons[0].SetActive(false);
            backButtons[1].SetActive(true);
        }
        else {
            backButtons[0].SetActive(true);
            backButtons[1].SetActive(false);
        }
	}
}
