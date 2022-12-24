using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // tiempo de espera antes que aparesca el player
    public float waitToRespawn;
    // numero de gemas recolecatdas por el player
    public int gemsCollected;

    private void Awake()
    {
        instance = this;
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
}
