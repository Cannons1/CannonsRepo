using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;

public class AdMobManager : MonoBehaviour {

    [SerializeField] string appID = "";
    [SerializeField] public static string coinsRewardId = "";
    [SerializeField] public static string lifeRewardId = "";
    [SerializeField] string interstitialID = "";
    [SerializeField] float interstitialProbability;

    public Action<float> interstitialHandler;
    [SerializeField] int reward;
    [SerializeField] GameObject panelReward; 

    public float InterstitialProbability { get { return interstitialProbability; } }

    [SerializeField] WriteVbles writeVbles;

    public static RewardBasedVideoAd coinsVideo;
    public static RewardBasedVideoAd lifeVideo;
    public static InterstitialAd interstitial;

    private void Awake()
    {
        #if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544~3347511713";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif

        interstitialHandler = ShowInterstitialAD;
        //For test
        coinsRewardId = "ca-app-pub-3940256099942544/5224354917";
        lifeRewardId = "ca-app-pub-3940256099942544/5224354917";
        interstitialID = "ca-app-pub-3940256099942544/1033173712";

        coinsVideo = RewardBasedVideoAd.Instance;
        lifeVideo = RewardBasedVideoAd.Instance;
                                     
        interstitial = new InterstitialAd(interstitialID);

        coinsVideo.OnAdRewarded += OnGiveCoins;

        interstitial.OnAdClosed += InterstitialClosed;

        coinsVideo.OnAdClosed += RewardVideoClosed;

        RequestRewardAD();
        RequestInterstitialAD();
        RequestLifeVideo();
    }

    private void OnRevivePlayer(object sender, EventArgs args)
    {
        DieEvent.DesactivatePanel();
        Will.will.Revive();
        Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
        Time.timeScale = 1;
        lifeVideo.OnAdCompleted -= OnRevivePlayer;

    }


    private void OnCancelRevive(object sender, EventArgs args)
    {
        lifeVideo.OnAdCompleted -= OnRevivePlayer;
        lifeVideo.OnAdClosed -= OnCancelRevive;
    }

    public void RequestLifeVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        lifeVideo.LoadAd(request, lifeRewardId);
    }

    public void ShowReviveVideo()
    {
        if (lifeVideo.IsLoaded())
        {
            lifeVideo.Show();
            lifeVideo.OnAdCompleted += OnRevivePlayer;
            lifeVideo.OnAdClosed += OnCancelRevive;
        }
        else
        {
            RequestLifeVideo();
        }

        //DieEvent.DesactivatePanel();
        //Will.will.Revive();
        //Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
        //Time.timeScale = 1;
    }

    private void OnGiveCoins(object sender, EventArgs args)
    {
        if (!panelReward.activeInHierarchy) panelReward.SetActive(true);
        Singleton.SaveCoins(reward);
        writeVbles.WriteOnPurchase();
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
        coinsVideo.LoadAd(request, coinsRewardId);        
    }

    public void ShowRewardVideo()
    {
        if (coinsVideo.IsLoaded())
            coinsVideo.Show();
        else
            RequestRewardAD();
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
