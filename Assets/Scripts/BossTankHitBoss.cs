using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBoss : MonoBehaviour
{
    public BossTankController bossController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossController.TakeHit();
            PlayerController.instance.Bounce();
            gameObject.SetActive(false);
        }
    }
}
