using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public GameObject deathEffect;

    // probabilidad de que el enemy lance una coin cuando muera
    [Range(0f, 100f)]
    public float changeToDrop;
    // coin 
    public GameObject collectible;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            // aplicamos un salto al jugador
            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f);

            if(dropSelect <= changeToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }

        }
    }
}
