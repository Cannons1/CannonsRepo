using UnityEngine;
using UnityEngine.Purchasing;

public class ButtonsShop : MonoBehaviour {

    int id;
    //[SerializeField] IAPManager mIAP;
    [SerializeField] AudioController audioController;
    [SerializeField] WriteVbles writeVbles;

    private void Start()
    {

    }
    
    public void Buy(Product _product)
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
    }

    public void ButtonBuyIAP(int _id) {
        
        //id = _id;
        //audiocontroller.audiobtndef();
        //switch (id) {
        //    case 0:
        //        miap.buyfirstproduct();
        //        break;
        //    case 1:
        //        miap.buysecondproduct();
        //        break;
        //    case 2:
        //        miap.buythirdproduct();
        //        break;
        //    case 3:
        //        miap.buyfourthproduct();
        //        break;
        //    case 4:
        //        miap.buyfifthproduct();
        //        break;
        //    default:
        //        break;
        //}
        
    }
}
