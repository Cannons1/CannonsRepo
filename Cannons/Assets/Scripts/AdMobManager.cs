using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour {

    [SerializeField] string appID = "";
    [SerializeField] string rewardID = "";
    [SerializeField] string interstitialID = "";
    [SerializeField] string interstitialVideoID = "";
    [SerializeField] float interstitialProbability;

    public Action<float> interstitialHandler;
    [SerializeField] int reward;

    public float InterstitialProbability { get { return interstitialProbability; } }

    [SerializeField] WriteVbles writeVbles;

    RewardBasedVideoAd rewardVideo;
    InterstitialAd interstitial;
    InterstitialAd interstitialVideo;

    private void Awake()
    {
        interstitialHandler = ShowInterstitialAD;
        //For test
        rewardID = "ca-app-pub-3940256099942544/5224354917";
        interstitialID = "ca-app-pub-3940256099942544/1033173712";
        interstitialVideoID = "ca-app-pub-3940256099942544/8691691433";

        rewardVideo = RewardBasedVideoAd.Instance;
        interstitial = new InterstitialAd(interstitialID);
        interstitialVideo = new InterstitialAd(interstitialVideoID);

        rewardVideo.OnAdRewarded += OnRewardPlayer;
        interstitial.OnAdClosed += InterstitialClosed;
        interstitialVideo.OnAdClosed += InterstitialVideoClosed;

        rewardVideo.OnAdClosed += RewardVideoClosed;

        RequestRewardAD();
        RequestInterstitialAD();
        RequestInterstitialVideo();       
    }

    /*
    private IEnumerator Start()
    {
        while(!interstitial.IsLoaded())
        {
            yield return null;
        }
        interstitialHandler(10f);
    }
    */
    private void OnRewardPlayer(object sender, EventArgs args)
    {
        Singleton.instance.Coins += reward;
        Singleton.SaveCoins();
        writeVbles.WriteOnPurchase();
    }

    private void InterstitialVideoClosed(object sender, EventArgs e)
    {
        RequestInterstitialVideo();
    }

    private void InterstitialClosed(object sender, EventArgs e)
    {
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


    private void RequestInterstitialVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitialVideo.LoadAd(request);
    }


    public void ShowRewardVideo()
    {
        if (rewardVideo.IsLoaded())
            rewardVideo.Show();
        else
            RequestRewardAD();
    }

    public void ShowInterstitialVideo()
    {
        if (interstitialVideo.IsLoaded())
            interstitialVideo.Show();
        else
            RequestInterstitialVideo();
    }

    public void ShowInterstitialAD(float interstitialProb)
    {
        float random = UnityEngine.Random.Range(0f, 100f);

        if (random < interstitialProb)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            else
            {
                RequestInterstitialAD();
            }
        }
    }
}
