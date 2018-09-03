using UnityEngine;

public class UnlockLevels : MonoBehaviour {

    ButtonsLocked[] lvlsUnlocked;
    private int lvlsToUnlock;

    private void Start()
    {
        lvlsUnlocked = GetComponentsInChildren<ButtonsLocked>();

        if (PlayerPrefs.HasKey("LvlUnlocked")) {
            lvlsToUnlock = PlayerPrefs.GetInt("LvlUnlocked");
            Debug.Log(lvlsToUnlock);
        }

        foreach (ButtonsLocked a in lvlsUnlocked) {
            a.Locked();
        }
        for (int i = 0; i <= lvlsToUnlock; i++) {
            lvlsUnlocked[i].Unlocked();
        }
    }
}
