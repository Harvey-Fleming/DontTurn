using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDataPersistence, IsKillable
{
    [SerializeField] private string id;

    [SerializeField] private bool isDead = false;

    private int health = 20;

    [ContextMenu("Generate Unique Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            gameObject.SetActive(false);
        }   
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
        if(isDead)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnHit(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            OnDeath();
        }
        Debug.Log(health);
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
        isDead = true;
    }
}
