using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    public AudioSource[] soundEffects;
    public AudioSource bgm, levelEndMusic;

    public void Awake()
    {
        instance = this;
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        // para que el sonido sea siempre diferente
        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        soundEffects[soundToPlay].Play();
    }

    // cuando finalize el game 
    // lo llamamos desde levelManager
    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }
}
