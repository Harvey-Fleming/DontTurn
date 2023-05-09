using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public GameObject player;
    public CorruptionScript corruptionScript;
    public Color invisible;
    public CurseFX curseFX;
    public ParticleSystem deathFX;

    public void Die()
    {
        StartCoroutine(DeathRespawn());
    }

    IEnumerator DeathRespawn()
    {
        ParticleSystem currentDeathFX = Instantiate(deathFX);
        currentDeathFX.transform.position = player.transform.position;
        player.GetComponent<SpriteRenderer>().color = invisible;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        curseFX.enabled = false;
        
        yield return new WaitForSeconds(2f);

        curseFX.enabled = true;

        player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().maxHealth;
        corruptionScript.time = 0f;

        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
        player.transform.position = player.GetComponent<PlayerStats>().spawnPoint;
        corruptionScript.StartCoroutine(corruptionScript.Timer(corruptionScript.areaTick));
    }
}
