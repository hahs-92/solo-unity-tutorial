using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explotion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(explotion, transform.position, transform.rotation);

            PlayerHealthController.instance.DealDamage();
        }
    }
}
