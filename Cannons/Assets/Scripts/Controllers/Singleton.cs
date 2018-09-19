using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance = null;

    #region booleans for muted audios(Configuration)
    public bool SfxMuted {
        get { return sFxMuted; }
        set { sFxMuted = value; }
    }
    public bool MusicMuted {
        get { return musicMuted; }
        set { musicMuted = value; }
    }
    private bool sFxMuted;
    private bool musicMuted;
    #endregion

    #region ints for coins
    public int Coins {
        get { return coins; }
        set { coins = value; }
    }
    private int coins;
    #endregion

    #region lvl
    public int LvlsUnlocked
    {
        get
        {
            return lvlsUnlocked;
        }

        set
        {
            lvlsUnlocked = value;
        }
    }
    private int lvlsUnlocked;
    #endregion

    #region dailyGifts
    public int DailyGifts
    {
        get { return dailyGifts; }
        set { dailyGifts = value; }
    }
    public int ActiveToggles {
        get { return activeToggles; }
        set { activeToggles = value; }
    }


    private int dailyGifts;
    private int activeToggles;
    #endregion

    void Awake()
    {
        if (!PlayerPrefs.HasKey("LvlUnlocked"))
        {
            lvlsUnlocked = 0;
            PlayerPrefs.SetInt("LvlUnlocked", lvlsUnlocked);
        }
        else {
            lvlsUnlocked = PlayerPrefs.GetInt("LvlUnlocked");
        }
        if (!PlayerPrefs.HasKey("DailyCount"))
        {
            dailyGifts = 0;
            PlayerPrefs.SetInt("DailyCount", dailyGifts);
        }
        else {
            dailyGifts = PlayerPrefs.GetInt("DailyCount");
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void ResetStats()
    {
        PlayerPrefs.DeleteKey("LvlUnlocked");
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("DailyCount");
        PlayerPrefs.DeleteKey("TogglesActive");
        PlayerPrefs.DeleteKey("Daily");
    }

    public static void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", instance.coins);
    }

    public static void SaveUnlockLevels() {
        PlayerPrefs.SetInt("LvlUnlocked", instance.lvlsUnlocked);
    }
}
