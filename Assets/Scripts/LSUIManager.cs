using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;

    public Image fadeScreen;

    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    // panel que se muestra cuando el jugador elige nivel
    public GameObject levelInfoPanel;
    public TextMeshProUGUI levelName, gemsFound, gemsTarget, bestTime, timeTarget;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            // MoveToFowards hace que se mueva lentamente
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            // MoveToFowards hace que se mueva lentamente
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        // asignamos el nombre de la escena al texto del panel
        // que muestra el nombre del nivel
        levelName.text = levelInfo.levelName;
        //mostramos el panel
        levelInfoPanel.SetActive(true);

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";

        if(levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST ---";
        }
        else
        {
            // F2 muestra dos decimales
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("F2") + "s";
        }
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
