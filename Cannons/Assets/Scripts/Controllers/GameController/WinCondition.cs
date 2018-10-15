using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCondition : MonoBehaviour {
    [Header("Win prize")]
    [SerializeField] int coins;
    [SerializeField] GameObject canvasWin;
    [SerializeField] Text[] winTxt;
    [SerializeField] CanvasMgr mCanvasMgr;
    [SerializeField] WriteVbles mWriteVbles;
    public int level;

    [SerializeField] Animator[] anim;
    [SerializeField] ParticleSystem cParticle;

    public bool win = false;

    public void Win(Rigidbody _wills) {
        anim[0].SetBool("Opened", true);
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
        mCanvasMgr.Canvas[0].SetActive(false);
        yield return new WaitForSeconds(0.8f);
        cParticle.Play();
        yield return animLength;
        anim[1].SetBool("Win", true);
        yield return new WaitForSeconds(0.32f);
        canvasWin.SetActive(true);
        winTxt[1].text = "Level " + level.ToString() + " Complete"; 
        cParticle.Stop();
    }

    private void CoinsMgr() {
        Singleton.instance.Coins += coins;
        mWriteVbles.WritingNumberOfCoins();
        winTxt[0].text = coins.ToString("+0 Coins");
        Singleton.SaveCoins();
    }
}
