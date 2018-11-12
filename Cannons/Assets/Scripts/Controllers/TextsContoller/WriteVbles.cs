using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins = null, numberOfCoinsRetry =null; 
    [SerializeField] Text[] menuCoinsTxt;
    [SerializeField] Text dailyGiftTxtAmount= null;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Coins")) {
            Singleton.instance.Coins = PlayerPrefs.GetInt("Coins");
        }
        WriteOnPurchase();
        WritingNumberOfCoins();
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }

    public void CoinsInRetry() {
        numberOfCoinsRetry.text = Singleton.instance.Coins.ToString("0");
    }

    public void WriteOnPurchase() {
        foreach (Text coinsTxt in menuCoinsTxt) {
            coinsTxt.text = Singleton.instance.Coins.ToString();
        }
    }

    public IEnumerator CountCoins(int _plusNum) {
        int actualCoins = Singleton.instance.Coins;
        int amount = actualCoins + _plusNum;
        float t = 0f;
        float time = 3f;
        dailyGiftTxtAmount.text = _plusNum.ToString("+00");

        Singleton.SaveCoins(_plusNum);
        yield return new WaitForSeconds(1f);
        while (t < time) {
            t += Time.deltaTime;
            Singleton.instance.Coins = (int)Mathf.Lerp(actualCoins, amount, t / time);
            menuCoinsTxt[1].text = Singleton.instance.Coins.ToString();
            yield return null;
        }
        WriteOnPurchase();
    }
}
