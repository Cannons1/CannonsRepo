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
    public AnimationClip[] animations = new AnimationClip[3];

    public SkinClass(string _skinName, string _skinDescription, int _skinValue, Sprite _skinSprite, GameObject _skinModel, AnimationClip[] _animations)
    {
        skinName = _skinName;
        skinDescription = _skinDescription;
        skinValue = _skinValue;
        skinSprite = _skinSprite;
        skinModel = _skinModel;
        animations = _animations;
    }
}
