using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDataPersistence, IsKillable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
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

    public void OnHit(int damageTaken)
    {
        currentHealth = currentHealth - damageTaken;
        StartCoroutine(ChangeColour());
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
    }


}
