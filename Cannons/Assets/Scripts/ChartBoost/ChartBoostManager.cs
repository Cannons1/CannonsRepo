using ChartboostSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartBoostManager : MonoBehaviour
{
    void Start()
    {
        Chartboost.didCompleteRewardedVideo += RewardCoins;
    }

    private void RewardCoins(CBLocation arg1, int arg2)
    {
        Singleton.instance.Coins += 60;
        WriteVbles.sharedInstance.WritingNumberOfCoins();
    }


    public void ShowRewardedVideo()
    {
        if (Chartboost.hasRewardedVideo(CBLocation.HomeScreen))
        {
            Chartboost.hasRewardedVideo(CBLocation.HomeScreen);
        }
        else
        {
            Chartboost.hasRewardedVideo(CBLocation.HomeScreen);
        }
    }


}
