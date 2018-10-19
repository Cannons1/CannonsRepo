using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins; 
    [SerializeField] Text[] menuCoinsTxt;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Coins")) {
            Singleton.instance.Coins = PlayerPrefs.GetInt("Coins");
        }

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

    public IEnumerator CountCoins(int _plusNum) {
        int amount = Singleton.instance.Coins + _plusNum;
        while (Singleton.instance.Coins < amount) {
            Singleton.instance.Coins++;
            menuCoinsTxt[1].text = Singleton.instance.Coins.ToString();
            yield return new WaitForEndOfFrame();
        }
        WriteOnPurchase();
        Singleton.SaveCoins();
    }
}
