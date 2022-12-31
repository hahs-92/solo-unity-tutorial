using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;
    // levelToCheck es el nombre de la scene anterior
    // la cual ya debio ser pasada para ´poder seguir
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    // la gema y el reloj que aparece cuando
    // se consigue el mejor tiempo y todas las gemas
    public GameObject gemBadge, timeBadge;

    void Start()
    {
        if (isLevel && levelToLoad != null)
        {
            CheckPlayerPrefs();
            ShowBadges();
            CheckLevelToUnlocked();
        }
    }

    private void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
        {
            gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
        }

        if (PlayerPrefs.HasKey(levelToLoad + "_time"))
        {
            bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
        }
    }

    private void CheckLevelToUnlocked()
    {
        isLocked = true;

        if (levelToCheck != null)
        {
            // estas preferencias fueron guardadas desde LevelManager
            // cuando el jugador termina un nivel
            if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
            {
                if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                {
                    isLocked = false;
                }
            }
        }

        // cuando inicia el primer nivel, va estar desbloqueado
        if (levelToLoad == levelToCheck)
        {
            isLocked = false;
        }
    }

    private void ShowBadges()
    {
        if (gemsCollected >= totalGems)
        {
            gemBadge.SetActive(true);
        }

        if (bestTime <= targetTime && bestTime != 0)
        {
            timeBadge.SetActive(true);
        }
    }

   
}
