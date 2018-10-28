using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameAnalyticsSDK;

public class WinCondition : MonoBehaviour {

    public int level;

    [SerializeField] GameObject canvasWin;
    [SerializeField] Text[] winTxt;
    [SerializeField] Animator[] anim;
    [SerializeField] ParticleSystem cParticle;
    [SerializeField] AudioController audioController;
    [SerializeField] IGLevelManager iGLevelManager;

    private bool win = false;

    public bool WinBool {
        get { return win; }
    }

    public void Win(Rigidbody _wills) {
        win = true;
        _wills.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(ActivatingCanvas());
        if (level > Singleton.instance.LvlsUnlocked) {
            Singleton.instance.LvlsUnlocked = level;
            Singleton.SaveUnlockLevels();
        }
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level.ToString());
    }

    IEnumerator ActivatingCanvas() {
        iGLevelManager.canvas[0].SetActive(false);
        anim[0].SetBool("Opened", true);
        yield return new WaitForSeconds(0.7f);
        audioController.AudioOpenChest();
        audioController.AudioTropicalWin();
        yield return new WaitForSeconds(0.1f);
        cParticle.Play();
        for (int i = 0; i < 3; i++) {
            audioController.AudioCoins();
            yield return new WaitForSeconds(0.16f);
        }
        yield return new WaitForSeconds(1.2f);
        anim[1].SetBool("Win", true);
        yield return new WaitForSeconds(0.32f);
        canvasWin.SetActive(true);
        winTxt[1].text = "Level " + level.ToString() + " Complete"; 
        cParticle.Stop();
    }

    public void CoinsMgr(int _coins) {
        Singleton.instance.Coins += _coins;
        winTxt[0].text = _coins.ToString("+0 Coins");
        Singleton.SaveCoins();
    }
}
