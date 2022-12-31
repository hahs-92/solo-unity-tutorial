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

    void Start()
    {
        if (isLevel && levelToLoad != null)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
