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
    public Animator deathFade;
    public bool isDead = false;

    public void Die()
    {
        isDead = true;
        StartCoroutine(DeathRespawn());
        
    }

    IEnumerator DeathRespawn()
    {
        deathFade.SetBool("hasDied", true);

        ParticleSystem currentDeathFX = Instantiate(deathFX);
        currentDeathFX.transform.position = player.transform.position;
        player.GetComponent<AttackScript>().enabled = false;
        player.GetComponent<PrototypeDash>().enabled = false;
        player.GetComponent<SpriteRenderer>().color = invisible;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        curseFX.enabled = false;
        
        yield return new WaitForSeconds(2f);

        curseFX.enabled = true;

        player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().maxHealth;
        corruptionScript.time = 0f;

        player.GetComponent<PrototypeDash>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
        player.GetComponent<AttackScript>().enabled = true;
        player.GetComponent<AttackScript>().ResetWindow();
        player.transform.position = player.GetComponent<PlayerStats>().spawnPoint;

        yield return new WaitForSeconds(2f);
        deathFade.SetBool("hasDied", false);
        isDead = false;
    }
}
