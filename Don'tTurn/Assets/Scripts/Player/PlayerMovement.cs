using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Movement")]
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private DebugButtonsEditor debugButtonsEditor;
    private PlayerInput playerInput;
    private Transform moveToTargetTrans;
    float hmoveValue = 0, vmoveValue = 0;
    public float moveSpeed = 3f;
    float moveMultiplier = 100f;

    [Header("Jump Movement")]
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private float jumpForce = 1f, maxJumpTime = 0.1f;
    private float jumpTime = 0;
    public int maxAerialJumpCount;
    private int aerialJumpCount, gravityScale = 4, fallingGravityScale = 10;
    private bool IsJumping = false, jumpEndEarly = false;

    //"Coyote Jump" Variables
    [SerializeField] private float coyoteTimer = 0.1f;
    private float currentcoyoteTimer;

    [Header("Player")]
    [SerializeField] private Cursor cursorScript;
    [SerializeField] private float flipTimer, maxflipTimer = 0.5f;
    private Animator animator;
    private Vector2 cursorPos;
    public bool facingright = true, isGodEnabled;

    private void Start() 
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

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
        Flip();
    }

    void CheckJump()
    {
        //Basic Jump
        if (currentcoyoteTimer > 0f && playerInput.jumpKey)
        {
            IsJumping = true; jumpTime = 0;
        }

        if (IsJumping)
        {
            jumpEndEarly = false;
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  
            jumpTime += Time.deltaTime;
        }

        if (playerInput.jumpKeyReleased | jumpTime > maxJumpTime)
        {
            IsJumping = false;
            jumpEndEarly = true;
        }

        if (rb.velocity.y >= 0) { rb.gravityScale = gravityScale;}
        if (rb.velocity.y < 0 && jumpEndEarly == true) { rb.gravityScale = fallingGravityScale;}

        if (IsGrounded())
            {
                //Reset aerial jumps when on ground
                aerialJumpCount = maxAerialJumpCount;
                jumpEndEarly = false;
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

    void Flip()
    {
        //flips the sprite depending on their direction of movement and whether they are moving the cursor.
        if (Input.GetAxis("Mouse Y") != 0 && Input.GetAxis("Mouse X") != 0)
        {
            flipTimer = maxflipTimer;
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
        else if (Input.GetAxis("Mouse Y") == 0 && Input.GetAxis("Mouse X") == 0)
        {
            flipTimer -= 1 * Time.deltaTime;
            if(hmoveValue < 0  && facingright && flipTimer <= 0)
            {
                Vector2 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;

                facingright = !facingright;
            }
            else if(hmoveValue > 0  && !facingright && flipTimer <= 0)
            {
                Vector2 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;

                facingright = !facingright;
            }
        }

    }

    void AerialJump()
    {
        //Can perform as many aerial jumps as there are max aerial jumps
        if (aerialJumpCount > 0 && !IsGrounded())
        {
            if (playerInput.jumpKey)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                --aerialJumpCount;
            }
        }
    }

    public void ResetAirJump()
    {
        aerialJumpCount = maxAerialJumpCount;
    }

}
