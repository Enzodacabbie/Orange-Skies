using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem groundTrail;
    public float speedModifier;

    [Header("Ground")]
    public float speed;
    bool facingRight;

    [Header("Ground Check")]
    public Transform groundCheckOrigin;
    public float groundCheckDistance;
    public LayerMask groundLayer; //input Ground layer here
    private bool grounded;
    private Vector2 groundCheckBoxSize;
    public int jumpLimit = 1;
    private int jumps;

    [Header("Jumping")]
    public float jumpHeight;
    public float airSpeed;
    private float GRAVITY; //never change

    [Header("Dashing")]
    public float dashSpeed;
    public int dashLimit = 1;
    public float dashDuration = 0.5f;
    private int dashes;

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

    private void FixedUpdate()
    {
        //grounded if ground detected beneath player
        grounded = Physics2D.OverlapBox(groundCheckOrigin.position, groundCheckBoxSize, 0, groundLayer);
    }

    //runs every frame
    private void Update()
    {
        speed += Time.deltaTime * speedModifier;
        airSpeed += Time.deltaTime * speedModifier;

        //resets jump and dash counter
        if (grounded && rb.velocity.y <= 0)
        {
            jumps = jumpLimit;
            dashes = dashLimit;
        }
        else
        {
            if (Input.GetKeyDown("z") && dashes > 0)
            {
                StartCoroutine(Dash());
            }
        }

        //flip if player faces and moves in the same direction
        //written here (kinda) inefficiently for readability
        if (facingRight && rb.velocity.x > 0)
            Flip();
        else if (!facingRight && rb.velocity.x < 0)
            Flip();

        //moves player horizontally
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        //if player has jumps, you can jump
        if (Input.GetKeyDown("up") && jumps > 0)
        {
            //if in midair and jump limit is 1, don't jump.
            if (grounded || jumpLimit != 1)
            {
                //physics equation.
                //upwards velocity = square root of (jump height * -2 * gravity)
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * GRAVITY));
            }
            
            //if you fell off a ledge and you jump, do essentially a double-jump
            if (!grounded && jumps == jumpLimit)
            {
                jumps--;
            }

            jumps--;
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

    //ienumerators/coroutines are essentially looping functions that rely on time. 
    //this is advanced unity stuff so if anyone needs finds this too confusing
    //then contact me and i'll do my best to do an alternative.
    IEnumerator Dash()
    {
        float dashTime = dashDuration;

        dashes--;

        //ternary operator. if facing right, dashDir is Vector2.right, otherwise, it's left.
        Vector2 dashDir = facingRight ? Vector2.left : Vector2.right;

        while (dashTime > 0)
        {
            //count down
            dashTime -= Time.deltaTime;

            //the part that dashes
            rb.velocity = dashDir * dashSpeed;

            //halfway through the player can cancel their dash for precision
            if (dashTime <= dashDuration/2 && Input.GetAxisRaw("Horizontal") != 0)
                break;

            //yield return null tells the program to wait for
            //the next frame
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cache the other object's tag for efficiency
        string tag = collision.gameObject.tag;

        //bounce should only be used on triggers, might change in the future - V.P
        if (tag == "Bounce")
        {
            print("bounced");

            //bounce half jumpHeight
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * GRAVITY));

            //replenish dashes if the player didn't have any
            if (dashes == 0)
            {
                print("replinished dashes");
                dashes++;
            }
        }
        //some enemies use a trigger
        else if (tag == "Enemy")
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //cache the other object's tag for efficiency
        string tag = collision.gameObject.tag;

        //move half speed when on mud blocks (also spawn a particle effect)
        if (tag == "Mud")
        {
            print("stepped in mud");
            groundTrail.gameObject.SetActive(true);
            speed /= 2;
        }
        //some enemies use an actual collider
        else if (tag == "Enemy")
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mud"))
        {
            groundTrail.gameObject.SetActive(false);
            speed *= 2;
        }
    }

    public void Die()
    {
        print("died to enemy");
        Destroy(gameObject);
    }
}
