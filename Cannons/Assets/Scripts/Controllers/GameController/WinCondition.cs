using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    }

    IEnumerator ActivatingCanvas() {
        WaitForSeconds one = new WaitForSeconds(0.7f);
        WaitForSeconds two = new WaitForSeconds(0.1f);
        WaitForSeconds three = new WaitForSeconds(0.16f);
        WaitForSeconds four = new WaitForSeconds(1.2f);
        WaitForSeconds five = new WaitForSeconds(0.32f);
        iGLevelManager.canvas[0].SetActive(false);
        anim[0].SetBool("Opened", true);
        yield return one;
        audioController.AudioOpenChest();
        audioController.AudioTropicalWin();
        yield return two;
        cParticle.Play();
        for (int i = 0; i < 3; i++) {
            audioController.AudioCoins();
            yield return three;
        }
        yield return four;
        anim[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2f, Screen.height-(Screen.height*0.28f), 10));
        anim[1].SetBool("Win", true);
        yield return five;
        canvasWin.SetActive(true);
        winTxt[1].text = string.Format("Level {0} complete",level); 
        cParticle.Stop();
    }

    public void CoinsMgr(int _coins) {
        Singleton.instance.Coins += _coins;
        winTxt[0].text = _coins.ToString("+0 Coins");
        Singleton.SaveCoins();
    }
}
