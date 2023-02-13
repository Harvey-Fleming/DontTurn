using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Info")]
    public int moveDirection;
    public float moveSpeed;
    public float damageRange;
    public float maxRange;

    Rigidbody2D rb;
    Transform player;
    RaycastHit2D hit;
    public LayerMask layerMask;

    [Header("States")]
    public bool isWandering;
    public bool isAggro;
    public bool isDamaging;

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        isWandering = true;

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderDelay());
    }

    private void Update()
    {
        if (!isAggro)
        {
            isWandering = true;
        }

        if (Vector2.Distance(transform.position, player.position) < maxRange)
        {
            hit = Physics2D.Raycast(transform.position, (player.position - transform.position), maxRange, ~layerMask);

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
    }

    void WanderMovement()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        moveSpeed = 2f;
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
                }
                if (player.position.x < transform.position.x)
                {
                    moveDirection = -1;
                }
            }
            else
            {
                StartCoroutine(Damage());
            }
        }

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        moveSpeed = 4f;
    }

    IEnumerator Damage()
    {
        //hit

        isDamaging = true;
        moveDirection = 0;
        yield return new WaitForSeconds(1f);
        isDamaging = false;

    }
}
