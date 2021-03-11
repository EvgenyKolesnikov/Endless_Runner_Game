using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;


public class InputShip : MonoBehaviour
{
    public int life = 3;
    
    private float Immortality_time = 2;
    
    public static bool onCollision = true;
    public  string record;

    public static float timerData = 0.0f;
    public static int Crystals = 0;


    private GetShipFromStore GetShipFromStore;
    private GameObject ship_model;

    public GameObject prefabExplosion;
    public Fade fade;
    private BoxCollider Box_Collider;
    private Rigidbody rigidbody;



    public Action OnHitWithWall { get; set; }


    public delegate void DeadHandler();
    public static event DeadHandler OnDead;

    public delegate void OnLife(int life);
    public static event OnLife Life_checker;

    public delegate void Crystal(int Crystal);
    public static event Crystal OnCrystalPick;


    private void Start()
    {
        Crystals = 0;

        OnCrystalPick?.Invoke(Crystals);
        GetShipFromStore = GetComponent<GetShipFromStore>();
        ship_model = GetShipFromStore.Ships_Moldels[PlayerPrefs.GetInt("ShipNumber")];
       
        
       // PlayerPrefs.DeleteAll();
        rigidbody = GetComponent<Rigidbody>();
        Box_Collider = GetComponent<BoxCollider>();
        timerData = 0.0f;
        Application.targetFrameRate = 60;
        CinemachineShake.Instance.ShakeCamera(2f, 50f);
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
        //GoogleAdMobController.RequestAndLoadInterstitialAd();
    }
    // Update is called once per frame
    private void Update()
    {
        timerData += Time.deltaTime;
    }


    private void Dead()
    {
        record = Calculate_High_Score(timerData);
        AddCrystals(Crystals);
        Vector3 expPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
       
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        fade.isActive = true;
        Destroy(gameObject);

        GameObject explosion = Instantiate(prefabExplosion, expPos, Quaternion.identity);
        OnDead?.Invoke();

        CinemachineShake.Instance.ShakeCamera(0f, 50f);
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
        //GoogleAdMobController.ShowInterstitialAd();
    }


    public static string Calculate_High_Score(float game_score) //TODO fix sort algoritm
    {
        Dictionary<int, string> Place_score = new Dictionary<int,string>
        {
            {1 , "Place_1_score" },
            {2 , "Place_2_score" },
            {3 , "Place_3_score"},
            {4 , "Place_4_score" },
            {5 , "Place_5_score" },
            {6 , "Place_6_score" },
            {7 , "Place_7_score" },
            {8 , "Place_8_score" },
            {9 , "Place_9_score" },
        };
        
        void ReWrite()
        {
            for (int i = Place_score.Count; i > 1; i--)
            {
                PlayerPrefs.SetFloat(Place_score[i], PlayerPrefs.GetFloat(Place_score[i - 1]));
            }
        }

        foreach (var item in Place_score)
        {
            if (game_score > PlayerPrefs.GetFloat(item.Value))
            {
                if (item.Key != 1)
                {
                    ReWrite();
                    PlayerPrefs.SetFloat(item.Value, game_score);
                    return "IT'S A " + item.Key + "PLACE!" + "\n" + "SCORE: " + Mathf.Round(game_score);
                }
                else
                {
                    ReWrite();
                    PlayerPrefs.SetFloat(item.Value, game_score);
                    return "NEW RECORD!" + "\n" + "SCORE: " + Mathf.Round(game_score);
                }
            }
        }
        return "You Lose";
    }



    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Bonus_Life"))
        {
           
            Health Life_score = collision.gameObject.GetComponent<Health>();    
            life += Life_score.Life;
            Life_checker(life);

            collision.gameObject.SetActive(false);
        

        }
        else if (collision.CompareTag("Bonus_SpeedDown"))
        {
           

            print("Speed_down");
        }

        else if (collision.CompareTag("Bonus_Crystal"))
        {
           
            Crystals++;
            OnCrystalPick?.Invoke(Crystals);
        
        }
        else
        {
            if (onCollision)
            {
            
                life--;
                OnHitWithWall?.Invoke();
                Life_checker(life);
                if (life <= 0)
                    {
                        Dead();
                    }
                StartCoroutine(Immortality(2));   
            }

        };
    }

    private void AddCrystals(int Adding_Crystals)
    {
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + Adding_Crystals);
    }

    IEnumerator Immortality(float immortal_time)
    {
        float flick_time = 0.2f;
        int time = Mathf.RoundToInt((immortal_time / (flick_time * 2)));
       
        Box_Collider.enabled = false;
        for (int i = 0; i < time; i++)
        {
            ship_model.active = false;
            yield return new WaitForSeconds(flick_time);
            ship_model.active = true;
            yield return new WaitForSeconds(flick_time);
        }
        Box_Collider.enabled = true;
    }
}


