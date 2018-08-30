using UnityEngine;

public class Currency : MonoBehaviour {

	void Start () {
        if (PlayerPrefs.HasKey("Coins"))
            Singleton.instance.Coins = PlayerPrefs.GetInt("Coins");
        if (PlayerPrefs.HasKey("Exp"))
            Singleton.instance.Experience = PlayerPrefs.GetInt("Exp");
        if (PlayerPrefs.HasKey("Lvl"))
            Singleton.instance.Lvl = PlayerPrefs.GetInt("Lvl");
	}

    public void ResetCurr() {
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("Exp");
        PlayerPrefs.DeleteKey("Lvl");
    }
}
