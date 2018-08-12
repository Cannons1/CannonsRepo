using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCondition : MonoBehaviour {
    [Header("Win prize")]
    [SerializeField] int coins, exp;
    [SerializeField] Text winCoinsTxt, winExpTxt;
    [SerializeField] GameObject canvasWin;
    CanvasMgr mCanvasMgr;

    private void Start()
    {
        mCanvasMgr = (CanvasMgr)FindObjectOfType(typeof(CanvasMgr));
    }

    public void Win() {
        mCanvasMgr.Canvas[0].SetActive(false);
        Singleton.instance.Coins += coins;
        Singleton.instance.Experience += exp;

        canvasWin.SetActive(true);
        winCoinsTxt.text = coins.ToString("+0");
        winExpTxt.text = exp.ToString("+0");
        Time.timeScale = 0;
    }
}
