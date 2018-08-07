using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    public static Memento memento = null;

    private void Awake()
    {
        if (memento == null)
        {
            memento = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string Load(string key)
    {
        string value = PlayerPrefs.GetString(key);
        return value;
    }

    public void Save(string[] toSave)
    {
        PlayerPrefs.SetString(toSave[0], toSave[1]);
    }

    void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
