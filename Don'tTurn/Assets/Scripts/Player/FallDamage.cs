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
        if(PlayerMovement.IsGrounded() == true && rb2D.velocity.y < 0.0001f)
        {
            if(maxYVel <= -40f)
            {
                TakeFallDamage(2f * -maxYVel);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDmg, this.transform.position);
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
