using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private EnemyMovement enemyMovementScript;
    private Rigidbody2D rb2D;
    
    [SerializeField] private float baseKnockback = 30f, KnockbackDelay = 0.3f;


    private void Start() 
    {
        enemyMovementScript = GetComponent<EnemyMovement>();
        playerMovementScript = GetComponent<PlayerMovement>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void ApplyKnockBack(GameObject attacker)
    {
        /*if (rb2D != null)
        {
        Vector2 knockbackposdiff =  (gameObject.transform.position - attacker.transform.position).normalized;
        StartCoroutine("Reset");
        rb2D.AddForce(( knockbackposdiff * baseKnockback), ForceMode2D.Impulse);
        }
        else
        {
            return;
        } */

        if (rb2D != null)
        {
        Vector2 knockbackposdiff =  (gameObject.transform.position - attacker.transform.position).normalized;
        StartCoroutine("Reset");
        rb2D.velocity = new Vector2(knockbackposdiff.x * baseKnockback, baseKnockback);
        }
        else
        {
            return;
        }

    }

    IEnumerator Reset()
    {
        if (enemyMovementScript != null)
        {
            enemyMovementScript.enabled = false;
            yield return new WaitForSeconds(KnockbackDelay);
            rb2D.velocity = new Vector2(0,0);
            enemyMovementScript.enabled = true;
            yield break;
        }
        else if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
            yield return new WaitForSeconds(KnockbackDelay);
            rb2D.velocity = new Vector2(0,0);
            playerMovementScript.enabled = true;
            yield break;
        }
        
    }

    private void OnValidate() 
    {
      
    }
}
