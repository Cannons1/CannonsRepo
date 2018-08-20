using UnityEngine;
using UnityEngine.UI;

public class ExpBoost : DateTimeController
{
    [SerializeField] Button expBoostButton;
    public static int cost = 150;
    WriteVbles mWriteVbles;

    private void Start()
    {
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        if (PlayerPrefs.HasKey("BoostExp")) {
            Singleton.instance.ExpBoost = PlayerPrefs.GetInt("BoostExp");
            if (Singleton.instance.ExpBoost == 1)
            {
                expBoostButton.interactable = false;
            }
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
            PlayerPrefs.SetInt("BoostExp", Singleton.instance.ExpBoost);
            mWriteVbles.WritingNumberOfCoins();
        }
    }

    public void ExpBoostReady()
    {
        Singleton.instance.ExpBoost = 0;
        expBoostButton.interactable = true;
    }
}
