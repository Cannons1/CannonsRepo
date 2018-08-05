using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int currentSkin = 0;
    public int skinAvailability = 1;
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
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkin);
        PlayerPrefs.SetInt("SkinAvailibility", skinAvailability);
    }
}
