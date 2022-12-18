using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;
    public CheckPoint[] checkPoints;
    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        // otra forma para llenar la lista de checkpoints
        checkPoints = FindObjectsOfType<CheckPoint>();
        // guardamos la posicion inicial de jugador
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void DeactiveCheckPoints()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void setSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    
}
