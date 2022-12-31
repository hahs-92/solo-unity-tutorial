using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    // jugador en el mapa
    // accedemos a su script
    public LSPlayer thePlayer;

    //puntos en el mapa
    private MapPoint[] allPoints;


    private void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();

        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint point in allPoints)
            {
                if(point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint= point;
                }
            }
        }
    }

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
