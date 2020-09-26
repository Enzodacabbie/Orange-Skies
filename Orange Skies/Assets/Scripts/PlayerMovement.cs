using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground")]
    public float speed;
    bool facingRight;

    [Header("Ground Check")]
    public Transform groundCheckOrigin;
    public float groundCheckDistance;
    public LayerMask groundLayer; //input Ground layer here
    private bool grounded;
    private Vector2 groundCheckBoxSize;

    [Header("Jumping")]
    public float jumpHeight;
    public float airSpeed;
    private float GRAVITY; //never change
    
    Rigidbody2D rb;

    //runs before game starts
    private void Awake()
    {
        //grab a reference to the rigidbody before the game starts
        rb = GetComponent<Rigidbody2D>();

        //get the force of gravity from the physics menu
        GRAVITY = Physics2D.gravity.y;

        //let the ground check box size be 95% the width
        //of the player's transform scale, with height
        //groundcheckDistance.
        //cache it here to save on memory
        groundCheckBoxSize = transform.localScale;
        groundCheckBoxSize.x *= 0.95f;
        groundCheckBoxSize.y = groundCheckDistance;
    }

    //runs every frame
    private void Update()
    {
        //grounded if ground detected beneath player
        grounded = Physics2D.OverlapBox(groundCheckOrigin.position, groundCheckBoxSize, 0, groundLayer);

        //flip if player faces and moves in the same direction
        //written here (kinda) inefficiently for readability
        if (facingRight && rb.velocity.x > 0)
            Flip();
        else if (!facingRight && rb.velocity.x < 0)
            Flip();

        //moves player horizontally
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        //if player is grounded, you can jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //physics equation.
            //upwards velocity = square root of (jump height * -2 * gravity)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * GRAVITY));
        }
    }

    //flips player to the other side
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 flipped = transform.localScale;
        flipped.x *= -1;
        transform.localScale = flipped;
    }

    //debug for inspector
    private void OnDrawGizmosSelected()
    {
        Vector2 gizmoBoxSize = transform.localScale;
        gizmoBoxSize.x *= 0.95f;
        gizmoBoxSize.y = groundCheckDistance;

        Gizmos.DrawWireCube(groundCheckOrigin.position, gizmoBoxSize);
    }
}
