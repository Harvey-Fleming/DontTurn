using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStats>().OnDeath();
        }    
        if (collision.gameObject.CompareTag("Player"))
        {
           collision.gameObject.GetComponent<PlayerStats>().OnDeath();
        } 
    }

}
