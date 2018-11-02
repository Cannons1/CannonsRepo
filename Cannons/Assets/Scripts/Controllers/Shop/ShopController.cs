using UnityEngine.UI;
using UnityEngine;

public class ShopController : MonoBehaviour {

    [SerializeField] GameObject skinButtonPref;
    [SerializeField] GameObject skinContainer;
    [SerializeField] SkinData skinInfo;
    [SerializeField] AudioController audioController;
    [SerializeField] CanvasMenu canvasManager;
    [SerializeField] WriteVbles writeVbles;

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

            if ((GameManager.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
                container.transform.GetChild(1).gameObject.SetActive(true);
                container.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        //buySkin(GameManager.Instance.currentSkin, 0);
        skinContainer.transform.GetChild(GameManager.Instance.currentSkin).GetChild(4).gameObject.SetActive(true);
    }

    public void buySkin(int skinIndex, int _value)
    {  
        if (!((GameManager.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)) 
        {
            int value = _value;
            if (Singleton.instance.Coins >= value)
            {
                Singleton.instance.Coins -= value;
                PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
                writeVbles.WriteOnPurchase();
                GameManager.Instance.skinAvailability += 1 << skinIndex;
                GameManager.Instance.Save();
                skinContainer.transform.GetChild(skinIndex).GetChild(0).gameObject.SetActive(false);
                skinContainer.transform.GetChild(skinIndex).GetChild(1).gameObject.SetActive(true);
                skinContainer.transform.GetChild(skinIndex).GetChild(3).gameObject.SetActive(false);
                audioController.AudioTropicalWin();
            }
            else {
                canvasManager.Canvas[3].SetActive(true);
                canvasManager.Canvas[3].GetComponentInChildren<Animator>().SetBool("OpenShop", true);
                audioController.AudioBtnDef();
            }
        }
    }    
    public void SelectSkin(int skinIndex)
    {
        skinContainer.transform.GetChild(GameManager.Instance.currentSkin).GetChild(4).gameObject.SetActive(false);
        if ((GameManager.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)
        {
            audioController.AudioBtnDef();
            skinContainer.transform.GetChild(skinIndex).GetChild(4).gameObject.SetActive(true);
            GameManager.Instance.currentSkin = skinIndex;
            GameManager.Instance.Save();
        }
    }   
}


