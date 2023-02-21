using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Movement")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider2D;
    float moveValue = 0;
    public float moveSpeed = 3f;
    float moveMultiplier = 100f;

    [Header("Jump Movement")]
    [SerializeField] private LayerMask GroundLayerMask;
    public float jumpForce = 1f;
    int aerialJumpCount;
    public int maxAerialJumpCount;

    [Header("Player")]
    [SerializeField] Animator animator;
    SpriteRenderer playerSprite;
    public bool facingright = true;



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
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        CheckFlip();
    }

    void Jump()
    {
            //Basic Jump
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                
            }

            if(IsGrounded())
            {
                //Reset aerial jumps when on ground
                aerialJumpCount = maxAerialJumpCount;
                animator.SetBool("IsJumping", false);
            }
            else if (!IsGrounded())
            {
                animator.SetBool("IsJumping", true);
            }
    }

    public bool IsGrounded()
    {
        float extraHeightTest = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeightTest, GroundLayerMask);
        return raycastHit.collider != null;
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
        if (aerialJumpCount > 0 && !IsGrounded())
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
