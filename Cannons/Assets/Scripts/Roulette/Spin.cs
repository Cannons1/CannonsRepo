using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    float rouletteRotationSpeed = 3f;
    int spinningDir = -1;
    int i = 0;

    private void Start()
    {
        StartCoroutine(SpinRoulette());
    }

    IEnumerator SpinRoulette()
    {
        while (Time.time < 20 && rouletteRotationSpeed >= 0)
        {
            transform.Rotate(0, 0, rouletteRotationSpeed * Time.time * spinningDir);
            //i++;
            rouletteRotationSpeed -= 0.01f;
            yield return null;
        }
        
    }
}
