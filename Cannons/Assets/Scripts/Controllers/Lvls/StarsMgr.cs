﻿using UnityEngine;
using UnityEngine.UI;

public class StarsMgr : MonoBehaviour {

    UnlockStars[] unlockStars;
    static bool getArray = false;
    int totalStars;

    public int TotalStars
    {
        get
        {
            return totalStars;
        }

        set
        {
            totalStars = value;
        }
    }

    [SerializeField] Text txtTotalStars;

    private void OnEnable()
    {
        TotalStars = 0;
        unlockStars = GetComponentsInChildren<UnlockStars>();

        if (!getArray) {
            Singleton.instance.Stars = new int[unlockStars.Length];
            getArray = true;
        }

        if (PlayerPrefs.HasKey("Stars")) {
            int[] starsValueArray = PlayerPrefsX.GetIntArray("Stars");
            for (int i = 0; i < starsValueArray.Length; i++)
            {
                TotalStars += starsValueArray[i];
                Singleton.instance.Stars[i] = starsValueArray[i];
            }
        }

        txtTotalStars.text = TotalStars.ToString() + "/" + (unlockStars.Length * 3).ToString();

        for (int i = 0; i < Singleton.instance.Stars.Length; i++)
        {
            if (Singleton.instance.Stars[i] == 3)
            {
                unlockStars[i].ThreeStars();
            }
            if (Singleton.instance.Stars[i] == 2)
            {
                unlockStars[i].TwoStars();
            }
            if (Singleton.instance.Stars[i] == 1)
            {
                unlockStars[i].OneStar();
            }
        }
    }
}