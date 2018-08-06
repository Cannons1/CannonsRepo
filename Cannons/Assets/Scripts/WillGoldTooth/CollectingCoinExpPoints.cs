﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectingCoinExpPoints : MonoBehaviour
{
    private int coin = 1;
    private int point = 1;
    private int exp;
    private bool rouletteCoin;

    [SerializeField] AudioItems mAudioItems;

    private void Start()
    {
        mAudioItems = FindObjectOfType<AudioItems>();
        rouletteCoin = false; //Always starts in false, unless you get the RouletteCoin;

        if (Singleton.instance.ExpBoost == 0)
            exp = Random.Range(1, 4);
        else
            exp = Random.Range(4, 7);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisioned = other.gameObject;

        if (collisioned.GetComponent<ICoins>() != null)
        {
            mAudioItems.AudioCoin();
            ICoins iCoins;
            iCoins = collisioned.GetComponent<ICoins>();
            iCoins.CoinsCollected(coin);
        }

        if (collisioned.GetComponent<IRouletteCoin>() != null)
        {
            mAudioItems.AudioRouletteCoin();
            rouletteCoin = true;
            IRouletteCoin iRouletteCoin;
            iRouletteCoin = collisioned.GetComponent<IRouletteCoin>();
            iRouletteCoin.RoulletteCoinCollected(rouletteCoin);
        }

        if (collisioned.GetComponent<IExperience>() != null)
        {
            IExperience iExperience;
            iExperience = collisioned.GetComponent<IExperience>();
            iExperience.EarningExperience(exp);
        }

        if (collisioned.GetComponent<IPoints>() != null)
        {
            IPoints iPoints;
            iPoints = collisioned.GetComponent<IPoints>();
            iPoints.GettingPoints(point);
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if (collisioned.GetComponent<IExperience>() != null)
        {
            exp = Random.Range(0.05f, 0.15f);
            IExperience iExperience;
            iExperience = collisioned.GetComponent<IExperience>();
            iExperience.EarningExperience(exp);
        }

        if (collisioned.GetComponent<IPoints>() != null)
        {
            IPoints iPoints;
            iPoints = collisioned.GetComponent<IPoints>();
            iPoints.GettingPoints(point);
        }
    }*/
}
