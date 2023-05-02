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
    private PlayerStats playerStats;
    private Knockback knockbackScript;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;

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
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnPos = gameObject.transform.position;
        Respawn();


        //audio
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.duoSkellyVoice, this.gameObject);
        emitter.Play();
    }

    private void Update()
    {
        damageIndicatorText.transform.localScale = new Vector3(1, damageIndicatorText.transform.localScale.y, damageIndicatorText.transform.localScale.z); 
    }


    #region TakingDamage
    public void OnHit(float damageTaken, GameObject incomingAttacker)
    {
        StartCoroutine(DamageIndication(damageTaken)); 
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
        spriteRenderer.color = normalColour;
        RandomDrop(); 
        gameObject.SetActive(false);
        isDead = true;
        emitter.Stop();
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
        float medkitDropRate = UnityEngine.Random.Range(1, 1); 
        if(medkitDropRate == 1)
        {
            Instantiate(MedKit, transform.position, transform.rotation); 
        }
        float mushroomDropRate = UnityEngine.Random.Range(1, 5);
        if (mushroomDropRate == 1)
        {
            Instantiate(Mushroom, transform.position, transform.rotation);
        }

    }

    public IEnumerator DamageIndication(float attackDamage)
    {
        damageIndicatorText.text = attackDamage.ToString();
        damageIndicatorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damageIndicatorText.gameObject.SetActive(false);
    }
    #endregion

    #region DealingDamage
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            playerStats.OnHit(collisionDamageDealt, gameObject);
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
