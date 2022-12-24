using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject pickUpEffect;

    public bool isGem, isHeal;
    public bool isCollected;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isCollected)
        {
            if(isGem)
            {
                LevelManager.instance.gemsCollected++;
                UIController.instance.UpdateGemCount();
                // instanciamos el effect de collected
                Instantiate(pickUpEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(6);
                isCollected= true;
                Destroy(gameObject);
            } 

            if(isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();
                    AudioManager.instance.PlaySFX(6);
                    isCollected = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
