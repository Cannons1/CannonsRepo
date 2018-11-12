using UnityEngine;

public class UnlockLevels : MonoBehaviour {

    [SerializeField] int levelsFirstWorld, levelsOnePlusTwo = 0;
    [SerializeField] int minStarsToWorldTwo;
    [SerializeField] int minStarsToWorldThree;

    ButtonsLocked[] lvlsUnlocked;
    private int lvlsToUnlock;

    [SerializeField] StarsMgr starsMgr;
    public static bool writeMinStarsWorldTwo;
    public static bool writeMinStarsWorldThree;
    bool secondWorld;
    bool thirdWorld;

    public int MinStarsToWorldTwo
    {
        get
        {
            return minStarsToWorldTwo;
        }
    }

    public int MinStarsToWorldThree
    {
        get
        {
            return minStarsToWorldThree;
        }

        set
        {
            minStarsToWorldThree = value;
        }
    }

    private void Start()
    {
        writeMinStarsWorldTwo = false;
        writeMinStarsWorldThree = false;
        secondWorld = false;
        thirdWorld = false;
        lvlsUnlocked = GetComponentsInChildren<ButtonsLocked>();

        if (PlayerPrefs.HasKey("LvlUnlocked")) {
            lvlsToUnlock = PlayerPrefs.GetInt("LvlUnlocked");

            if (lvlsToUnlock >= levelsFirstWorld && starsMgr.TotalStars >= MinStarsToWorldTwo) {
                secondWorld = true;
            }

            if (lvlsToUnlock >= levelsOnePlusTwo && starsMgr.TotalStars >= MinStarsToWorldThree) {
                thirdWorld = true;
            }

            if (starsMgr.TotalStars >= MinStarsToWorldTwo) {
                writeMinStarsWorldTwo = true;
            }

            if (starsMgr.TotalStars >= MinStarsToWorldThree)
            {
                writeMinStarsWorldThree = true;
            }
        }

        foreach (ButtonsLocked a in lvlsUnlocked) {
            a.Locked();
        }
        for (int i = 0; i < lvlsToUnlock + 1; i++) {
            if (i < levelsFirstWorld)
                lvlsUnlocked[i].Unlocked();
            if (secondWorld && i < levelsOnePlusTwo)
                lvlsUnlocked[i].Unlocked();
            if (thirdWorld)
                lvlsUnlocked[i].Unlocked();
        }
    }
}
