using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    PlayerMovement PlayerMovement;
    Rigidbody2D rb2D;
    PlayerStats playerStats;
    public float maxYVel; 
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
        PlayerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.isGrounded == true && rb2D.velocity.y == 0)
        {
            if(maxYVel <= -15)
            {
                TakeFallDamage(); 
            }
        }
        else if(PlayerMovement.isGrounded == false)
        {
            if(rb2D.velocity.y < maxYVel)
            {
                maxYVel = rb2D.velocity.y; 
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && PlayerMovement.isGrounded == true)
        {
            maxYVel = 0; 
        }
    }

    void TakeFallDamage()
    {
        playerStats.health -= 10;
        maxYVel = 0; 
    }
}
