using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GraphicSettings : MonoBehaviour
{
    public Dropdown dropDown;

    void Start()
    {
        //dropDown.ClearOptions();
        dropDown.AddOptions(QualitySettings.names.ToList());
        if (PlayerPrefs.HasKey("Quality"))
        {
            dropDown.value = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }
        else
        {
            dropDown.value = QualitySettings.GetQualityLevel();
        }

        void SetQuality()
        {
            QualitySettings.SetQualityLevel(dropDown.value);
            PlayerPrefs.SetInt("Quality", dropDown.value);
        }
    }
}
