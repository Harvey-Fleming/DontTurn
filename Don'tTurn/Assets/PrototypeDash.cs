using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeDash : MonoBehaviour
{
    PlayerMovement movement;
    Rigidbody2D rb;
    public float dashSpeed;
    bool isDashing;
    public float dashTime;
    float dashDirection;
    int dashCount = 1;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            if (!isDashing)
            {
                dashDirection = Input.GetAxisRaw("Horizontal");
            }
        }

        if (!isDashing && dashCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isDashing = true;
                dashCount--;
            }
        }

        if (movement.isGrounded)
        {
            dashCount = 1;
        }

        if (isDashing)
        {
            StartCoroutine(DashAbility());
        }
    }

    IEnumerator DashAbility()
    {
        movement.enabled = false;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);
        yield return new WaitForSeconds(dashTime);
        movement.enabled = true;
        isDashing = false;
    }
}
