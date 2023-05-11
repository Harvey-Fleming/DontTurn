using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Info")]
    public int moveDirection;
    public float moveSpeed, damageRange, maxRange, wanderDistance, jumpForce;
    private float jumpTimer;
    public float jumpCooldown;
    public float startMoveSpeed;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private LayerMask layerMask, jumpMask;
    private EnemyAttack enemyAttackScript;
    private StudioEventEmitter emitter;
    private StudioEventEmitter aggroEmitter;

    [Header("States")]
    [SerializeField] public bool isWandering;
    [SerializeField] private bool isAggro;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool canJump;
    [SerializeField] private bool canMove;

    private bool facingright;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        enemyAttackScript = GetComponent<EnemyAttack>();
        isWandering = true;
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.duoSkellySounds, this.gameObject);
        emitter.Play();

        startMoveSpeed = moveSpeed;

        StartCoroutine(WanderDelay());

    }

    private void Update()
    {
        //UpdateSound();

        if (!isAggro)
        {
            isWandering = true;
            //emitter.Play();
            //aggroEmitter.Stop();

        }

        //if (gameObject.GetComponent<EnemyStats>().isDead == true)
        {
            //emitter.Stop();
            //aggroEmitter.Stop();
        }

        if (!canJump)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpCooldown)
            {
                canJump = true;
            }
        }

        if (Vector2.Distance(transform.position, player.position) < maxRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position), maxRange, ~layerMask);

            if (hit.transform == player)
            {
                isAggro = true;
                isWandering = false;
            }
            else
            {
                isAggro = false;
            }
        }
        else
        {
            isAggro = false;
        }

        if (isAggro)
        {
            AggroMovement();
            //emitter.Stop();
            //aggroEmitter.Play();
        }

        if (isWandering)
        {
            WanderMovement();
        }

        CheckFlip();
    }

    void WanderMovement()
    {
        if (Vector2.Distance(transform.position, player.position) < wanderDistance && canMove)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            moveSpeed = startMoveSpeed;

            if (Physics2D.Raycast(transform.position, new Vector2(moveDirection , 0), 1, jumpMask))
            {
                if (canJump)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                    canJump = false;
                    jumpTimer = 0;
                }
            }
        }
    }

    IEnumerator WanderDelay()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 0)
            {
                moveDirection = -1;
            }
            else
            {
                moveDirection = 1;
            }

            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            moveDirection = 0;
            yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        }
    }

    void AggroMovement()
    {
        if (!isAttacking)
        {
            if (Vector2.Distance(transform.position, player.position) > damageRange)
            {
                if (player.position.x > transform.position.x)
                {
                    moveDirection = 1;

                    if(Physics2D.Raycast(transform.position, Vector2.right, 2, jumpMask))
                    {
                        if (canJump)
                        {
                            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                            canJump = false;
                            jumpTimer = 0;
                        }
                    }
                }
                if (player.position.x < transform.position.x)
                {
                    moveDirection = -1;

                    if (Physics2D.Raycast(transform.position, Vector2.left, 2, jumpMask))
                    {
                        if (canJump)
                        {
                            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                            canJump = false;
                            jumpTimer = 0;
                        }
                    }
                }
            }
            else
            {
                Damage();
            }
        }

        if(canMove)
        {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            moveSpeed = startMoveSpeed * 1.75f;  
        }                 
    }

    void Damage()
    {
        isAttacking = true;
        moveDirection = 0;
        if(isAttacking)
        {
            animator.SetBool("IsAttacking", true);
        }
    }

    public void ResetAttack()
    {
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    void CheckFlip()
    {
        //flips the sprite depending on their direction of movement
        if( moveDirection < 0 && facingright)
        {
            Flip();
        }
        if( moveDirection > 0 && !facingright)
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

    private void UpdateSound()
    {
        if (isAggro)
        {
            if (!aggroEmitter.IsPlaying())
            {
                Debug.Log("enemyAggro");
                emitter.Stop();
                aggroEmitter.Play();
            }
        }
        else if (isWandering)
        {
            if (!emitter.IsPlaying())
            {
                aggroEmitter.Stop();
                emitter.Play();
            }
        else
            {
                //aggroEmitter.Stop();
                //emitter.Stop();
            }
        }

    }

}
