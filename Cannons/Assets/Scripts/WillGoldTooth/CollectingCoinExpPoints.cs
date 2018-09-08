using UnityEngine;

public class CollectingCoinExpPoints : MonoBehaviour
{
    private int coin = 1;
    AudioItems mAudioItems;

    private void Start()
    {
        mAudioItems = FindObjectOfType<AudioItems>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisioned = other.gameObject;
        if (collisioned.GetComponent<ICoins>() != null)
        {
            mAudioItems.AudioCoin();
            ICoins iCoins;
            iCoins = collisioned.GetComponent<ICoins>();
            iCoins.CoinsCollected(coin);
        }
    }
}
