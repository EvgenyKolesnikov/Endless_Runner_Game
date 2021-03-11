using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float fadeTimeStart = 3.0f;
    public Image fadeScrean;
    private Color colorFade;
    private float fadeTime;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        colorFade = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        fadeTime = fadeTimeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            fadeTime -= Time.deltaTime;
            if (fadeTime < 0.0f)
            {
                fadeTime = 0.0f;
                Restart();
            }
            //Debug.Log((1.0f - fadeTime / fadeTimeStart).ToString());
            //float resFade = fadeTime / fadeTimeStart;
           
            fadeScrean.color= new Color(0.0f, 0.0f, 0.0f, (1.0f - fadeTime / fadeTimeStart));
        }

    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
