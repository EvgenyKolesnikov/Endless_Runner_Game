using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Main_ShopScreen : MonoBehaviour
{
    public Button LeftArrow;
    public Button RightArrow;
    public Button SelectButton;
    public Button HomeButton;

    public GameObject Ships;

    private int NumberOfPack = 1;
    private float ShipPosition = 0;
    private float SwipeStep = 20;

    private MainCanvas MainCanvas;
    
    private void Awake()
    {
        MainCanvas = GetComponentInParent<MainCanvas>();
    }
    void Start()
    {
        
       SwipeStep = 20;

        LeftArrow.onClick.AddListener(Swipe_left_wrapper);
        RightArrow.onClick.AddListener(Swipe_Right_wrapper);
        SelectButton.onClick.AddListener(Select_Ship);
        HomeButton.onClick.AddListener(ReturnHome);

        ShipPosition =  PlayerPrefs.GetInt("ShipNumber")  * -SwipeStep;
        NumberOfPack = PlayerPrefs.GetInt("ShipNumber");
        Ships.transform.localPosition = new Vector3(ShipPosition, 0, 0);
        print("number of pack " + NumberOfPack);
    }


    private void OnEnable()
    {
        Ships.SetActive(true);
    }
    private void OnDisable()
    {
        Ships.SetActive(false);
    }

   


    private void ReturnHome()
    {
        MainCanvas.StartScreen.SetActive(true);
        MainCanvas.ShopScreen.SetActive(false);
    }



    private void Swipe_left_wrapper()
    {
        if (NumberOfPack >= 1 )
        {
            StartCoroutine(Swipe_left());
        }
    }
    private void Swipe_Right_wrapper()
    {

        if (NumberOfPack < Ships.transform.childCount-1)
        {
            StartCoroutine(Swipe_Right());
        }
    }

    IEnumerator Swipe_left()
    {
     
        NumberOfPack -= 1;
        ShipPosition += SwipeStep;
        while (true)
        {
            Ships.transform.Translate(new Vector3(1, 0, 0), Space.Self);
            yield return new WaitForSeconds(0.02f);
            if (Ships.transform.localPosition.x >= ShipPosition)
            {
                break;
            }
        }
        yield return null;
    }

    
    IEnumerator Swipe_Right()
    {
        int i = 0;
        NumberOfPack += 1;
        ShipPosition -= SwipeStep;
        while (true)
        {
            Ships.transform.Translate(new Vector3(-1, 0, 0), Space.Self);
            yield return new WaitForSeconds(0.02f);
            if (Ships.transform.localPosition.x <= ShipPosition)
            {
                break;
            }
        }
        yield return null;
    }

    void Select_Ship()
    {
        PlayerPrefs.SetInt("ShipNumber", NumberOfPack);
    }

    
   
}
