using UnityEngine.UI;
using UnityEngine;

public class ShopController : MonoBehaviour {

    [SerializeField] GameObject skinButtonPref;
    [SerializeField] GameObject skinContainer;
    [SerializeField] SkinData skinInfo;
    [SerializeField] CanvasMenu canvasManager;

    private void Start()
    {
        for (int i = 0; i < skinInfo.skins.Count; i++)
        {
            GameObject container = Instantiate(skinButtonPref) as GameObject;
            container.GetComponent<Image>().sprite = skinInfo.skins[i].skinSprite;
            container.transform.GetChild(2).GetComponent<Text>().text = skinInfo.skins[i].skinName;
            container.transform.GetChild(3).GetComponent<Text>().text += skinInfo.skins[i].skinValue;
            container.transform.SetParent(skinContainer.transform, false);

            int skinIndex = i;
            int skinValue = skinInfo.skins[i].skinValue;
            container.GetComponentInChildren<Button>().onClick.AddListener(() => buySkin(skinIndex, skinValue));
            container.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SelectSkin(skinIndex));

            if ((GameController.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
                container.transform.GetChild(1).gameObject.SetActive(true);
                container.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //buySkin(GameManager.Instance.currentSkin, 0);
        skinContainer.transform.GetChild(GameController.Instance.currentSkin).GetChild(4).gameObject.SetActive(true);
    }

    public void buySkin(int skinIndex, int _value)
    {  
        if (!((GameController.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)) 
        {
            int value = _value;
            if (Singleton.instance.Coins >= value)
            {
                Singleton.instance.Coins -= value;
                PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
                WriteVbles.sharedInstance.WriteOnPurchase();
                GameController.Instance.skinAvailability += 1 << skinIndex;
                GameController.Instance.Save();
                skinContainer.transform.GetChild(skinIndex).GetChild(0).gameObject.SetActive(false);
                skinContainer.transform.GetChild(skinIndex).GetChild(1).gameObject.SetActive(true);
                skinContainer.transform.GetChild(skinIndex).GetChild(3).gameObject.SetActive(false);
                AudioController.sharedInstance.AudioTropicalWin();
            }
            else {
                canvasManager.Canvas[3].SetActive(true);
                canvasManager.Canvas[3].GetComponentInChildren<Animator>().SetBool("OpenShop", true);
                AudioController.sharedInstance.AudioBtnDef();
            }
        }
    }    
    public void SelectSkin(int skinIndex)
    {
        skinContainer.transform.GetChild(GameController.Instance.currentSkin).GetChild(4).gameObject.SetActive(false);
        if ((GameController.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)
        {
            AudioController.sharedInstance.AudioBtnDef();
            skinContainer.transform.GetChild(skinIndex).GetChild(4).gameObject.SetActive(true);
            GameController.Instance.currentSkin = skinIndex;
            GameController.Instance.Save();
        }
    }   
}


