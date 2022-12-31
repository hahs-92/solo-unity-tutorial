using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    public string startScene, continueScene;


    private void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked" ))
        {
            // si ya jugamos el juego podemos, continuar
            // se habilita el button de continuar
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }
}
