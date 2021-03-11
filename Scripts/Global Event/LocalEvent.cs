using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEvent : MonoBehaviour
{
    /// <summary>
    /// Класс отвечающий за локальные события на сцене. Удаляется после завершения сцены.
    /// </summary>

    public static bool Bloom_status = true;
    public static bool Debug = false;

   
    private void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MobileBloom>().enabled = Convert.ToBoolean(PlayerPrefs.GetInt("Bloom"));

    }

}
