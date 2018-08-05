using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinClass{

    public string skinName;
    public string skinDescription;
    public Sprite skinSprite;
    public int skinValue;
    public GameObject skinModel;

    public SkinClass(string _skinName, string _skinDescription, int _skinValue, Sprite _skinSprite, GameObject _skinModel)
    {
        skinName = _skinName;
        skinDescription = _skinDescription;
        skinValue = _skinValue;
        skinSprite = _skinSprite;
        skinModel = _skinModel;
    }
}
