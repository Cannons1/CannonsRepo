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

    ParticleSystem cParticle;

    private void Start()
    {
        cParticle = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
        mWriteVbles = (WriteVbles)FindObjectOfType(typeof(WriteVbles));
        mCanvasMgr = (CanvasMgr)FindObjectOfType(typeof(CanvasMgr));
    }

    public void Win(Rigidbody _wills) {
        anim.SetBool("Opened", true);
        _wills.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(ActivatingCanvas());
        CoinsMgr();
        if (level > Singleton.instance.LvlsUnlocked) {
            Singleton.instance.LvlsUnlocked = level;
            Singleton.SaveUnlockLevels();
        }
    }

    WaitForSeconds animLength = new WaitForSeconds(1.2f);//anim openChestLength

    IEnumerator ActivatingCanvas() {
        yield return new WaitForSeconds(0.8f);
        cParticle.Play();
        yield return animLength;
        mCanvasMgr.Canvas[0].SetActive(false);
        canvasWin.SetActive(true);
        cParticle.Stop();
    }

    private void CoinsMgr() {
        Singleton.instance.Coins += coins;
        mWriteVbles.WritingNumberOfCoins();
        winCoinsTxt.text = coins.ToString("+0 Coins");
        Singleton.SaveCoins();
    }
}
