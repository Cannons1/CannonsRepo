using ChartboostSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartBoostManager : MonoBehaviour
{
    void Start()
    {
        if (Chartboost.hasInterstitial(CBLocation.HomeScreen))
        {
            Chartboost.showInterstitial(CBLocation.HomeScreen);
        }
        else
        {
            Chartboost.cacheInterstitial(CBLocation.HomeScreen);
        }
    }

    void Update()
    {
        
    }


}
