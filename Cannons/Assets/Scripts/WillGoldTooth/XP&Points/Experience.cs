using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour, IExperience
{
    [SerializeField] WriteVbles mWriteExp;

    private void Start()
    {
        Singleton.instance.ExpInGame = 0;
        Singleton.instance.LvlInGame = 0;
        Singleton.instance.MaxValueInGame = 0;
    }

    public void EarningExperience(int _Experience)
    {
        Singleton.instance.Experience += _Experience;
        Singleton.instance.ExpInGame += _Experience;
        Debug.Log(Singleton.instance.Experience + "xp");
        mWriteExp.WriteExp();
    }

    public void MinusExperienceInGame()
    {
        Singleton.instance.Experience -= Singleton.instance.ExpInGame;
        Singleton.instance.Lvl -= Singleton.instance.LvlInGame;
        Singleton.instance.MaxValue -= Singleton.instance.MaxValueInGame;
    }
}
