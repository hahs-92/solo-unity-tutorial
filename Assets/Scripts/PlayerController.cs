using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Components")]
    public Animator anim;
    private SpriteRenderer sr;
    public Rigidbody2D myBody;

    [Header("Player variables")]
    private bool canDoubleJump;
    public float moveSpeed;
    public float jumpFource;
    public float bounceForce;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private float knockBackCounter;
    public float knockBackLength, knockBackForce;
    public bool stopInput;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(myBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        // solo se podra mover, si el juego no esta pausado
        // stopInput se vuelve true cuando toca la bandera
        if(!PauseMenu.instance.isPaused && !stopInput)
        {
            Move();
            Jump();
            FlipXController();
        }

    }

    public void Move()
    {
        if(knockBackCounter <= 0)
        {
            // vector2 recibe dos paramtros, el ejex y el ejey
            myBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), myBody.velocity.y);
        } else
        {
            knockBackCounter -= Time.deltaTime;
        }
    }

    public void Jump()
    {
        // creamos un circulo, para que detecte la layer del piso
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if(isGrounded )
        {
            canDoubleJump = true;
        } 

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpFource);
                AudioManager.instance.PlaySFX(10);
            } else if(canDoubleJump)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpFource);
                AudioManager.instance.PlaySFX(10);
                canDoubleJump = false;
            }
        }
    }

    public void FlipXController()
    {
        if (myBody.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (myBody.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
       
        if(sr.flipX)
        {
            myBody.velocity = new Vector2(knockBackForce, myBody.velocity.y);
        } else
        {
            myBody.velocity = new Vector2(-knockBackForce, myBody.velocity.y);
        }
    }

    // para que el player salye cuando golpee a un enemy
    public void Bounce()
    {
        myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
    }
}

