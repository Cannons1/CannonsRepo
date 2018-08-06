using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCoinPoinMgr : MonoBehaviour
{
    public int Saved { get { return saved; } set { saved = value; } }
    public bool SavedLastExp { get { return saveLastExp; } set { saveLastExp = value; } }

    int saved;
    bool saveLastExp;


    private void Start()
    {
        saveLastExp = false;
        Singleton.instance.ExpInGame = 0;
        Singleton.instance.LvlInGame = 0;
        Singleton.instance.MaxValueInGame = 0;
        saved = Singleton.instance.Experience;
        Singleton.instance.PointsInGame = 0;
        Singleton.instance.CoinsInGame = 0;
        Debug.Log(saved + "saved");
    }
    /*public void EarningExperience(int _Experience)
    {
        
        mWriteVbles.experience.value = Singleton.instance.Experience;

        if (Singleton.instance.ExpInGame >= mWriteVbles.experience.maxValue)
        {
            //Singleton.instance.Lvl++; 
            Singleton.instance.LvlInGame++;//Lvl up
            mWriteVbles.experience.maxValue += percentaje;//Ten percent more each time you lvl up
            // Singleton.instance.MaxValue += percentaje; 
            Singleton.instance.MaxValueInGame += percentaje;
            mWriteVbles.experience.value = 0;
            //Singleton.instance.ExpInGame = 0; Singleton.instance.Experience = 0;
            mWriteVbles.WriteCharacterLvl();
        }
    }*/
    public void MinusExperienceInGame()
    {
         Singleton.instance.Experience -= Singleton.instance.ExpInGame;
         Singleton.instance.Lvl -= Singleton.instance.LvlInGame;
         Singleton.instance.MaxValue -= Singleton.instance.MaxValueInGame;
        /*Singleton.instance.ExpInGame = 0;
        Singleton.instance.LvlInGame = 0;
        Singleton.instance.MaxValueInGame = 0;*/
    }

    public void MinusLvl() {
        Singleton.instance.Lvl -= Singleton.instance.LvlInGame;
        Singleton.instance.MaxValue -= Singleton.instance.MaxValueInGame;
    }


}
