using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms_Moving : MonoBehaviour
{
    public float speed;
    public static float speed_coef = 1f;


    public GameObject Player;
    public GameObject Canvas_Debug;

    void Start()
    {
       
       Speed_Wrapper();
        speed_coef = 1f;
    }

    private void OnEnable()
    {
        UI_Debug.OnSpeedChange += Speed_Update;
    }

    private void OnDisable()
    {
        UI_Debug.OnSpeedChange -= Speed_Update;
    }


    void FixedUpdate()
    {
        speed_coef = (speed_coef >= 1) ? (speed_coef + Time.deltaTime * 2 / 100) : 1.0f;
        
        if (Player!= null)
        {
            foreach(GameObject stage in ObjectPool.Stages_Queue)
            {
                stage.transform.Translate(0, 0, -speed * speed_coef, Space.World);
            }    
        }                                                                                                                          
    }

    private void Speed_Update(float speed_UI_Debug)
    {
        speed = speed_UI_Debug;
    }

    private void Speed_Wrapper()
    {
        speed = PlayerPrefs.GetFloat("Speed");                  //Берем значение скорости из памяти
        if (Canvas_Debug.activeInHierarchy)                    //Если дебаг активен - переписываем значение скорости
        {                                                      //Если нет, значения берутся из инспектора   
            Speed_Update(UI_Debug._Speed_bar);
        }
        PlayerPrefs.SetFloat("Speed", speed);                 //Записываем скорость в память
    }
}
