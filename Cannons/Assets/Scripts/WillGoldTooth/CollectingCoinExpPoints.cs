using UnityEngine;

public class CollectingCoinExpPoints : MonoBehaviour
{
    private int coin = 1;

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisioned = other.gameObject;
        if (collisioned.GetComponent<ICoins>() != null)
        {
            ICoins iCoins;
            iCoins = collisioned.GetComponent<ICoins>();
            iCoins.CoinsCollected(coin);
        }
    }
}
