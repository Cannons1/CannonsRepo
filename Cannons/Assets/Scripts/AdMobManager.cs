using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;

public class AdMobManager : MonoBehaviour
{

    [SerializeField] string appID = "";
    [SerializeField] string coinsRewardId = "";
    [SerializeField] string lifeRewardId = "";
    [SerializeField] string interstitialID = "";
    [SerializeField] float interstitialProbability;

    [SerializeField] int reward;
    [SerializeField] GameObject panelReward;
    [SerializeField] WriteVbles writeVbles;

    public RewardBasedVideoAd coinsVideo;
    public RewardBasedVideoAd lifeVideo;

    private void Awake()
    {
#if UNITY_ANDROID
        appID = "ca-app-pub-6196431305860923~5849023719";
        coinsRewardId = "ca-app-pub-6196431305860923/2279360087";
        lifeRewardId = "ca-app-pub-6196431305860923/5802035643";
        interstitialID = "ca-app-pub-6196431305860923/8128460863";
#elif UNITY_IPHONE
            appID = "ca-app-pub-6196431305860923~8195855577";
            coinsRewardId = "ca-app-pub-6196431305860923/9463994779";
            lifeRewardId = "ca-app-pub-6196431305860923/5841154975";
            interstitialID = "ca-app-pub-6196431305860923/1686287544";
#endif

        MobileAds.Initialize(appID);

        //For test
        //coinsRewardId = "ca-app-pub-3940256099942544/5224354917";
        //lifeRewardId = "ca-app-pub-3940256099942544/5224354917";

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            coinsVideo = new RewardBasedVideoAd();
            coinsVideo.OnAdCompleted += OnGiveCoins;
            coinsVideo.OnAdClosed += CoinsVideoClosed;
            RequestCoinsVideo();
        }
        else
        {
            lifeVideo = new RewardBasedVideoAd();
            lifeVideo.OnAdCompleted += OnRevivePlayer;
            RequestLifeVideo();
        }
    }

    private void OnRevivePlayer(object sender, EventArgs args)
    {
        //Time.timeScale = 1;
        DieEvent.DesactivatePanel();
        Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
        //Will.will.Revive();
        StartCoroutine(IGLevelManager.countDownHandler());
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


    public void RequestCoinsVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        coinsVideo.LoadAd(request, coinsRewardId);
    }

    public void ShowReviveVideo()
    {
        if (lifeVideo.IsLoaded())
        {
            lifeVideo.Show();
        }
        else
        {
            RequestLifeVideo();
        }

        
        //TEST RESPAWN IN GAME
        //Time.timeScale = 1f;
        //DieEvent.DesactivatePanel();
        //Will.will.cannonTriggered.GetComponent<CannonParent>().Reactivate();
        //Will.will.Revive();
        //StartCoroutine(IGLevelManager.countDownHandler());     
    }

    private void OnGiveCoins(object sender, EventArgs args)
    {
        if (!panelReward.activeInHierarchy) panelReward.SetActive(true);
        panelReward.GetComponent<AnimPanels>().AnimPanelReward();
        Singleton.instance.Coins += reward;
        Singleton.SaveCoins();
        writeVbles.WriteOnPurchase();
    }

    private void CoinsVideoClosed(object sender, EventArgs e)
    {
        RequestCoinsVideo();
        coinsVideo.OnAdCompleted -= OnGiveCoins;
        coinsVideo.OnAdClosed -= CoinsVideoClosed;
    }

    public void ShowCoinsVideo()
    {
        if (coinsVideo.IsLoaded())
        {
            coinsVideo.Show();
        }
        else
        {
            RequestCoinsVideo();
        }
    }
}