using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private PlayerMovement PlayerMovement;
    private Rigidbody2D rb2D;
    private PlayerStats playerStats;
    public float maxYVel; 

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
        PlayerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.IsGrounded() == true && rb2D.velocity.y == 0)
        {
            if(maxYVel <= -30f && maxYVel >= -40f)
            {
                TakeFallDamage(10); 
            }
            if(maxYVel <= -40f && maxYVel >= -60f)
            {
                TakeFallDamage(20);
            }
            if(maxYVel <= -60f && maxYVel >= -80f)
            {
                TakeFallDamage(30);
            }
            if(maxYVel <= -80f && maxYVel >= -100f)
            {
                TakeFallDamage(40);
            }
            if(maxYVel <= -100f && maxYVel >= -120f)
            {
                TakeFallDamage(50);
            }
            if (maxYVel <= -120f && maxYVel >= -140f)
            {
                TakeFallDamage(60);
            }
            if (maxYVel <= -140f && maxYVel >= -160f)
            {
                TakeFallDamage(70);
            }
            if (maxYVel <= -160f && maxYVel >= -180f)
            {
                TakeFallDamage(80);
            }
            if (maxYVel <= -180f && maxYVel >= -200f)
            {
                TakeFallDamage(90);
            }
            if (maxYVel <= -200f && maxYVel >= -220f)
            {
                TakeFallDamage(100);
            }

        }
        else if(PlayerMovement.IsGrounded() == false)
        {
            if(rb2D.velocity.y < maxYVel)
            {
                maxYVel = rb2D.velocity.y; 
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && PlayerMovement.IsGrounded() == true)
        {
            maxYVel = 0; 
        }
    }

    void TakeFallDamage(float damage)
    {
        playerStats.health -= damage;
        maxYVel = 0; 
    }
}
