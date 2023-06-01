using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingPunchedScript : MonoBehaviour
{
    public bool isPunched;
    private EnemyStats enemyStats; 
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hitbox"))
        {
            isPunched = true;
        }

        if (isPunched == true && collision.gameObject.CompareTag("Enemy"))
        {
            enemyStats.OnHit(5, gameObject, 5);
            isPunched = false; 
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            isPunched = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
