using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// para que la camera siga al player en el mapa
public class LSCameraController : MonoBehaviour
{
    public Vector2 minPos, maxPos;
    public Transform target;

    void Start()
    {
        
    }

    
    // para dar el effecto de que la camara se mueve despues del 
    // movimiento del jugador
    void LateUpdate()
    {
        //! CLAMP => Restringir
        // dado 5 value, 10 min, 15 max => 10
        // dado 16 value, 10 min, 15 max => 15
        // dado 12 value, 10 min, 15 max => 12

        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
