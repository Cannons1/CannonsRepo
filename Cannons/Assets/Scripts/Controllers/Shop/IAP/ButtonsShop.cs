using UnityEngine;
using UnityEngine.Purchasing;

public class ButtonsShop : MonoBehaviour {

    int id;
    [SerializeField] IAPManager miap;
    //[SerializeField] WriteVbles writeVbles;
    
    /*public void Buy(Product _product)
    {
        switch (_product.definition.id)
        {
            case "150coins":
                Coins(150);
                break;
            case "800coins":
                Coins(950);
                break;
            case "1500coins":
                Coins(1800);
                break;
            case "3200coins":
                Coins(3800);
                break;
            case "8000coins":
                Coins(10000);
                break;
            default:
                break;
        }
    }

    private void Coins(int _coins)
    {
        Singleton.instance.Coins += _coins;
        Singleton.SaveCoins();
        writeVbles.WriteOnPurchase();
    }*/

    public void ButtonBuyIAP(int _id) {

        id = _id;
        AudioController.sharedInstance.AudioBtnDef();
        switch (id)
        {
            case 0:
                miap.BuyFirstProduct();
                break;
            case 1:
                miap.BuySecondProduct();
                break;
            case 2:
                miap.BuyThirdProduct();
                break;
            case 3:
                miap.BuyFourthProduct();
                break;
            case 4:
                miap.BuyFifthProduct();
                break;
            default:
                break;
        }
    }
}
