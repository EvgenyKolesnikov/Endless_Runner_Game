using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class FPSManagerScript : MonoBehaviour
{
    public bool showFpsMeter = true;
    public Text fpsMeter;
    private float deltaTime;

    // Update is called once per frame
    private void Update()
    {
        if (showFpsMeter)
        {
            fpsMeter.gameObject.SetActive(true);
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsMeter.text = string.Format("{0:0.} FPS", fps);
        }
        else
        {
            fpsMeter.gameObject.SetActive(false);
        }
    }
}
