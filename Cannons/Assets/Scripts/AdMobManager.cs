using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour {

    private static AdMobManager instance = null;
    public static AdMobManager Instance { get { return instance; } }

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        interstitialHandler = ShowInterstitialAD;
        //For test
        coinsRewardId = "ca-app-pub-3940256099942544/5224354917";
        lifeRewardId = "ca-app-pub-3940256099942544/5224354917";
        interstitialID = "ca-app-pub-3940256099942544/1033173712";

        coinsVideo = RewardBasedVideoAd.Instance;
        lifeVideo = RewardBasedVideoAd.Instance;
        interstitial = new InterstitialAd(interstitialID);

        coinsVideo.OnAdRewarded += OnGiveCoins;
        //lifeVideo.OnAdRewarded += OnRevivePlayer;
        //lifeVideo.OnAdCompleted += OnRevivePlayer;

        //lifeVideo.OnAdClosed += OnCancelRevive;

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
    /*
    public IEnumerator ShowReviveVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        lifeVideo.LoadAd(request, lifeRewardId);
        while(!lifeVideo.IsLoaded())
        {
            yield return null;
        }

        lifeVideo.Show();
        //DieEvent.DesactivatePanel();
        //Will.will.Revive();
        //Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
        //Time.timeScale = 1;
    }
    */
    private void OnGiveCoins(object sender, EventArgs args)
    {
        if (!panelReward.activeInHierarchy) panelReward.SetActive(true);
        Singleton.instance.Coins += reward;
        Singleton.SaveCoins();
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
