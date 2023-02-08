using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    PlayerMovement PlayerMovement;
    Rigidbody2D rigidbody2D;
    PlayerStats playerStats;
    public float maxYVel; 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        PlayerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.isGrounded == true && rigidbody2D.velocity.y == 0)
        {
            if(maxYVel <= -2)
            {
                TakeFallDamage(); 
            }
        }
        else if(PlayerMovement.isGrounded == false)
        {
            if(rigidbody2D.velocity.y < maxYVel)
            {
                maxYVel = rigidbody2D.velocity.y; 
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
