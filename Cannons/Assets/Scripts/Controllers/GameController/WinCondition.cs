using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCondition : MonoBehaviour {
    [Header("Win prize")]
    [SerializeField] int coins;
    [SerializeField] GameObject canvasWin;
    [SerializeField] Text winCoinsTxt;
    CanvasMgr mCanvasMgr;
    WriteVbles mWriteVbles;
    public int level;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mCanvasMgr = (CanvasMgr)FindObjectOfType(typeof(CanvasMgr));
    }

    public void Win(Rigidbody _wills) {
        anim.SetBool("Opened", true);
        _wills.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(OpeningChest());
        CoinsMgr();
        if (level > Singleton.instance.LvlsUnlocked) {
            Singleton.instance.LvlsUnlocked = level;
            PlayerPrefs.SetInt("LvlUnlocked", Singleton.instance.LvlsUnlocked);
        }
    }

    WaitForSeconds animLength = new WaitForSeconds(2f);//anim openChestLength

    IEnumerator OpeningChest() {
        yield return animLength;
        mCanvasMgr.Canvas[0].SetActive(false);
        canvasWin.SetActive(true);
    }

    private void CoinsMgr() {
        Singleton.instance.Coins += coins;
        mWriteVbles.WritingNumberOfCoins();
        winCoinsTxt.text = coins.ToString("+0 Coins");
        SavePlayerStatus();
    }

    public void SavePlayerStatus() {
        PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
    }
}
