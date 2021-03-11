using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_HighScoreScreen : MonoBehaviour
{

    public Button HomeButton;
    // Start is called before the first frame update

    private MainCanvas MainCanvas;
    private void Awake()
    {
        MainCanvas = GetComponentInParent<MainCanvas>();
    }

    void Start()
    {
        HomeButton.onClick.AddListener(ReturnHome);
    }

   
    private void ReturnHome()
    {
        MainCanvas.HighScoreScreen.SetActive(false);
        MainCanvas.StartScreen.SetActive(true);
    }



}
