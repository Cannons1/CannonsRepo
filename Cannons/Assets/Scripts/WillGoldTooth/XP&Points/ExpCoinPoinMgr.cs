using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCoinPoinMgr : MonoBehaviour
{
    public int Saved { get { return saved; } set { saved = value; } }
    public bool SavedLastExp { get { return saveLastExp; } set { saveLastExp = value; } }

    int saved;
    bool saveLastExp;
    IGLevelManager igLevelManager;


    private void Start()
    {
        igLevelManager = (IGLevelManager)FindObjectOfType(typeof(IGLevelManager));
        saveLastExp = false;
        Singleton.instance.ExpInGame = 0;
        Singleton.instance.LvlInGame = 0;
        Singleton.instance.MaxValueInGame = 0;
        saved = Singleton.instance.Experience;
        Singleton.instance.PointsInGame = 0;
        Singleton.instance.CoinsInGame = 0;
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

    public void MinusCoinsInGame()
    {
        Singleton.instance.Coins -= Singleton.instance.CoinsInGame;
    }

    public void Mgr() {
        if (SavedLastExp)
        {
            Singleton.instance.Experience = Saved;
            MinusLvl();
            Debug.Log(Saved + " Esto es lo que cargaré");
        }
        else
        {
            MinusExperienceInGame();//If the user press menu in a middle of a game, the experience wont count
        }
        MinusCoinsInGame();//If the user press menu in a middle of a game, the coins wont count
        igLevelManager.MenuButton();//Returns to menu
    }

}
