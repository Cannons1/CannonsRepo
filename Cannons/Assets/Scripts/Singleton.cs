﻿using System.Collections;
using System.Collections.Generic;
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
    public int CoinsInGame {
        get { return coinsInGame; }
        set { coinsInGame = value; }
    }
    private int coins;
    private int coinsInGame;
    #endregion

    #region Experience
    public int Experience {
        get { return experience; }
        set { experience = value; }
    }
    public int ExpInGame {
        get { return expInGame; }
        set { expInGame = value; }
    }
    public int LvlInGame {
        get { return lvlInGame; }
        set { lvlInGame = value; }
    }
    public int MaxValue {
        get { return maxValue; }
        set { maxValue = value; }
    }
    public int MaxValueInGame {
        get { return maxValueInGame; }
        set { maxValueInGame = value; }
    }
    private int experience;
    private int expInGame;
    private int lvlInGame;
    private int maxValue;
    private int maxValueInGame;
    #endregion

    #region lvl
    public int Lvl
    {
        get { return lvl; }
        set { lvl = value; }
    }
    private int lvl;
    #endregion

    #region ints for points
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    public int PointsInGame
    {
        get { return pointsInGame; }
        set { pointsInGame = value; }
    }
    private int points;
    private int pointsInGame;
    #endregion

    #region shop boosts
    public byte ExpBoost {
        get { return expBoost; }
        set { expBoost = value; }
    }
    private byte expBoost;
    #endregion

    #region dailyGifts
    public byte DailyGifts
    {
        get { return dailyGifts; }
        set { dailyGifts = value; }
    }
    public byte ActiveToggles {
        get { return activeToggles; }
        set { activeToggles = value; }
    }
    private byte dailyGifts;
    private byte activeToggles;
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
        lvl = 1;
    }
}
