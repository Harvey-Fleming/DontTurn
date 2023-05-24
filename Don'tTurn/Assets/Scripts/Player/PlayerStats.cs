using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class PlayerStats : MonoBehaviour, IDataPersistence, IsKillable
{
    //Component References
    public static PlayerStats instance {get; private set;}
    [SerializeField] private CorruptionScript corruptionScript;
    private PlayerMovement playerMovement;
    private Knockback knockbackScript;
    private AttackScript attackScript;
    private SpriteRenderer spriteRenderer;
    private DamageIndicator damageIndicatorScript;
    public Image healthFX;

    //Stats
    [Range(0, 100)] public float maxHealth = 100f, health = 100f;
    [Range(0,100)] private float cursePoints;
    private float iframeflicker = 0.1f;
    private bool canTakeDamage = true;

    private Vector3 playerPosition;
    public Vector3 spawnPoint;
    public PlayerDeathHandler deathHandler;
    public ParticleSystem hitFX;
    public int abilAmount = 0;
    public ToggleAbilityImage toggleAbil;
 
    private void Awake() 
    {

        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        this.gameObject.transform.position = spawnPoint;

        playerMovement = GetComponent<PlayerMovement>();
        knockbackScript = GetComponent<Knockback>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageIndicatorScript = GetComponent<DamageIndicator>();
        attackScript = GetComponent<AttackScript>();

    }

    private void Start() 
    {
        this.gameObject.transform.position = spawnPoint;
        health = 30f;
    }

    void Update()
    {
        if(health <= 0)
        {
            OnDeath();
        }

        if(health < 30)
        {
            healthFX.color = new Color(1f, 0f, 0f, 0.5f - (health / 30f * 0.5f));
        }
        else
        {
            healthFX.color = new Color(1f, 0f, 0f, 0f);
        }
    }

    public void OnHit(float attackDamage, GameObject attacker)
    {
        if(canTakeDamage == true)
        {
            health -= attackDamage;
            ParticleSystem currentHitFX = Instantiate(hitFX);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDmg, this.transform.position);
            currentHitFX.transform.position = transform.position;
            damageIndicatorScript.SpawnIndicator(attackDamage, Color.red);
            corruptionScript.OnHitCorruption(attackDamage);
            knockbackScript.ApplyKnockBack(attacker);
            attackScript.ResetWindow();
            StartCoroutine("IFrames");
        }
    }

    public void AddAbility()
    {
        abilAmount++;
        toggleAbil.ChangeGraphic();
    }

    public void OnDeath()
    {
        GetComponent<GrappleAbility>().StopGrapple();
        health = maxHealth;
        corruptionScript.time = 0f;
        deathHandler.Die();
    }

        public IEnumerator DeathWait()
    {
        Time.timeScale = 0; 
        health = maxHealth;
        corruptionScript.time = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = spawnPoint;
    }

    IEnumerator IFrames()
    {
        canTakeDamage = false;
        for (int i = 3; i > 0; i--)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(iframeflicker);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(iframeflicker);
        }
        canTakeDamage = true;
        yield break;
    }
    


//Subscribing to on scene loaded and on scene unloaded events.
    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

//Checks where to spawn the player when the Scene is loaded, if one cannot be found. Location Defaults to start of Level
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPoint == Vector3.zero)
        {
            spawnPoint = GameObject.FindWithTag("SpawnPoint").GetComponent<Transform>().position;
            if(spawnPoint != Vector3.zero)
            {
                this.gameObject.transform.position = spawnPoint;      
            }  
        }
        else if (spawnPoint != Vector3.zero)
        {
        this.gameObject.transform.position = spawnPoint;    
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        spawnPoint = Vector3.zero;        
    }

//Save and Loading Data
    public void LoadData(GameData data)
    {
        this.health = data.playerHealth;
        this.spawnPoint = data.spawnPoint;
    }

    public void SaveData(GameData data)
    {
        data.playerHealth = this.health;
        data.spawnPoint = this.spawnPoint;
    }



}
