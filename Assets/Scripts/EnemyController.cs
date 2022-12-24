using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    public Transform leftPoint, rightPoint;
    public SpriteRenderer sr;

    private float moveCout, waitCount;
    public float moveSpeed;
    public bool movingRight;
    public float moveTime, waitTime;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // para que estos points, no se muevan cuando la rana lo haga
        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;
        moveCout = moveTime;
    }


    void Update()
    {
       if(moveCout > 0) {
            moveCout -= Time.deltaTime;
            if (movingRight)
            {
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
                sr.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
                sr.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if(moveCout <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMove", true);
       } else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            body.velocity = new Vector2(0f, body.velocity.y);

            if (waitCount <= 0)
            {
                moveCout = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMove", false);
        }
        
    }
}
