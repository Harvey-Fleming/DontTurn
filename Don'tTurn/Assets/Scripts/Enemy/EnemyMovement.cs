using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Info")]
    public int moveDirection;
    public float moveSpeed, damageRange, maxRange, wanderDistance, jumpForce;
    private float jumpTimer;
    public float jumpCooldown;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private LayerMask layerMask, jumpMask;

    [Header("States")]
    [SerializeField] private bool isWandering;
    [SerializeField] private bool isAggro;
    [SerializeField] private bool isDamaging;
    [SerializeField] private bool canJump;
    private bool facingright;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        isWandering = true;

        StartCoroutine(WanderDelay());
    }

    private void Update()
    {
        if (!isAggro)
        {
            isWandering = true;
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
        }

        if (isWandering)
        {
            WanderMovement();
        }

        CheckFlip();
    }

    void WanderMovement()
    {
        if (Vector2.Distance(transform.position, player.position) < wanderDistance)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            moveSpeed = 2f;

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
        if (!isDamaging)
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
                StartCoroutine(Damage());
            }
        }

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        moveSpeed = 3.5f;                       
    }

    IEnumerator Damage()
    {
        //hit

        isDamaging = true;
        moveDirection = 0;
        yield return new WaitForSeconds(1f);
        isDamaging = false;

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

    private void OnValidate() {

    }
}
