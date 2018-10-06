﻿using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Material Handv
    {
        get
        {
            return handv;
        }
    }

    public Material Rotating
    {
        get
        {
            return rotating;
        }
    }

    public Material StaticCannon
    {
        get
        {
            return staticCannon;
        }
    }

    public int currentSkin = 0;
    public int skinAvailability = 1;

    [SerializeField] Material handv, rotating, staticCannon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            currentSkin = PlayerPrefs.GetInt("CurrentSkin");
            skinAvailability = PlayerPrefs.GetInt("SkinAvailibility");
        }
        else Save();

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkin);
        PlayerPrefs.SetInt("SkinAvailibility", skinAvailability);
    }
}
