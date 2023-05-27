using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using TMPro; 

public class EnemyStats : MonoBehaviour, IDataPersistence, IsKillable
{
    //Component References
    private EnemyMovement enemyMovementScript;
    private Knockback knockbackScript;
    private DamageIndicator damageIndicatorScript;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
    public BossHealth healthBar;

    private StudioEventEmitter emitter;
    public TextMeshProUGUI damageIndicatorText;

    //Stats Variables
    [SerializeField] private string id;
    public bool isDead = false;
    [SerializeField] private float maxHealth = 15, collisionDamageDealt = 10;
    private float currentHealth = 15;
    private Vector2 respawnPos;
    Color normalColour = Color.white;

    //Drop item references
    [SerializeField] private GameObject MedKit;
    [SerializeField] private GameObject Mushroom;

    [ContextMenu("Generate Unique Guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        enemyMovementScript = GetComponent<EnemyMovement>();
        knockbackScript = GetComponent<Knockback>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        damageIndicatorScript = GetComponent<DamageIndicator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnPos = gameObject.transform.position;
        Respawn();


        //audio
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.duoSkellySounds, this.gameObject);
        emitter.Play();
    }

    private void Update()
    {
        if (maxHealth == 75)
        {
            healthBar.SetHealth(currentHealth);
        }
    }


    #region TakingDamage
    public void OnHit(float damageTaken, GameObject incomingAttacker)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.duoSkellyDmg, this.transform.position);
        damageIndicatorScript.SpawnIndicator(damageTaken, Color.white);
        currentHealth = currentHealth - damageTaken;
        StartCoroutine(ChangeColour());
        knockbackScript.ApplyKnockBack(incomingAttacker);
        if (currentHealth <= 0)
        {
            OnDeath();
            emitter.Stop();
        }
        Debug.Log(currentHealth);
    }

    public void OnDeath()
    {
        spriteRenderer.color = normalColour;
        RandomDrop(); 
        gameObject.SetActive(false);
        isDead = true;
        //emitter.Stop();
        Debug.Log("enemy sound stop");
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
            if(respawnPos == Vector2.zero)
            {
                respawnPos = gameObject.transform.position;
                return;
            }
            else
            {
                gameObject.transform.position = respawnPos;
            }
            enemyMovementScript.enabled = true;
            enemyMovementScript.isWandering = true;
            enemyMovementScript.ResetState();
            currentHealth = maxHealth;
        }
    }

    IEnumerator ChangeColour()
    {
        Color hitColour = Color.red;
        spriteRenderer.color = hitColour;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = normalColour;
        yield break;
    }

    public void RandomDrop()
    {
        float medkitDropRate = UnityEngine.Random.Range(1, 10); 
        if(medkitDropRate == 1)
        {
            Instantiate(MedKit, transform.position, transform.rotation); 
        }
        float mushroomDropRate = UnityEngine.Random.Range(1, 10);
        if (mushroomDropRate == 1)
        {
            Instantiate(Mushroom, transform.position, transform.rotation);
        }

    }
    #endregion

    #region DealingDamage
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>()?.OnHit(collisionDamageDealt, gameObject);
        }
    }
    #endregion

    #region SaveRegion
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

    #endregion





    
}
