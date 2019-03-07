using ChartboostSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartBoostManager : MonoBehaviour
{
    void Start()
    {
        Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        Chartboost.didCompleteRewardedVideo += RewardCoins;       
    }

    private void RewardCoins(CBLocation arg1, int arg2)
    {
        Singleton.instance.Coins += 60;
        WriteVbles.sharedInstance.WriteOnPurchase();
    }


    public void ShowRewardedVideo()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.HomeScreen))
        {
            Chartboost.showRewardedVideo(CBLocation.HomeScreen);
        }
        else
        {
            Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        }       
    }

    public void ShowLifeVideo()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.HomeScreen))
        {
            Chartboost.showRewardedVideo(CBLocation.HomeScreen);
        }
        else
        {
            Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        }
    }

}
