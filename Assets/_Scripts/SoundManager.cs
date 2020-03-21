using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class SoundManager : Singleton<SoundManager>
{
    [Header("SFX")]
    public StudioEventEmitter attack;
    public StudioEventEmitter damage;
    public StudioEventEmitter dash1;
    public StudioEventEmitter dash2;
    public StudioEventEmitter dying;
    public StudioEventEmitter explosion;
    public StudioEventEmitter jump;
    public StudioEventEmitter jumpBass;
    public StudioEventEmitter shoot1;
    public StudioEventEmitter shoot2;
    public StudioEventEmitter shoot3;
    public StudioEventEmitter swoosh;

    [Header("Soundtrack")]
    public StudioEventEmitter gameplayMusic;

    private void Start()
    {
        PlayGameplayMusic();
        DontDestroyOnLoad(this.gameObject);

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetMusicMood(0);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SetMusicMood(1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetMusicMood(2);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetMusicMood(3);
        }
    }

    public void PlayAttack()
    {
        attack.Play();
    }

    public void PlayDamage()
    {
        damage.Play();
    }

    public void PlayDash1()
    {
        dash1.Play();
    }

    public void PlayDash2()
    {
        dash2.Play();
    }

    public void PlayDying()
    {
        dying.Play();
    }

    public void PlayExplosion()
    {
        explosion.Play();
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlayJumpBass()
    {
        jumpBass.Play();
    }


    public void PlayShoot1()
    {
        shoot1.Play();
    }
    public void PlayShoot2()
    {
        shoot2.Play();
    }

    public void PlaySwoosh()
    {
        swoosh.Play();
    }

    public void PlayGameplayMusic()
    {
        gameplayMusic.Play();
    }

    public void SetMusicMood(int i)
    {
        gameplayMusic.SetParameter("Mood", i);
    }
}
