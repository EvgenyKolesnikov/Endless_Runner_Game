using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class UI_Debug : MonoBehaviour
{
    private static UI_Debug instance;

    public Slider Speed_bar;
    public Slider Difficult_bar;
    public Slider Side_speed_bar;
    public Slider Rotate_speed_bar;
    public Slider Angle_bar;
    public Toggle Collision_toggle;

    public Text Speed_bar_text;
    public Text Difficult_text;
    public Text Side_speed_text;
    public Text Rotate_speed_text;
    public Text Angle_text;

    public static float _Speed_bar;
    public static float _Difficult_bar;
    public static bool Collision;
    public static float _Side_speed;
    public static float _Rotate_speed;
    public static float _Angle;

    public delegate void MovementHandler(float Movement);
    public static event MovementHandler OnSpeedChange;
    public static event MovementHandler OnRotateSpeedChange;
    public static event MovementHandler OnSideSpeedChange;
    public static event MovementHandler OnAngleChange;
    public static event MovementHandler OnDifficultChange;




    private void Start()
    {


        Speed_bar.onValueChanged.AddListener(Speedbar_Update);
        Rotate_speed_bar.onValueChanged.AddListener(RotateSpeedbar_Update);
        Side_speed_bar.onValueChanged.AddListener(SideSpeedbar_Update);
        Angle_bar.onValueChanged.AddListener(AngleBar_Update);
        Difficult_bar.onValueChanged.AddListener(Difficulbar_Update);
        Collision_toggle.onValueChanged.AddListener(Collision_Update);

        Speedbar_Update(PlayerPrefs.GetFloat("Speed"));
        RotateSpeedbar_Update(PlayerPrefs.GetFloat("RotateSpeed"));
        SideSpeedbar_Update(PlayerPrefs.GetFloat("SideSpeed"));
        AngleBar_Update(PlayerPrefs.GetFloat("Angle"));
        Difficulbar_Update(PlayerPrefs.GetFloat("Difficult"));
        Collision = Collision_toggle.isOn;
    }


    private void Speedbar_Update(float speed)
    {
        _Speed_bar = speed;
        Speed_bar_text.text = speed.ToString();
        PlayerPrefs.SetFloat("Speed", speed);
        OnSpeedChange?.Invoke(speed);
    }

    private void  RotateSpeedbar_Update(float rotateSpeed)
    {
        _Rotate_speed = rotateSpeed;
        Rotate_speed_text.text = rotateSpeed.ToString();
        PlayerPrefs.SetFloat("RotateSpeed", rotateSpeed);
        OnRotateSpeedChange?.Invoke(rotateSpeed);
    }

    private void SideSpeedbar_Update(float SideSpeed)
    {
        _Side_speed = SideSpeed;
        Side_speed_text.text = SideSpeed.ToString();
        PlayerPrefs.SetFloat("SideSpeed", SideSpeed);
        OnSideSpeedChange?.Invoke(SideSpeed);
    }

    private void AngleBar_Update(float Angle)
    {
        _Angle = Angle;
        Angle_text.text = Angle.ToString();
        PlayerPrefs.SetFloat("Angle", Angle);
        OnAngleChange?.Invoke(Angle);
    }
    private void Difficulbar_Update(float Difficult)
    {
        _Difficult_bar = Difficult;
        Difficult_text.text = Difficult.ToString();
        PlayerPrefs.SetFloat("Difficult", Difficult);
        OnDifficultChange?.Invoke(Difficult);
    }
    private void Collision_Update(bool Collision_status)
    {
       
        InputShip.onCollision = Collision_status;
    }

}
