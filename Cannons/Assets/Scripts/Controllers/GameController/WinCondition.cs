using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCondition : MonoBehaviour {
    [Header("Win prize")]
    [SerializeField] int coins, exp;
    [SerializeField] Text winCoinsTxt, winExpTxt;
    [SerializeField] GameObject canvasWin;
    CanvasMgr mCanvasMgr;
    WriteVbles mWriteVbles;

    private void Start()
    {
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mCanvasMgr = (CanvasMgr)FindObjectOfType(typeof(CanvasMgr));
    }

    public void Win() {
        mCanvasMgr.Canvas[0].SetActive(false);
        canvasWin.SetActive(true);
        ExpCoinsMgr();
        Time.timeScale = 0;
    }

    private void ExpCoinsMgr() {
        Singleton.instance.Coins += coins;
        Singleton.instance.Experience += exp;
        mWriteVbles.WriteExp();
        mWriteVbles.WritingNumberOfCoins();
        winCoinsTxt.text = coins.ToString("+0");
        winExpTxt.text = exp.ToString("+0");
    }
}
