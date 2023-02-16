using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private int bulletDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void CalculateBulletDamage()
    {
        bulletDamage = 2 * Mathf.RoundToInt(rb2D.velocity.x);
        Debug.Log(rb2D.velocity.x);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enter Collision");
            CalculateBulletDamage();
            other.gameObject.GetComponent<EnemyStats>()?.OnHit(bulletDamage);
        }
        else
        {
            return;
        }
    }

}
