﻿using UnityEngine;
using UnityEngine.UI;

public class WriteVbles : MonoBehaviour
{
    public Slider experience;
    [SerializeField] Text numberOfCoins, numCoinsInRetry, characterLvl; 
    [SerializeField] ExpCoinPoinMgr mExperience;
    [SerializeField] AudioUI mAudioUI;

    int percentaje;
    
    private void Start()
    {
        percentaje = 30;
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
        characterLvl.text = Singleton.instance.Lvl.ToString("0");
        experience.maxValue = Singleton.instance.MaxValue;
        experience.value = Singleton.instance.Experience;
        WriteExp();
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }
    public void WriteCoinInRetry() {
        numCoinsInRetry.text = Singleton.instance.Coins.ToString("0");
    }
    public void WriteCharacterLvl() {
        characterLvl.text = Singleton.instance.Lvl.ToString("0");
    }
    public void WriteExp()
    {
        experience.value = Singleton.instance.Experience;
        if (Singleton.instance.Experience >= experience.maxValue)
        {
            mExperience.SavedLastExp = true;
            Singleton.instance.Lvl++; Singleton.instance.LvlInGame++;//Lvl up
            experience.maxValue += percentaje;//Ten percent more each time you lvl up
            Singleton.instance.MaxValue += percentaje; Singleton.instance.MaxValueInGame += percentaje;
            experience.value = 0;
            Singleton.instance.ExpInGame = 0; Singleton.instance.Experience = 0;
            WriteCharacterLvl();
        }
    }
}
