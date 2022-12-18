using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite checkPointOn, checkPointOff;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            CheckPointController.instance.DeactiveCheckPoints();
            sr.sprite= checkPointOn;

            // guardamos la posicion del checkpoint
            CheckPointController.instance.setSpawnPoint(transform.position);
        }
    }

    public void ResetCheckPoint()
    {
        sr.sprite = checkPointOff;
    }
}
