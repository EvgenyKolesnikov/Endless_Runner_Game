using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty_level : MonoBehaviour
{
    public static int High_chance;
    public static int Medium_chance;
    public static int Easy_chance;

    public GameObject[] High_prefabs;
    public GameObject[] Medium_prefabs;
    public GameObject[] Easy_prefabs;


    public int difficult_level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(difficult_level)
        {
            case 1:
                High_chance = 0;
                Medium_chance = 0;
                Easy_chance = 100;
                break;
            case 2:
                High_chance = 0;
                Medium_chance = 35;
                Easy_chance = 65;
                break;
            case 3:
                High_chance = 40;
                Medium_chance = 40;
                Easy_chance = 20;
                break;
        }
        

        GameObject Take_prefab(int difficult_level)
        {
            switch (difficult_level)
            {
                case 1:
                    High_chance = 0;
                    Medium_chance = 25;
                    Easy_chance = 75;
                    break;
                case 2:
                    High_chance = 0;
                    Medium_chance = 35;
                    Easy_chance = 65;
                    break;
                case 3:
                    High_chance = 5;
                    Medium_chance = 45;
                    Easy_chance = 50;
                    break;
                case 4:
                    High_chance = 15;
                    Medium_chance = 40;
                    Easy_chance = 20;
                    break;
            }

            int Rand = Random.Range(0, 100);
            
            if (Rand < Easy_chance)
            {
                return Easy_prefabs[Random.Range(0, Easy_prefabs.Length)];
            }
            else if (Easy_chance < Rand &&  100 - High_chance > Rand)
            {
                return Medium_prefabs[Random.Range(0, Easy_prefabs.Length)];
            }
            else
            {
                return High_prefabs[Random.Range(0, Easy_prefabs.Length)];
            }
        }
        



    }
}
