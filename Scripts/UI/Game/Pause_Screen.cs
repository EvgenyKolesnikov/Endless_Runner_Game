using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Pause_Screen : MonoBehaviour
{
    public Button ResumeButton;
    public Button MusicButton;
    public Button SFXButton;
    public Button MainMenuButton;
    public Button PrefButton;

    private GameCanvas MainCanvas;
    

    public static Action OnMusicUpdate;
    public static Action OnSFXUpdate;

    // Start is called before the first frame update

    private void Awake()
    {
        MainCanvas = GetComponentInParent<GameCanvas>();
    }
    void Start()
    {
        PrefButton.onClick.AddListener(Start_PrefScreen);
        ResumeButton.onClick.AddListener(Resume);
        MainMenuButton.onClick.AddListener(Main_menu);
        MusicButton.onClick.AddListener(Music);
        SFXButton.onClick.AddListener(SFX);
    }

   
 
    private void OnEnable()
    {
        MainCanvas.PlayScreen.SetActive(false);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        
    }

    private void Main_menu()
    {
        SceneManager.LoadScene("Main_menu");
        SceneManager.UnloadSceneAsync("level");
    }
    private void Start_PrefScreen()
    {
        MainCanvas.PrefScreen.SetActive(true);
        MainCanvas.PauseScreen.SetActive(false);
        
    }
    private void Resume()
    {
        MainCanvas.PauseScreen.SetActive(false);
        MainCanvas.PlayScreen.SetActive(true);
    }

    private void Music() {
        
        OnMusicUpdate?.Invoke();
    }
    private void SFX()
    {
        OnSFXUpdate?.Invoke();
    }

}
