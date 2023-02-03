using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Movement")]
    private Rigidbody2D rb;
    float moveValue = 0;
    public float moveSpeed = 3f;
    float moveMultiplier = 100f;

    [Header("Jump Movement")]
    public bool isGrounded;
    public float jumpForce = 1f;
    int aerialJumpCount;
    public int maxAerialJumpCount;
    public LayerMask groundLayer;

    [Header("Player")]
    SpriteRenderer playerSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        //the horizontal movement value
        moveValue = Input.GetAxisRaw("Horizontal");

        Jump();

        FlipSprite();

        AerialJump();
    }

    private void FixedUpdate()
    {
        GroundMovement();
    }

    void GroundMovement()
    {
        rb.velocity = new Vector2(moveValue * (moveSpeed * moveMultiplier) * Time.deltaTime, rb.velocity.y);
    }
    void Jump()
    {
        //raycast checks if player is on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        if(hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            //Reset aerial jumps when on ground
            aerialJumpCount = maxAerialJumpCount;

            //Basic Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }

    void FlipSprite()
    {
        //flips the sprite depending on their direction of movement
        if(moveValue < 0)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }
    }

    void AerialJump()
    {
        //Can perform as many aerial jumps as there are max aerial jumps
        if (aerialJumpCount > 0 && !isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                --aerialJumpCount;
            }
        }
    }

}
