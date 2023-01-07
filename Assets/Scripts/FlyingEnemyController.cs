using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    private Vector3 attackTarget;
    public Transform[] points;
    public SpriteRenderer sr;

    private float attackCounter;
    public float moveSpeed, distanceToAttackPlayer, chaseSpeed, waitAfterAttack;
    public int currentPoint;


    private void Start()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    void Update()
    {
        if(attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        } else
        {
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer) {
                attackTarget = Vector3.zero;
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.05f)
                {
                    currentPoint++;

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }
            } else
            {
                if(attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                // seguimos al player
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, attackTarget) <= 0.1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }

        FlipController();
    }

    private void FlipController()
    {
        if(transform.position.x < points[currentPoint].position.x)
        {
            sr.flipX= true;
        } else {
            sr.flipX= false;
        }
    }
}
