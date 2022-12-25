using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    // jugador en el mapa
    // accedemos a su script
    public LSPlayer thePlayer;


    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    public IEnumerator LoadLevelCoroutine()
    {
        LSUIManager.instance.FadeToBlack();
        yield return new WaitForSeconds(1f / LSUIManager.instance.fadeSpeed + .25f);
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
