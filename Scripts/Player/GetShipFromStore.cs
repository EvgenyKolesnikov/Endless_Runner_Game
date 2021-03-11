using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShipFromStore : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Ships_Moldels;
    void Start()
    {
        Instantiate(Ships_Moldels[PlayerPrefs.GetInt("ShipNumber")],transform).SetActive(true);
    }


}
