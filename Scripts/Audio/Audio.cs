using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioMixer Audiomixer;
    private static Audio instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    private void OnEnable()
    {
        GlobalEvent.MusicUpdate_Handler += MusicToggle;
        GlobalEvent.SFX_Handler += SFXToggle;
    }
    private void OnDisable()
    {
        GlobalEvent.MusicUpdate_Handler -= MusicToggle;
        GlobalEvent.SFX_Handler -= SFXToggle;
    }


 

    private void MusicToggle(bool state)
    {
        if(state)
        {
            GlobalEvent.Music = false;
            Audiomixer.SetFloat("MusicVol", -80); 
        }
        else
        {
            GlobalEvent.Music = true;
            Audiomixer.SetFloat("MusicVol", 0); 
        }
    }

    private void SFXToggle(bool state)
    {
        
        if (state)
        {
            GlobalEvent.SFX = false;
            Audiomixer.SetFloat("SFXVol", -80); // Уровень громкости канала музыки 0 Дб
        }
        else
        {
            GlobalEvent.SFX = true;
            Audiomixer.SetFloat("SFXVol", 0); // Уровень громкости канала музыки -80 Дб
        }

    }


}