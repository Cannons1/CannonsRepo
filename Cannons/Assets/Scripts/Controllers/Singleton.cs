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
    private int dailyGifts;
    #endregion

    public int[] Stars
    {
        get
        {
            return stars;
        }

        set
        {
            stars = value;
        }
    }

    int[] stars;

    void Awake()
    {
        ResetStats();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("LvlUnlocked"))
        {
            lvlsUnlocked = 0;
            PlayerPrefs.SetInt("LvlUnlocked", lvlsUnlocked);
        }
        else
            lvlsUnlocked = PlayerPrefs.GetInt("LvlUnlocked");
        if (!PlayerPrefs.HasKey("Coins"))
        {
            Coins = 0;
            PlayerPrefs.SetInt("Coins", instance.Coins);
        }
        else 
            instance.Coins = PlayerPrefs.GetInt("Coins");
    }

    private void ResetStats()
    {
        PlayerPrefs.DeleteKey("LvlUnlocked");
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("DailyCount");
        PlayerPrefs.DeleteKey("Stars");
        PlayerPrefs.DeleteKey("ButtonDaily");
    }

    public static void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", instance.coins);
    }

    public static void SaveUnlockLevels() {
        PlayerPrefs.SetInt("LvlUnlocked", instance.lvlsUnlocked);
    }
}
