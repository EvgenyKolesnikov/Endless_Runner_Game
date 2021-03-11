using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Way : MonoBehaviour
{
    public GameObject[] Way_coins;
    private int chance;

    private void OnEnable()
    {
        chance = Random.Range(0, Way_coins.Length);
        print(chance);
        for (int i = 0; i < Way_coins.Length; i++)
        {
            if (i != chance)
            {
                Way_coins[i].SetActive(false);
            }
            else
            {
                Way_coins[i].SetActive(true);
            }
        }
    }
}
