using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerStats playerStats;
    private CorruptionScript corruptionScript;
    private Knockback knockbackScript;

    private SpriteRenderer spriteRenderer;
    private GameObject incomingAttacker;
    private int attackDamage = 10;
    private float timebetweenRegens = 1, iframeflicker = 0.1f;
    [SerializeField] private bool canTakeDamage = true;
    private bool canRegen = true;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            playerStats.OnHit(attackDamage, collision.gameObject);
            corruptionScript.OnHitCorruption(attackDamage);
            knockbackScript.ApplyKnockBack(collision.gameObject);
            StartCoroutine("IFrames");
        }    
    }

    public void OnEnterCheckpoint()
    {
        StartCoroutine("Regenerate");
    }

    IEnumerator Regenerate()
    {
        while(canRegen)
        {
        canRegen = false;
        if (playerStats.health < playerStats.maxHealth || corruptionScript.time > 0)
        {
            playerStats.health += 20;
            corruptionScript.time -= 10;

            if (playerStats.health > playerStats.maxHealth)
            {
                playerStats.health = playerStats.maxHealth;
            }
            else if(corruptionScript.time < 0)
            {
                corruptionScript.time = 0f;
            }
        }
        yield return new WaitForSeconds(timebetweenRegens);
        if (playerStats.health != playerStats.maxHealth || corruptionScript.time != 0)
        {
            canRegen = true;
            StartCoroutine("Regenerate");
        }
        else
        {
            canRegen = true;
            yield break;
        }
        }
    }

    IEnumerator IFrames()
    {
        canTakeDamage = false;
        for (int i = 3; i > 0; i--)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(iframeflicker);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(iframeflicker);
        }
        canTakeDamage = true;
        yield break;
    }

    //calls when the script is loaded or a value changes in the Inspector. Allows us to free up space in the inspector by assigning references automatically
    private void OnValidate() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
        corruptionScript = GameObject.FindObjectOfType<CorruptionScript>();
        knockbackScript = GetComponent<Knockback>();
    }

}
