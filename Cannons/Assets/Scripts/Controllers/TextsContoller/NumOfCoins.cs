using UnityEngine;
using UnityEngine.UI;

public class NumOfCoins : MonoBehaviour {
    Text mText;
    private void Start()
    {
        mText = GetComponent<Text>();
        mText.text = Singleton.instance.Coins.ToString();  
    }
}
