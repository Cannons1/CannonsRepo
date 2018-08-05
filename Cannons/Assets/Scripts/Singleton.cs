using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance = null;

    #region booleans for muted audios(Configuration)
    public bool sFxMuted;
    public bool musicMuted;
    #endregion

    #region ints for coins
    public int coins;
    public int coinsInGame;
    #endregion

    #region floats for Experience
    public float experience;
    public float expInGame;
    public int lvlInGame;
    public float maxValue;
    public float maxValueInGame;
    #endregion

    #region lvl
    public int lvl = 1;
    #endregion

    #region ints for points
    public int points;
    public int pointsInGame;
    #endregion

    #region shop boosts
    public byte expBoost = 0;
    #endregion

    #region dailyGifts
    public int dailyGifts = 0;
    public int activeToggles = 0;
    #endregion

    void Awake()
    {
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
}
