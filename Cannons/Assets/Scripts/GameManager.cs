using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Material Handv
    {
        get
        {
            return handv;
        }
    }

    public Material Rotating
    {
        get
        {
            return rotating;
        }
    }

    public Material StaticCannon
    {
        get
        {
            return staticCannon;
        }
    }

    public float adsProbability = 100;
    public float maxProbability = 100;

    public int currentSkin = 0;
    public int skinAvailability = 1;

    [SerializeField] Material handv, rotating, staticCannon;
    public CannonParent lastCannon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);       

        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            currentSkin = PlayerPrefs.GetInt("CurrentSkin");
            skinAvailability = PlayerPrefs.GetInt("SkinAvailibility");
        }
        else
        {
            if(skinAvailability == 0) skinAvailability = 1;
            Save();
        }

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkin", currentSkin);
        PlayerPrefs.SetInt("SkinAvailibility", skinAvailability);
    }
}
