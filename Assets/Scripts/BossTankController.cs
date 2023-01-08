using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{

    public enum BossStates {  SHOOTING, HURT, MOVING, ENDED};
    public BossStates currentStates;
    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public GameObject mine;
    public Transform minePoint;
    private float mineCounter;
    public float timeBetweenMines;
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

    [Header("Health")]
    public GameObject explotion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;
    public int health = 5;
    


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

                        if(isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explotion, theBoss.position, theBoss.rotation);
                            AudioManager.instance.StopBossMusic();
                            currentStates = BossStates.ENDED;
                            winPlatform.SetActive(true);
                        }
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
                    mineCounter = timeBetweenMines;
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
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();

        if(mines.Length > 0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        health--;
        if(health <= 0)
        {
            isDefeated = true;
        } else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentStates = BossStates.SHOOTING;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBoss.SetActive(true); 
    }
}
