using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private CorruptionScript corruptionScript;
    private int attackDamage = 10;
    private float timebetweenRegens = 1;

    private bool canRegen = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerStats.OnHit(attackDamage);
        }    
        if (collision.gameObject.CompareTag("DeathBox"))
        {
            playerStats.OnDeath();
        } 
    }

    public void OnEnterCheckpoint()
    {
        StartCoroutine("Regenerate");

    }

    IEnumerator Regenerate()
    {
        while (canRegen == true)
        {
        canRegen = false;
        if (playerStats.health < playerStats.maxHealth)
        {
        playerStats.health += 20;
            if (playerStats.health > playerStats.maxHealth)
            {
                playerStats.health = playerStats.maxHealth;
            }
        }
        else if (playerStats.health > playerStats.maxHealth)
        {
            playerStats.health = playerStats.maxHealth;
        }

        if (corruptionScript.time > 0)
        {
            corruptionScript.time -= 10;
        }
        else if (corruptionScript.time < 0)
        {
            corruptionScript.time = 0f;
        }
        yield return new WaitForSeconds(timebetweenRegens);
        canRegen = true;
        yield break;
        }
    }

}
