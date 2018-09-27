using UnityEngine;

public class CollectingCoinExpPoints : MonoBehaviour
{
    private int coin = 1;
    AudioUI audioUI;

    private void Start()
    {
        audioUI = FindObjectOfType<AudioUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisioned = other.gameObject;
        if (collisioned.GetComponent<ICoins>() != null)
        {
            audioUI.AudioCoins();
            ICoins iCoins;
            iCoins = collisioned.GetComponent<ICoins>();
            iCoins.CoinsCollected(coin);
        }
    }
}
