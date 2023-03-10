using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    public Transform farBG, middleBG;

    private Vector2 lastPos;
    public float minHeight, maxHeight;
    public bool stopFollow;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    { 
        if(!stopFollow)
        {
            // transform.position = new Vector3(target.position.x , transform.position.y , transform.position.z);
            //! CLAMP => Restringir
            // dado 5 value, 10 min, 15 max => 10
            // dado 16 value, 10 min, 15 max => 15
            // dado 12 value, 10 min, 15 max => 12

            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBG.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBG.position += new Vector3(amountToMove.x , amountToMove.y, 0f) * .5f;

            lastPos = transform.position;
        }
    }
}
