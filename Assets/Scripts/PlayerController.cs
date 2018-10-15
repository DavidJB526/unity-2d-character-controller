using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float jumpForce = 700f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;

    private float groundRadius = 0.2f;

    private bool facingRight = true;
    private bool grounded = false;
    private bool doubleJump = false;

    private Rigidbody2D rb2d;
    private Animator anim;


	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    private void Update()
    {
        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("ground", false);
            rb2d.AddForce(new Vector2(0f, jumpForce));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
    }

    void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("ground", grounded);
        anim.SetFloat("vSpeed", rb2d.velocity.y);

        if (grounded)
        {
            doubleJump = false;
        }

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(move));
        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
