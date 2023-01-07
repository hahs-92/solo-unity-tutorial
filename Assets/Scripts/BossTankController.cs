using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{

    public enum BossStates {  SHOOTING, HURT, MOVING};
    public BossStates currentStates;
    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public GameObject mine;
    public Transform minePoint;
    private float mineCounter;
    public float betweenMines;
    public float moveSpeed;

    [Header("Points")]
    public Transform leftPoint, rightPoint;
    private bool moveRight;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    private float shotCounter;
    public float timeBetweenShots;

    [Header("Hurt")]
    public GameObject hitBoss;
    private float hurtCounter;
    public float hurtTime;

    void Start()
    {
        currentStates = BossStates.SHOOTING;   
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentStates)
        {
            case BossStates.SHOOTING:
                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;
            case BossStates.HURT:
                if(hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if(hurtCounter <= 0)
                    {
                        currentStates = BossStates.MOVING;
                        mineCounter = 0;
                    }
                }
                break;
            case BossStates.MOVING:
                if(moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if(theBoss.position.x > rightPoint.position.x)
                    {
                        //theBoss.localScale = new Vector3(1f,1f,1f);
                        theBoss.localScale = Vector3.one;
                        moveRight = false;
                        EndMovement();
                    }
                } else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;
                if(mineCounter <= 0)
                {
                    mineCounter = betweenMines;
                    Instantiate(mine, mine.transform.position, mine.transform.rotation);
                }
                break;
        }
    }

    // se llama este metodo desde BossTankHiBoss
    public void TakeHit()
    {
        currentStates = BossStates.HURT;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");
    }

    private void EndMovement()
    {
        currentStates = BossStates.SHOOTING;
        shotCounter = timeBetweenShots;
        anim.SetTrigger("StopMoving");
        hitBoss.SetActive(true); 
    }
}
