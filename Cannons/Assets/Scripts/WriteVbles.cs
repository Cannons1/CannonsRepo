using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins, characterLvl, numberOfPoints, highScore;
    public Slider experience;
    [SerializeField] ExpCoinPoinMgr mExperience;
    [SerializeField] AudioUI mAudioUI;

    int percentaje;

    private void Start()
    {
        percentaje = 3;
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
        characterLvl.text = Singleton.instance.Lvl.ToString("0");
        highScore.text = Singleton.instance.Points.ToString("0");
        experience.maxValue += Singleton.instance.MaxValue;
        experience.value = Singleton.instance.Experience;
    }
    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.Coins.ToString("0");
    }
    public void WritingPoints()
    {
        numberOfPoints.text = Singleton.instance.PointsInGame.ToString("0");
        if (Singleton.instance.PointsInGame > Singleton.instance.Points)
        {
            Singleton.instance.Points = Singleton.instance.PointsInGame;
            highScore.text = Singleton.instance.Points.ToString("0");
        }
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
