using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D myBody;

    [Header("Player variables")]
    public float moveSpeed;
    public float jumpFource;

    [Header("Grounded")]
    private bool isGround;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        
    }

    public void Move()
    {
        // vector2 recibe dos paramtros, el ejex y el ejey
        myBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), myBody.velocity.y);
    }

    public void Jump()
    {

        // creamos un circulo, para que detecte la layer del piso
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpFource);
        }
    }
}
