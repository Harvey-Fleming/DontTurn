using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerStats playerStats;
    private CorruptionScript corruptionScript;
    private int attackDamage = 10;
    private float timebetweenRegens = 1;
    private bool canRegen = true;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerStats.OnHit(attackDamage, collision.gameObject);
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

    //calls when the script is loaded or a value changes in the Inspector. Allows us to free up space in the inspector by assigning references automatically
    private void OnValidate() 
    {
        playerStats = this.gameObject.GetComponent<PlayerStats>();
        corruptionScript = GameObject.FindObjectOfType<CorruptionScript>();
    }

}
