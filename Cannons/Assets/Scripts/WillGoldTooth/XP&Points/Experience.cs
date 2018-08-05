using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour, IExperience
{
    [SerializeField] WriteVbles mWriteExp;

    private void Start()
    {
        Singleton.instance.expInGame = 0f;
        Singleton.instance.lvlInGame = 0;
        Singleton.instance.maxValueInGame = 0f;
    }

    public void EarningExperience(float _Experience)
    {
        Singleton.instance.experience += _Experience;
        Singleton.instance.expInGame += _Experience;
        Debug.Log(Singleton.instance.experience + "xp");
        mWriteExp.WriteExp();
    }

    public void MinusExperienceInGame()
    {
        Singleton.instance.experience -= Singleton.instance.expInGame;
        Singleton.instance.lvl -= Singleton.instance.lvlInGame;
        Singleton.instance.maxValue -= Singleton.instance.maxValueInGame;
    }
}
