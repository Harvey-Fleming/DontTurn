using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDataPersistence, IsKillable
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private EnemyMovement enemyMovement;

    [SerializeField] private string id;
    [SerializeField] private float baseKnockback = 500f, KnockbackDelay = 0.5f;
    public bool isDead = false;
    private int maxHealth = 15, currentHealth = 15;
    private GameObject attacker;

    [ContextMenu("Generate Unique Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    public void SaveData(GameData data)
    {
        if(data.isEnemyDead.ContainsKey(id))
        {
            data.isEnemyDead.Remove(id);
        }
        data.isEnemyDead.Add(id, isDead);
    }

    public void LoadData(GameData data)
    {
        data.isEnemyDead.TryGetValue(id, out isDead);
        Respawn();
    }

    public void OnHit(int damageTaken, GameObject incomingAttacker)
    {
        currentHealth = currentHealth - damageTaken;
        attacker = incomingAttacker;
        StartCoroutine(ChangeColour());
        ApplyKnockBack();
        if (currentHealth <= 0)
        {
            OnDeath();
        }
        Debug.Log(currentHealth);
    }

    private void ApplyKnockBack()
    {
        if (rb2D != null)
        {
        Vector2 knockbackposdiff =  (gameObject.transform.position - attacker.transform.position).normalized;

        StartCoroutine("Reset");
        rb2D.AddForce(( knockbackposdiff * baseKnockback), ForceMode2D.Impulse);
        }
        else
        {
            return;
        }
    }

    IEnumerator Reset()
    {
        enemyMovement.enabled = false;
        yield return new WaitForSeconds(KnockbackDelay);
        enemyMovement.enabled = true;
        yield break;
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
        isDead = true;
    }

    //Used by Debug Buttons to respawn enemies and when Loading save data
    public void Respawn()
    {
        Debug.Log("Enemy Respawn Triggered");
        if(isDead)
        {
            gameObject.SetActive(false);
        }  
        else if (!isDead)
        {
            gameObject.SetActive(true);
            currentHealth = maxHealth;
        }
    }

    IEnumerator ChangeColour()
    {
        Color normalColour = Color.white;
        Color hitColour = Color.red;
        spriteRenderer.color = hitColour;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = normalColour;
        yield break;
    }

    private void OnValidate() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }


}
