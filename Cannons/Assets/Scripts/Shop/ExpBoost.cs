using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBoost : DateTimeController
{
    [SerializeField] Button expBoostButton;
    [SerializeField] public static int cost = 150;

    //string[] s = new string[2];

    private void Start()
    {
        if (Singleton.instance.ExpBoost == 1) {
            expBoostButton.interactable = false;
        }
        OnExpBoostReady += ExpBoostReady;

    }

    public void BuyExpBoost()
    {
        if (Singleton.instance.Coins >= cost)
        {
            Singleton.instance.Coins -= cost;
            Singleton.instance.ExpBoost = 1;
            SaveExpBoostTime();
            expBoostButton.interactable = false;
        }
        /*s[0] = "boostExp";
        s[1] = Singleton.expBoost.ToString();
        Memento.memento.Save(s);*/
    }

    public void ExpBoostReady()
    {
        Singleton.instance.ExpBoost = 0;
        expBoostButton.interactable = true;
    }
}
