using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // tiempo de espera antes que aparesca el player
    public float waitToRespawn, timeInLevel;
    // numero de gemas recolecatdas por el player
    public int gemsCollected;
    public string levelToLoad;

    private void Awake()
    {
        instance = this;
        timeInLevel = 0f;
    }

    private void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine() 
    {
        // desactivamos el player, luego espera unos segundos 
        // y por ultimo activa nuevamente el player, en la posicion 
        // guardada en el checkpoint
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);
        UIController.instance.FadeFromBlack();

        // instanciamos al jugador
        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint;
        // actualizamos los puntos d evida
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        //actualzamos la UI
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCoroutine());
    }

    public IEnumerator EndLevelCoroutine()
    {
        AudioManager.instance.PlayLevelVictory();
        PlayerController.instance.stopInput = true;
        // la camara deja de seguir al jugador
        CameraController.instance.stopFollow = true;
        // mostramos el mensage de completado
        UIController.instance.levelCompletText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);

        // guardamos la info que el nivel fue pasado, para despbloquear el siguiente nivel
        // esta info la utilizamos en MapPoint
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        //guardamos el nombre del nivel que pasamos
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            gemsCollected = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems") > gemsCollected 
                ? PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems") 
                : gemsCollected;
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            timeInLevel = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time") < timeInLevel
                ? PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time")
                : timeInLevel;
        }

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        SceneManager.LoadScene(levelToLoad);
    }
}
