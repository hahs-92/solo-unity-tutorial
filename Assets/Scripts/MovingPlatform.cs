using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public Transform platform;

    public float moveSpeed;
    public int currentPoint;
 

    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(platform.position, points[currentPoint].position) < 0.05f)
        {
            currentPoint++;

            if(currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
