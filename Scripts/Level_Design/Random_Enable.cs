using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Enable : MonoBehaviour
{
    public GameObject[] objects;
    public int chance;

    private void OnEnable()
    {
        foreach(GameObject obj in objects )
        {
            obj.SetActive(isOn(chance));
        }
        
    }

    bool isOn(int chance)
    {
        if (chance > Random.Range(0, 100))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
