using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Movement")]
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    
    private Transform moveToTargetTrans;
    float hmoveValue = 0, vmoveValue = 0;
    public float moveSpeed = 3f;
    float moveMultiplier = 100f;

    [Header("Jump Movement")]
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private float jumpForce = 1f, maxJumpTime = 0.1f;
    private float jumpTime = 0;
    public int maxAerialJumpCount;
    private int aerialJumpCount, gravityScale = 4, fallingGravityScale = 6;
    private bool IsJumping = false;


    //"Coyote Jump" Variables
    [SerializeField] private float coyoteTimer = 0.1f;
    private float currentcoyoteTimer;

    [Header("Player")]
    private Animator animator;
    [SerializeField] private Cursor cursorScript;
    private Vector2 cursorPos;
    public bool facingright = true;
    public bool isGodEnabled;

    void Update()
    {
        //the horizontal movement value
        hmoveValue = Input.GetAxisRaw("Horizontal");
        vmoveValue = Input.GetAxisRaw("Vertical");
        cursorPos = cursorScript.newCursorPos;

        CheckJump();

        AerialJump();
    }

    private void FixedUpdate()
    {
        if (isGodEnabled)
        {
            DebugMovement();
        }
        GroundMovement();

    }

    public void DebugMovement() => rb.velocity = new Vector2(rb.velocity.x, vmoveValue * (moveSpeed * moveMultiplier) * Time.deltaTime);

    void GroundMovement()
    {
        rb.velocity = new Vector2(hmoveValue * (moveSpeed * moveMultiplier) * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        CheckFlip();
    }

    void CheckJump()
    {
        //Basic Jump
        if (currentcoyoteTimer > 0f && Input.GetKeyDown(KeyCode.Space))
        {
            IsJumping = true; jumpTime = 0;
        }

        if (IsJumping)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  
            jumpTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) | jumpTime > maxJumpTime)
        {
            IsJumping = false;
        }

        if (rb.velocity.y >= 0) { rb.gravityScale = gravityScale;}
        if (rb.velocity.y < 0) { rb.gravityScale = fallingGravityScale;}

        if (IsGrounded())
            {
                //Reset aerial jumps when on ground
                aerialJumpCount = maxAerialJumpCount;
                animator.SetBool("IsJumping", false);
                currentcoyoteTimer = coyoteTimer;
            }
            else if (!IsGrounded())
            {
                animator.SetBool("IsJumping", true);
                currentcoyoteTimer -= Time.deltaTime;
            }
    }

    public bool IsGrounded()
    {
        float extraHeightTest = 0.05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, boxCollider2D.bounds.extents.y + extraHeightTest, GroundLayerMask);
        return raycastHit.collider != null;
    }

    void CheckFlip()
    {
        //flips the sprite depending on their direction of movement
        if((cursorPos.x < gameObject.transform.position.x) && facingright)
        {
            Vector2 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingright = !facingright;
        }
        else if((cursorPos.x > gameObject.transform.position.x) && !facingright)
        {
            Vector2 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingright = !facingright;
        }  
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

    //calls when the script is loaded or a value changes in the Inspector. Allows us to free up space in the inspector by assigning references automatically
    private void OnValidate() 
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


}
