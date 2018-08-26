using UnityEngine;

public class CollectingCoinExpPoints : MonoBehaviour
{
    private int coin = 1;
    private int point = 1;
    private int exp;
    AudioItems mAudioItems;

    private void Start()
    {
        mAudioItems = FindObjectOfType<AudioItems>();

        if (Singleton.instance.ExpBoost == 0)
            Debug.Log("SinBoost");
        else
            Debug.Log("ConBoost");
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

        if (collisioned.GetComponent<IExperience>() != null)
        {
            if (Singleton.instance.ExpBoost == 0)
                exp = Random.Range(1, 4);
            else
                exp = Random.Range(3, 6);
            IExperience iExperience;
            iExperience = collisioned.GetComponent<IExperience>();
            iExperience.EarningExperience(exp);
        }

        if (collisioned.GetComponent<IPoints>() != null)
        {
            IPoints iPoints;
            iPoints = collisioned.GetComponent<IPoints>();
            iPoints.GettingPoints(point);
        }
    }
}
