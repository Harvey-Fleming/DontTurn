using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private PlayerStats playerStats;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerStats.health -= 10f;
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

}
