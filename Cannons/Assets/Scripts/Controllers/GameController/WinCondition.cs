using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCondition : MonoBehaviour {
    [Header("Win prize")]
    [SerializeField] int coins, exp;
    [SerializeField] GameObject canvasWin;
    [SerializeField] Text winCoinsTxt;
    CanvasMgr mCanvasMgr;
    WriteVbles mWriteVbles;
    IGLevelManager igLvlMgr;
    public int level;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        igLvlMgr = (IGLevelManager)FindObjectOfType(typeof(IGLevelManager));
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mCanvasMgr = (CanvasMgr)FindObjectOfType(typeof(CanvasMgr));
    }

    public void Win(Rigidbody _wills) {
        anim.SetBool("Opened", true);
        _wills.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(OpeningChest());
        ExpCoinsMgr();
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

    private void ExpCoinsMgr() {
        Singleton.instance.Coins += coins;
        Singleton.instance.Experience += exp;
        mWriteVbles.WriteExp();
        mWriteVbles.WritingNumberOfCoins();
        winCoinsTxt.text = coins.ToString("+0 Coins");
        SavePlayerStatus();
    }

    public void SavePlayerStatus() {
        PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
        PlayerPrefs.SetInt("Exp", Singleton.instance.Experience);
        PlayerPrefs.SetInt("Lvl", Singleton.instance.Lvl);
        PlayerPrefs.SetInt("MaxValue", Singleton.instance.MaxValue);
    }
}
