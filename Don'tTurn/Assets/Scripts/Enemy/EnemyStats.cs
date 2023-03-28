using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDataPersistence, IsKillable
{
    private EnemyMovement enemyMovementScript;
    private Knockback knockbackScript;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;


    [SerializeField] private string id;
    public bool isDead = false;
    private int maxHealth = 15, currentHealth = 15;

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
        StartCoroutine(ChangeColour());
        knockbackScript.ApplyKnockBack(incomingAttacker);
        if (currentHealth <= 0)
        {
            OnDeath();
        }
        Debug.Log(currentHealth);
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
        isDead = true;
    }

    //Used by Debug Buttons to respawn enemies and when Loading save data
    public void Respawn()
    {
        //Debug.Log("Enemy Respawn Triggered");
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
        enemyMovementScript = GetComponent<EnemyMovement>();
        knockbackScript = GetComponent<Knockback>();
    }


}
