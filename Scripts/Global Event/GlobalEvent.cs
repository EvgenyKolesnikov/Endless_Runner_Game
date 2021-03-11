using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Audio;

public class GlobalEvent : MonoBehaviour
{
    public static bool Music = true;
    public static bool SFX = true;
    public static bool Bloom_status = true;

    public delegate void OnMusicUpdate(bool state);
    public static event OnMusicUpdate MusicUpdate_Handler;

    public delegate void OnSFXUpdate(bool state);
    public static event OnSFXUpdate SFX_Handler;

    private void Awake()
    {
      //  DontDestroyOnLoad(transform.gameObject);
    }
    

    private void OnEnable()
    {
        Pause_Screen.OnMusicUpdate += MusicUpdate;
        Main_SettingsScreen.OnMusicUpdate += MusicUpdate;

        Pause_Screen.OnSFXUpdate += SFXUpdate;
        Main_SettingsScreen.OnSFXUpdate += SFXUpdate;
    }
    private void OnDisable()
    {
        Pause_Screen.OnMusicUpdate -= MusicUpdate;
        Main_SettingsScreen.OnMusicUpdate -= MusicUpdate;

        Pause_Screen.OnSFXUpdate -= SFXUpdate;
        Main_SettingsScreen.OnSFXUpdate -= SFXUpdate;
    }


    public static void SFXUpdate()
    {
        SFX_Handler?.Invoke(SFX);
    }

    private void MusicUpdate()
    {
        MusicUpdate_Handler?.Invoke(Music);
    }
    
}

