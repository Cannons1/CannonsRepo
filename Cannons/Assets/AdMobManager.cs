using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour {

    [SerializeField] string appID = "ca-app-pub-6196431305860923~5849023719";
    [SerializeField] string rewardID = "ca - app - pub - 6196431305860923 / 2279360087";
    RewardBasedVideoAd rewardVideo;

    private void Awake()
    {
        MobileAds.Initialize(appID);
        rewardVideo = RewardBasedVideoAd.Instance;
        rewardVideo.OnAdRewarded += OnRewardPlayer;
        rewardVideo.OnAdClosed += RewardVideoClosed;
        RequestRewardAD();
    }

    private void OnRewardPlayer(object sender, EventArgs args)
    {
        Singleton.instance.Coins += 20;
        Singleton.SaveCoins();
    }

    private void RewardVideoClosed(object sender, EventArgs args) { RequestRewardAD(); }
 
    private void RequestRewardAD()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardVideo.LoadAd(request, rewardID);        
    }

    public void ShowRewardVideo()
    {
        if (rewardVideo.IsLoaded())
        {
            rewardVideo.Show();
        }
        else
        {
            RequestRewardAD();
        }
    }
}
