using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Main_SettingsScreen : MonoBehaviour
{

    public Button HomeButton;
    public Button MusicButton;
    public Button SFXButton;

    public static Action OnMusicUpdate;
    public static Action OnSFXUpdate;

    private MainCanvas MainCanvas;
    private void Awake()
    {
        MainCanvas = GetComponentInParent<MainCanvas>();
    }
    // Start is called before the first frame update
    void Start()
    {
        HomeButton.onClick.AddListener(ReturnHome);
        MusicButton.onClick.AddListener(Music);
        SFXButton.onClick.AddListener(SFX);
    }

    private void ReturnHome()
    {
        MainCanvas.Settings_screen.SetActive(false);
        MainCanvas.StartScreen.SetActive(true);
    }

    private void SFX()
    {
        OnSFXUpdate?.Invoke();
    }
    private void Music()
    {
        OnMusicUpdate?.Invoke();
    }
}

