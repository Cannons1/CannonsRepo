﻿using UnityEngine;
using UnityEngine.UI;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins; 
    [SerializeField] Text[] menuCoinsTxt;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
            Singleton.instance.Coins = PlayerPrefs.GetInt("Coins");

        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
        WriteOnPurchase();
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }
    public void WriteOnPurchase() {
        foreach (Text coinsTxt in menuCoinsTxt) {
            coinsTxt.text = Singleton.instance.Coins.ToString();
        }
    }
}
