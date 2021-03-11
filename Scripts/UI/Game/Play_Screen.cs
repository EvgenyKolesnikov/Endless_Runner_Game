using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Play_Screen : MonoBehaviour
{
    public InputShip Player;

    public Button PauseButton;
    
    public Text Life_score;
    public Text Game_score;
    public Text Speed_Coefficient;
    public Text New_record;
    public Text Crystal;
    public Text Crytal_Earned;

    private GameCanvas GameCanvas;
    public GameObject Canvas_Debug;
    private void Awake()
    {
        GameCanvas = GetComponentInParent<GameCanvas>();
        Canvas_Debug.SetActive(Convert.ToBoolean(PlayerPrefs.GetInt("Debug")));
    }
    private void Start()
    {
        
        
        PauseButton.onClick.AddListener(Pause);


        New_record.gameObject.SetActive(false);
        Crytal_Earned.gameObject.SetActive(false);

        Life_score.text = Player.life.ToString();
        Crystal_Update(InputShip.Crystals);

        
        InvokeRepeating("GameScore_Update", 0f, 1f);
        InvokeRepeating("SpeedCoef_Update", 0f, .5f);
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;
   

        InputShip.Life_checker += Life_Update;
        InputShip.OnCrystalPick += Crystal_Update;
        InputShip.OnDead += Record_Update;
        InputShip.OnDead += Crystal_Earned_Update;
    }

    private void OnDisable()
    {
        Time.timeScale = 0f;

        InputShip.Life_checker -= Life_Update;
        InputShip.OnCrystalPick -= Crystal_Update;
        InputShip.OnDead -= Record_Update;
        InputShip.OnDead -= Crystal_Earned_Update;
    }


   private void Record_Update()
    {
        New_record.gameObject.SetActive(true);
        New_record.text = Player.record.ToString();
    }

    private void Crystal_Earned_Update()
    {
        Crytal_Earned.gameObject.SetActive(true);
        Crytal_Earned.text = "Crystal Earned: " + InputShip.Crystals.ToString();
    }

    private void SpeedCoef_Update()
    {
        Speed_Coefficient.text = "Speed x " + Platforms_Moving.speed_coef.ToString("#.##");
    }

    private void GameScore_Update()
    {
        Game_score.text = InputShip.timerData.ToString("#");
    }

    private void Life_Update(int life)
    {
        Life_score.text = life.ToString();
    }

    private void Crystal_Update(int Crystals)
    {
        Crystal.text = Crystals.ToString();
    }

    

    private void Pause()
    {
        GameCanvas.PauseScreen.SetActive(true);
        GameCanvas.PlayScreen.SetActive(false);
    }




    
}
