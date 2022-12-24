using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject pauseScreen;
    public bool isPaused;
    public string levelSelect, mainMenu;

    private void Awake()
    {
        instance= this;
    }

    void Update()
    {
        // Menu, esta en projectSeetings, input manager
        // se creo, se elimino de cancel el button positive= escape
        // escape 
        if(Input.GetButtonDown("Menu"))
        {
            Debug.Log("puased");
            PauseUnPause();
        }
        
    }

    public void PauseUnPause()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        } else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
