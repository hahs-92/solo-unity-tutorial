using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    private SpriteRenderer sr;

    private float invicibleCounter;
    public int currentHealth, maxHealth;
    public float invincibleLength;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;

            if(invicibleCounter <= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f );
            }
        }
    }

    public void DealDamage()
    {
        if(invicibleCounter <= 0 )
        {
            currentHealth--;

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
            } else
            {
                invicibleCounter = invincibleLength;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }
}
