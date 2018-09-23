using UnityEngine;

public class UnlockLevels : MonoBehaviour {

    [SerializeField] int levelsFirstWorld;
    [SerializeField] int minStarsWorldOne;

    ButtonsLocked[] lvlsUnlocked;
    private int lvlsToUnlock;

    private StarsMgr starsMgr;
    public static bool secondWorld;

    public int MinStarsWorldOne
    {
        get
        {
            return minStarsWorldOne;
        }
    }

    private void Start()
    {
        secondWorld = false;
        starsMgr = GetComponent<StarsMgr>();
        lvlsUnlocked = GetComponentsInChildren<ButtonsLocked>();

        if (PlayerPrefs.HasKey("LvlUnlocked")) {
            lvlsToUnlock = PlayerPrefs.GetInt("LvlUnlocked");

            if (lvlsToUnlock >= levelsFirstWorld && starsMgr.TotalStars >= MinStarsWorldOne) {
                secondWorld = true;
            }
        }

        foreach (ButtonsLocked a in lvlsUnlocked) {
            a.Locked();
        }
        for (int i = 0; i < lvlsToUnlock + 1; i++) {
            if (i < levelsFirstWorld)
                lvlsUnlocked[i].Unlocked();
            if (secondWorld)
                lvlsUnlocked[i].Unlocked();
        }
    }

    private void Unlock() {
        Singleton.instance.LvlsUnlocked =11;
        PlayerPrefs.SetInt("LvlUnlocked", Singleton.instance.LvlsUnlocked);
    }
}
