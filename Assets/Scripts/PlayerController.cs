using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Animator anim;
    private SpriteRenderer sr;
    public Rigidbody2D myBody;

    [Header("Player variables")]
    private bool canDoubleJump;
    public float moveSpeed;
    public float jumpFource;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;


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

        Move();
        Jump();
        FlipXController();
    }

    public void Move()
    {
        // vector2 recibe dos paramtros, el ejex y el ejey
        myBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), myBody.velocity.y);
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
            } else if(canDoubleJump)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpFource);
                canDoubleJump= false;
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
}

