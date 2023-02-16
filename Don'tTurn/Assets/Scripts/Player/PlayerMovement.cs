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

    [Header("Player")]
    SpriteRenderer playerSprite;
    bool facingright = true;

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

        AerialJump();
    }

    private void FixedUpdate()
    {
        GroundMovement();
    }

    void GroundMovement()
    {
        rb.velocity = new Vector2(moveValue * (moveSpeed * moveMultiplier) * Time.deltaTime, rb.velocity.y);

        CheckFlip();
    }
    void Jump()
    {
        if (isGrounded)
        {
            //Reset aerial jumps when on ground
            aerialJumpCount = maxAerialJumpCount;

            //Basic Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void CheckFlip()
    {
        //flips the sprite depending on their direction of movement
        if(moveValue < 0 && facingright)
        {
            Flip();
        }
        if(moveValue > 0 && !facingright)
        {
            Flip();
        }  
    }

    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingright = !facingright;
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
