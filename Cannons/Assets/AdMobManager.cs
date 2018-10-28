using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour {

    [SerializeField] string appID = "";
    [SerializeField] string rewardID = "";
    [SerializeField] string interstitialID = "";
    [SerializeField] WriteVbles writeVbles;

    RewardBasedVideoAd rewardVideo;
    InterstitialAd interstitial;

    private void Awake()
    {
        MobileAds.Initialize(appID);
        rewardVideo = RewardBasedVideoAd.Instance;
        interstitial = new InterstitialAd(interstitialID);

        rewardVideo.OnAdRewarded += OnRewardPlayer;
        interstitial.OnAdClosed += InterstitialClosed;

        rewardVideo.OnAdClosed += RewardVideoClosed;

        RequestRewardAD();
        RequestInterstitialAD();
    }

    private void OnRewardPlayer(object sender, EventArgs args)
    {
        Singleton.instance.Coins += 20;
        Singleton.SaveCoins();
        writeVbles.WriteOnPurchase();
    }

    private void InterstitialClosed(object sender, EventArgs e) {

        RequestInterstitialAD();
    }
    private void RewardVideoClosed(object sender, EventArgs e) { RequestRewardAD(); }
 
    private void RequestInterstitialAD()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

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

    public void ShowInterstitialAD()
    {
        if(interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitialAD();
        }
    }
}
