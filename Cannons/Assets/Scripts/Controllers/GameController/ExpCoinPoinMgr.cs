using UnityEngine;

public class ExpCoinPoinMgr : MonoBehaviour
{
    private void Start()
    {
        Singleton.instance.CoinsInGame = 0;
    }

    public void MinusCoinsInGame()
    {
        Singleton.instance.Coins -= Singleton.instance.CoinsInGame;
    }

    public void Mgr() {
        MinusCoinsInGame();//If the user press menu in a middle of a game, the coins wont count
    }

}
