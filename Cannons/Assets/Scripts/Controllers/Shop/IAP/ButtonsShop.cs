using UnityEngine;
using ChartboostSDK;

public class ButtonsShop : MonoBehaviour {

    int id;
    [SerializeField] IAPManager mIAP;
    [SerializeField] AudioUI audioUI;

    private void Start()
    {
        Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        //Chartboost.didCompleteRewardedVideo += Reward;
    }

    public void ButtonBuyIAP(int _id) {
        id = _id;
        audioUI.AudioButtonDefault();
        switch (id) {
            case 0:
                mIAP.BuyFirstProduct();
                break;
            case 1:
                mIAP.BuySecondProduct();
                break;
            case 2:
                mIAP.BuyThirdProduct();
                break;
            case 3:
                mIAP.BuyFourthProduct();
                break;
            case 4:
                mIAP.BuyFifthProduct();
                break;
            default:
                break;
        }
    }
    
    public void Reward(CBLocation CB, int reward)
    {
        Singleton.instance.Coins += 10;
        Singleton.SaveCoins();
        Chartboost.didCompleteRewardedVideo -= Reward;
    }
    
    public void ShowADS()
    {
        Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        if (Chartboost.hasRewardedVideo(CBLocation.HomeScreen))
        {
            Chartboost.showRewardedVideo(CBLocation.HomeScreen);
            Chartboost.didCompleteRewardedVideo += Reward;
        }
        else
        {
            Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
        }
    }
}
