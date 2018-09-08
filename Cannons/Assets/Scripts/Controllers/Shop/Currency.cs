using UnityEngine;

public class Currency : MonoBehaviour {

	void Start () {
        if (PlayerPrefs.HasKey("Coins"))
            Singleton.instance.Coins = PlayerPrefs.GetInt("Coins");
	}
}
