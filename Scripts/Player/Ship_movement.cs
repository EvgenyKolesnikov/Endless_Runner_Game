using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ship_movement : MonoBehaviour
{
    public float angle = 15.0f;
    public float rotateSpeed = 50.0f;
    public float side_speed = 20.0f;
    public float adding_rotate = 0.5f;

    private float Screen_Center = Screen.width / 2;
    private GameObject Canvas_Debug;
    private Rigidbody rigidbody;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Canvas_Debug = GameObject.FindGameObjectWithTag("Debug");
        //RotateSpeed_Wrapper();
        //SideSpeed_Wrapper();
        //Angle_Wrapper();
    }

    private void OnEnable()
    {
       UI_Debug.OnRotateSpeedChange += RotateSpeed_Update;
        UI_Debug.OnSideSpeedChange += SideSpeed_Update;
        UI_Debug.OnAngleChange += Angle_Update;
    }

    private void OnDisable()
    {
        UI_Debug.OnRotateSpeedChange -= RotateSpeed_Update;
        UI_Debug.OnSideSpeedChange -= SideSpeed_Update;
        UI_Debug.OnAngleChange -= Angle_Update;
    }

    // Update is called once per frame
    void Update()
    {

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Debug.Log("touch UI");
                    return;
                }
                float horizontal_move = (touch.position.x - Screen_Center) / Screen_Center;

                //поворота корабля
                //дополнительное докручивание, при достижении максимального угла
                float angleZ = transform.rotation.eulerAngles.z;
                angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;
                if (angleZ < -angle && horizontal_move > 0)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, -angle - 30), adding_rotate);
                }
                else if (angleZ > angle && horizontal_move < 0)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle + 30), adding_rotate);
                }
                else
                {
                    transform.RotateAround(transform.position, Vector3.forward, horizontal_move * Time.deltaTime * -rotateSpeed);
                }

                // Боковое движение корабля
                float sideforce = 500.0f * horizontal_move * Time.deltaTime * side_speed;
                rigidbody.AddRelativeForce(sideforce, 0f, 0f);  // Перемещаем объект в мировом пространстве по оси X
        }
        else
            {
                //возвращение в исходную позицию
                float z_Angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 0, 1f);
                transform.eulerAngles = new Vector3(0, 0, z_Angle);
            }
    }


    private void RotateSpeed_Update(float UI_RotateSpeed_Debug)
    {
        rotateSpeed = UI_RotateSpeed_Debug;
    }

    private void SideSpeed_Update(float UI_SideSpeed_Debug)
    {
        side_speed = UI_SideSpeed_Debug;
    }

    private void Angle_Update(float UI_Angle_Debug)
    {
        angle = UI_Angle_Debug;
    }

    private void RotateSpeed_Wrapper()
    {
        rotateSpeed = PlayerPrefs.GetFloat("RotateSpeed");
        if (Canvas_Debug.activeInHierarchy)
        {
            RotateSpeed_Update(UI_Debug._Rotate_speed);
        }
        PlayerPrefs.SetFloat("RotateSpeed", rotateSpeed);
    }

    private void SideSpeed_Wrapper()
    {
        side_speed = PlayerPrefs.GetFloat("SideSpeed");
        if (Canvas_Debug.activeInHierarchy)
        {
            RotateSpeed_Update(UI_Debug._Side_speed);
        }
        PlayerPrefs.SetFloat("SideSpeed", side_speed);
    }
    private void Angle_Wrapper()
    {
        angle = PlayerPrefs.GetFloat("Angle");
        if (Canvas_Debug.activeInHierarchy)
        {
            RotateSpeed_Update(UI_Debug._Angle);
        }
        PlayerPrefs.SetFloat("Angle", angle);
    }

}
