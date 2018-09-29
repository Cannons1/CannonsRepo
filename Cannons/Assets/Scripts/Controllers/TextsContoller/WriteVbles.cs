using UnityEngine;
using UnityEngine.UI;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins; 
    [SerializeField] AudioUI mAudioUI;
    [SerializeField] Text[] menuCoinsTxt;

    private void Start()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }
    public void WriteOnPurchase() {
        for (int i = 0; i < menuCoinsTxt.Length; i++) {
            menuCoinsTxt[i].text = Singleton.instance.Coins.ToString();
        }
    }
}
