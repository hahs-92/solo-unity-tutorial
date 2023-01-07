using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private Animator anim;

    public float bouncerForce;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController.instance.myBody.velocity = new Vector2(PlayerController.instance.myBody.velocity.x, bouncerForce);
            anim.SetTrigger("Bouncer_On");
        }
    }

}
