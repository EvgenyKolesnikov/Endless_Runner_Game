using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Audio : MonoBehaviour
{
    public AudioSource HitWithWall_Sound;
    public AudioSource bonus_sound;
    public AudioSource bonus_Blackhole_sound;
    public AudioSource bonus_Death_sound;
    public AudioSource bonus_Freeze_sound;
    public AudioSource bonus_Health_sound;
    public AudioSource bonus_Random_sound;
    public AudioSource bonus_SpeedDown_sound;
    public AudioSource bonus_SpeedUp_sound;
    public AudioSource bonus_Stars_sound;

    private InputShip _inputship;

    private void Awake()
    {
        _inputship = GetComponent<InputShip>();
    }


    private void OnEnable()
    {
        _inputship.OnHitWithWall += Play_HitSound;
    }

    private void OnDisable()
    {
        _inputship.OnHitWithWall -= Play_HitSound;
    }


    private void Play_HitSound()
    {
        HitWithWall_Sound.PlayOneShot(HitWithWall_Sound.clip);
    }


}
