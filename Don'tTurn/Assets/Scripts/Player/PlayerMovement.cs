
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;


[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    [Header("Ground Movement")]
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private PlayerInput playerInput;
    private Transform moveToTargetTrans;
    float hmoveValue = 0, vmoveValue = 0;
    public float moveSpeed = 3f;
    float moveMultiplier = 100f;

    #if UNITY_EDITOR
    private DebugButtonsEditor debugButtonsEditor;
    #endif

    [Header("Jump Movement")]
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private float jumpForce = 1f, maxJumpTime = 0.1f;
    private float jumpTime = 0;
    public int maxAerialJumpCount = 1;
    private int aerialJumpCount;
    private bool IsJumping = false;

    //"Coyote Jump" Variables
    [SerializeField] private float coyoteTimer = 0.1f;
    private float currentcoyoteTimer;

    [Header("Player")]
    [SerializeField] private Cursor cursorScript;
    [SerializeField] private float flipTimer, maxflipTimer = 0.5f;
    private Animator animator;
    private Vector2 cursorPos;
    public bool facingright = true, isGodEnabled, isDoubleJumpUnlocked;

    //audio
    public EventInstance playerFootsteps;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        //initialise footsteps audio instance
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
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

        UpdateSound();
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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);
        }

        if (IsJumping)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTime += Time.deltaTime;
        }

        if (playerInput.jumpKeyReleased | jumpTime > maxJumpTime)
        {
            IsJumping = false;
        }

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

    void Flip()
    {
        //flips the sprite depending on their direction of movement and whether they are moving the cursor.
        if (Input.GetAxis("Mouse Y") != 0 && Input.GetAxis("Mouse X") != 0)
        {
            flipTimer = maxflipTimer;
            if ((cursorPos.x < gameObject.transform.position.x) && facingright)
            {
                Vector2 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;

                facingright = !facingright;
            }
            else if ((cursorPos.x > gameObject.transform.position.x) && !facingright)
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
            if (hmoveValue < 0 && facingright && flipTimer <= 0)
            {
                Vector2 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;

                facingright = !facingright;
            }
            else if (hmoveValue > 0 && !facingright && flipTimer <= 0)
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
        if (aerialJumpCount > 0 && !IsGrounded() && isDoubleJumpUnlocked)
        {
            if (playerInput.jumpKey)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                --aerialJumpCount;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerJump, this.transform.position);
            }
        }
    }

    public void ResetAirJump()
    {
        aerialJumpCount = maxAerialJumpCount;
    }

    private void UpdateSound()
    {
        //start footsteps event if the player has an x velocity and is on the ground
        if (rb.velocity.x != 0 && IsGrounded() == true && Time.timeScale == 1)
        {
            // get the playback state
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        //otherwise, stop the footsteps event
        else
        {
            playerFootsteps.stop(STOP_MODE.IMMEDIATE);
        }
    }

    //Save and Loading Data
    public void LoadData(GameData data)
    {
        this.isDoubleJumpUnlocked = data.isDoubleJumpUnlocked;
    }

    public void SaveData(GameData data)
    {
        data.isDoubleJumpUnlocked = this.isDoubleJumpUnlocked;
    }

}
