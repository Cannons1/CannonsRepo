﻿using UnityEngine;

public class ButtonsShop : MonoBehaviour {

    int id;
    IAPManager mIAP;
    [SerializeField] AudioUI audioUI;

    private void Start()
    {
        mIAP = GetComponent<IAPManager>();
    }

    public void ButtonBuyIAP(int _id) {
        id = _id;
        audioUI.AudioButtonDefault();
        switch (id) {
            case 0:
                mIAP.BuyFirstProduct();
                break;
            case 1:
                mIAP.BuySecondProduct();
                break;
            case 2:
                mIAP.BuyThirdProduct();
                break;
            case 3:
                mIAP.BuyFourthProduct();
                break;
            case 4:
                mIAP.BuyFifthProduct();
                break;
            default:
                break;
        }
    }
}