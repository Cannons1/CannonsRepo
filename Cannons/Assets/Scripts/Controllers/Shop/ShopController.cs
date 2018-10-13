using UnityEngine.UI;
using UnityEngine;

public class ShopController : MonoBehaviour {

    [SerializeField] GameObject skinButtonPref;
    [SerializeField] GameObject skinContainer;
    [SerializeField] SkinData skinInfo;
    [SerializeField] AudioUI audioUI;
    [SerializeField] CanvasMenu canvasManager;
    [SerializeField] WriteVbles writeVbles;

    bool isInShop;

    public bool IsInShop
    {
        get
        {
            return isInShop;
        }

        set
        {
            isInShop = value;
        }
    }

    private void Start()
    {
        isInShop = false;
               
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
        buySkin(GameManager.Instance.currentSkin, 0);
        skinContainer.transform.GetChild(GameManager.Instance.currentSkin).GetChild(4).gameObject.SetActive(true);
    }

    public void buySkin(int skinIndex, int _value)
    {
        //(skinAvailability & 1) produces a value that is either 1 or 0, depending on the least significant bit of skinAvailability.
        //1 << skinIndex  bytes representation of skinIndex.      
        if (!((GameManager.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)) //Si la disponibilidad de skin no es igual a la skin a comprar
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
                audioUI.SoundClaimGift();
            }
            else {
                isInShop = true;
                canvasManager.Canvas[3].SetActive(true);
                canvasManager.Canvas[3].GetComponentInChildren<Animator>().SetBool("OpenShop",true);
                audioUI.AudioButtonDefault();
            }
        }
    }    
    public void SelectSkin(int skinIndex)
    {
        skinContainer.transform.GetChild(GameManager.Instance.currentSkin).GetChild(4).gameObject.SetActive(false);
        if ((GameManager.Instance.skinAvailability & 1 << skinIndex) == 1 << skinIndex)
        {
            audioUI.AudioButtonDefault();
            skinContainer.transform.GetChild(skinIndex).GetChild(4).gameObject.SetActive(true);
            GameManager.Instance.currentSkin = skinIndex;
            GameManager.Instance.Save();
        }
    }   
}


