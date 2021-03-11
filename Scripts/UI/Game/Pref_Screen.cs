using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pref_Screen : MonoBehaviour
{
    private GameCanvas MainCanvas;
    public  GameObject Canvas_Debug;

    public Button BackButton;
    public Toggle DebugButton;
    public Toggle BloomButton;

   
    
    private void OnEnable()
    {
        BackButton.onClick.AddListener(Start_PauseScreen);
        BloomButton.onValueChanged.AddListener(Bloom);
        DebugButton.onValueChanged.AddListener(Debug);
    }


    void Start()
    {
        MainCanvas = GetComponentInParent<GameCanvas>();
        

        BloomButton.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Bloom"));
        DebugButton.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Debug"));

    }

    // переключение эффекта Bloom
    private void Bloom(bool status)
    {
        PlayerPrefs.SetInt("Bloom", Convert.ToInt32(status));
        PlayerPrefs.Save();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MobileBloom>().enabled = status;
    }

    // переключение Debug
    private void Debug(bool status)
    {
        PlayerPrefs.SetInt("Debug", Convert.ToInt32(status));
        PlayerPrefs.Save();
        Canvas_Debug.SetActive(status);
        
    }

    private void Start_PauseScreen()
    {
        MainCanvas.PauseScreen.SetActive(true);
        MainCanvas.PrefScreen.SetActive(false);
    }
}
