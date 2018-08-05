using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour, IPoints
{
    [SerializeField] WriteVbles mWriteCoins;

    private void Start()
    {
        Singleton.instance.pointsInGame = 0;
    }

    public void GettingPoints(int _points)
    {
        Singleton.instance.pointsInGame += _points;
        mWriteCoins.WritingPoints();//Will write the number of points in a text
    }
}
