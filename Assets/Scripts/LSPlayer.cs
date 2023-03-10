using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// para que el jugador se mueva entre el mapa
public class LSPlayer : MonoBehaviour
{
    // una referencia a esa clase para
    // cargar un nivel, cuando el player este 
    // en el mapa
    public LSManager lsManager;

    public MapPoint currentPoint;
    public float moveSpeed = 10f;
    public bool levelLoading;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position,moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f ) {

            if(Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                if(currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f)
            {
                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            if (Input.GetAxisRaw("Horizontal") < - 0.5f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }


            if (Input.GetAxisRaw("Vertical") < - 0.5f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            // si el nivel no asignado "", no cargamos nada
            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                //cuando el player este sobre el nivel
                // se mostrara el panel con el nombre del nivel
                LSUIManager.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Jump"))
                {
                    levelLoading= true;
                    lsManager.LoadLevel();
                }
            }
        }

    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIManager.instance.HideInfo();
    }
}
