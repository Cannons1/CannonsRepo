using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins = null, numberOfCoinsRetry =null; 
    [SerializeField] Text[] menuCoinsTxt;
    [SerializeField] Text dailyGiftTxtAmount= null;

    public static WriteVbles sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Coins")) {
            Singleton.instance.Coins = PlayerPrefs.GetInt("Coins");
        }
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            WriteOnPurchase();
        }
        else {
            WritingNumberOfCoins();
        }
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }

    public void CoinsInRetry() {
        numberOfCoinsRetry.text = Singleton.instance.Coins.ToString("0");
    }

    public void WriteOnPurchase() {
        if (menuCoinsTxt != null)
        {
            foreach (Text coinsTxt in menuCoinsTxt)
            {
                if (coinsTxt != null) {
                    coinsTxt.text = Singleton.instance.Coins.ToString();
                }
            }
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
