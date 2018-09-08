using UnityEngine;
using UnityEngine.UI;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins, numCoinsInRetry;
    [SerializeField] AudioUI mAudioUI;

    private void Start()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }
    public void WriteCoinInRetry() {
        numCoinsInRetry.text = Singleton.instance.Coins.ToString("0");
    }
}
