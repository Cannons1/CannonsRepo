using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour, IPoints
{
    [SerializeField] WriteVbles mWriteCoins;

    private void Start()
    {
        Singleton.instance.PointsInGame = 0;
    }

    public void GettingPoints(int _points)
    {
        Singleton.instance.PointsInGame += _points;
        mWriteCoins.WritingPoints();//Will write the number of points in a text
    }
}
