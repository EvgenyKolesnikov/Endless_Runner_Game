using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    public Text Place_Best;
    public Text Place_2;
    public Text Place_3;
    public Text Place_4;
    public Text Place_5;
    public Text Place_6;
    public Text Place_7; 
    public Text Place_8;
    public Text Place_9;


    void Start()
    {
        Refresh();
    }


 

    public void Refresh()
    {
        Place_Best.text = Mathf.Round(PlayerPrefs.GetFloat("Place_1_score", 0)).ToString();
        Place_2.text = Mathf.Round(PlayerPrefs.GetFloat("Place_2_score", 0)).ToString();
        Place_3.text = Mathf.Round(PlayerPrefs.GetFloat("Place_3_score", 0)).ToString();
        Place_4.text = Mathf.Round(PlayerPrefs.GetFloat("Place_4_score", 0)).ToString();
        Place_5.text = Mathf.Round(PlayerPrefs.GetFloat("Place_5_score", 0)).ToString();
        Place_6.text = Mathf.Round(PlayerPrefs.GetFloat("Place_6_score", 0)).ToString();
        Place_7.text = Mathf.Round(PlayerPrefs.GetFloat("Place_7_score", 0)).ToString();
        Place_8.text = Mathf.Round(PlayerPrefs.GetFloat("Place_8_score", 0)).ToString();
        Place_9.text = Mathf.Round(PlayerPrefs.GetFloat("Place_9_score", 0)).ToString();
    }
    
}
