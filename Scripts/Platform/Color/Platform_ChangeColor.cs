using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Platform_ChangeColor : MonoBehaviour
{
    public Material material_floor;
    public Material material_skybox;

    private Color start_color_floor;
    private Color start_color_skybox;


    private Color32 target_color_floor; //grey
    private Color32 target_color_skybox;

    private Color32 Current_color_floor;
    private Color32 Current_color_skybox;

    Dictionary<int, Color> floor_colors = new Dictionary<int, Color>
    {
        {0, new Color32(74,110,185,255)}, 
        {1, new Color32(176,70,90,255)}, 
        {2, new Color32(130,170,110,255)},
        {3, new Color32(130,70,245,255)}
    };

    Dictionary<int, Color> skybox_colors = new Dictionary<int, Color>
    {
        {0, new Color32(90,180,245,255)},
        {1, new Color32(240,90,90,255)},
        {2, new Color32(200,240,90,255)},
        {3, new Color32(210,90,240,255)}
    };

    void Start()
    {
         target_color_floor = new Color32(120, 120, 120, 255);
         target_color_skybox = new Color32(25, 25, 25, 255);

        int Random_Color = Random.Range(1, floor_colors.Count);

        start_color_floor = floor_colors.FirstOrDefault(x => x.Key == Random_Color).Value;
        start_color_skybox = skybox_colors.FirstOrDefault(x => x.Key == Random_Color).Value;

       InvokeRepeating("wrapper_LerpColor", 0, 1f);
    }


    void wrapper_LerpColor()
    {
        Current_color_floor = LerpColor(start_color_floor, target_color_floor, 2f);
        Current_color_skybox = LerpColor(start_color_skybox, target_color_skybox, 2f);

        material_floor.SetColor("_Color1_T", Current_color_floor);
        material_skybox.SetColor("_SkyGradientTop", Current_color_skybox);
    }


    //переход от входящего цвета к целевому в зависимости от скорости.
    //возвращает цвет в зависимости от коэфициента скорости
    //max_coef - при достижении этого значения, цвет полностью перельется в целевой.
    //input_RGBA - начальный цвет, должен определяться в методе Start()
    //target_RGBA - целевой цвет, определяется в Start()
    Color32 LerpColor(Color32 input_RGBA,Color32 target_RGBA,float max_coef)
    {
        if(Platforms_Moving.speed_coef < max_coef)
        {
            float step = (max_coef - 1) * 100;
            float current_step = Platforms_Moving.speed_coef * 100 - 100;

            float delta_R = (target_RGBA.r - input_RGBA.r) / step;
            float delta_G = (target_RGBA.g - input_RGBA.g) / step;
            float delta_B = (target_RGBA.b - input_RGBA.b) / step;
            float delta_A = (target_RGBA.a - input_RGBA.a) / step;

            byte r = (byte)(input_RGBA.r + delta_R * current_step);
            byte g = (byte)(input_RGBA.g + delta_G * current_step);
            byte b = (byte)(input_RGBA.b + delta_B * current_step);
            byte a = (byte)(input_RGBA.a + delta_A * current_step);
     
            return new Color32(r, g, b, a);
        }
        return target_RGBA;
    }
}
