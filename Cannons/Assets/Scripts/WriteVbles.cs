using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteVbles : MonoBehaviour
{
    [SerializeField] Text numberOfCoins, characterLvl, numberOfPoints, highScore;
    [SerializeField] Slider experience;
    int percentaje;

    private void Start()
    {
        numberOfCoins.text = Singleton.instance.coins.ToString("0");
        experience.value = Singleton.instance.experience;
        characterLvl.text = Singleton.instance.lvl.ToString("0");
        highScore.text = Singleton.instance.points.ToString("0");
        percentaje = 3;
        experience.maxValue += Singleton.instance.maxValue;
    }

    public void WritingNumberOfCoins()
    {
        numberOfCoins.text = Singleton.instance.coins.ToString("0");
    }

    public void WritingPoints()
    {
        numberOfPoints.text = Singleton.instance.pointsInGame.ToString("0");

        if (Singleton.instance.pointsInGame > Singleton.instance.points)
        {
            Singleton.instance.points = Singleton.instance.pointsInGame;
            highScore.text = Singleton.instance.points.ToString("0");
        }
    }

    public void WriteExp()
    {
        experience.value += Singleton.instance.expInGame;

        if (Singleton.instance.experience >= experience.maxValue)
        {
            Singleton.instance.lvlInGame++;//Lvl up
            Singleton.instance.lvl++;
            experience.maxValue += percentaje;//Ten percent more each time you lvl up
            Singleton.instance.maxValue += percentaje;
            Singleton.instance.maxValueInGame += percentaje;
            experience.value = 0;
            Singleton.instance.expInGame = 0;
            Singleton.instance.experience = 0;
            characterLvl.text = Singleton.instance.lvl.ToString("0");
        }
    }
}
