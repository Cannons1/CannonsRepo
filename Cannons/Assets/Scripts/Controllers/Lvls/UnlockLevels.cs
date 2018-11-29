using UnityEngine;

public class UnlockLevels : MonoBehaviour
{

    [SerializeField] int[] levelsInWorld;
    [SerializeField] int[] minStars;

    ButtonsLocked[] lvlsUnlocked;
    private int lvlsToUnlock;

    [SerializeField] StarsMgr starsMgr;
    public static bool[] writeStars;
    bool[] unlockedWorlds;

    public int[] MinStars
    {
        get
        {
            return minStars;
        }
    }

    private void Start()
    {
        //Init array 
        writeStars = new bool[minStars.Length];
        unlockedWorlds = new bool[minStars.Length];
        for (int i = 0; i < unlockedWorlds.Length; i++)
        {
            unlockedWorlds[i] = false;
            writeStars[i] = false;
        }

        lvlsUnlocked = GetComponentsInChildren<ButtonsLocked>();

        if (PlayerPrefs.HasKey("LvlUnlocked"))
        {
            lvlsToUnlock = PlayerPrefs.GetInt("LvlUnlocked");
            for (int i = 0; i < levelsInWorld.Length; i++)
            {
                if (lvlsToUnlock >= levelsInWorld[i] && starsMgr.TotalStars >= MinStars[i])
                {
                    unlockedWorlds[i] = true;
                }

                if (starsMgr.TotalStars >= MinStars[i])
                {
                    writeStars[i] = true;
                }
            }
        }
        foreach (ButtonsLocked a in lvlsUnlocked)
        {
            a.Locked();
        }
        for (int i = 0; i < lvlsToUnlock + 1; i++)
        {
            if (i < levelsInWorld[0])
                lvlsUnlocked[i].Unlocked();
            else if (unlockedWorlds[0] && i < levelsInWorld[1])
                lvlsUnlocked[i].Unlocked();
            else if(unlockedWorlds[1] && i < levelsInWorld[2])
                lvlsUnlocked[i].Unlocked();
            else if(unlockedWorlds[2])
                lvlsUnlocked[i].Unlocked();
        }
    }
}
