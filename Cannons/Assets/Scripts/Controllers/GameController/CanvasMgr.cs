using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMgr : MonoBehaviour {

    public GameObject[] Canvas {
        get { return canvas; }
        set { canvas = value; }
    }

    public static int unnpause;

    [SerializeField] GameObject[] canvas;
    [SerializeField] AudioUI mAduioUI;
    [SerializeField] GameObject txtTapToShoot;
    [SerializeField] Text lvlName, lvlNamePaused;
    [SerializeField] WinCondition winCondition;

    WaitForSeconds txtActive = new WaitForSeconds(6f);

    static int countTxtActive;

    private void Start()
    {
        unnpause = 0;

        if(SceneManager.GetActiveScene().name == "Lvl1")
            if(countTxtActive <1)
                StartCoroutine(TimeTxtActive());

        StartCoroutine(LvlName());
        lvlNamePaused.text = "Level " + winCondition.level.ToString();
    }

    IEnumerator LvlName() {
        lvlName.text = "Level " + winCondition.level.ToString();
        yield return new WaitForSeconds(2);
        lvlName.text = " ";
    }

    IEnumerator TimeTxtActive()
    {
        countTxtActive++;
        txtTapToShoot.SetActive(true);
        yield return txtActive;
        txtTapToShoot.SetActive(false);
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unnpause ++;
            switch (unnpause) {
                case 1:
                    PauseButton();
                    break;
                case 2:
                    ResumeButton();
                    break;
                default:
                    break;
            }
        }
	}

    private void PauseButton()
    {
        if (canvas[0].activeInHierarchy) {
            mAduioUI.AudioButtonDefault();
            canvas[0].SetActive(false);
            canvas[1].SetActive(true);
        }
        IGLevelManager.unpause = false;
        Time.timeScale = 0;
    }

    private void ResumeButton()
    {
        mAduioUI.AudioButtonBack();
        canvas[0].SetActive(true);
        canvas[1].SetActive(false);
        IGLevelManager.unpause = true;
        Time.timeScale = 1;
        unnpause = 0;
    }
}
