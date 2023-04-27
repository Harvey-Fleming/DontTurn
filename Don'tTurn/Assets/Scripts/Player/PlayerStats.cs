using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class PlayerStats : MonoBehaviour, IDataPersistence, IsKillable
{
    //Component References
    public static PlayerStats instance {get; private set;}
    [SerializeField] private CorruptionScript corruptionScript;
    private Knockback knockbackScript;
    private SpriteRenderer spriteRenderer;

    //Stats
    [Range(0, 100)] public float maxHealth = 100f, health = 100f;
    [Range(0,100)] private float cursePoints;
    private float iframeflicker = 0.1f;
    private bool canTakeDamage = true;

    
    private Vector3 playerPosition;
    public Vector3 spawnPoint;
    public TextMeshProUGUI damageIndicatorText; 


    private void Awake() 
    {
        DontDestroyOnLoad(this);

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

        knockbackScript = GetComponent<Knockback>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    public void OnHit(float attackDamage, GameObject attacker)
    {
        if(canTakeDamage == true)
        {
            health -= attackDamage;
            StartCoroutine(DamageIndication(attackDamage));
            corruptionScript.OnHitCorruption(attackDamage);
            knockbackScript.ApplyKnockBack(attacker);
            StartCoroutine("IFrames");
        }
    }

    public void OnDeath()
    {
        Debug.Log("You Died!");
        GetComponent<GrappleAbility>().StopGrapple();
        StartCoroutine(DeathWait()); 
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
        corruptionScript.StartCoroutine(corruptionScript.Timer(corruptionScript.areaTick));
    }

    public IEnumerator DamageIndication(float attackDamage)
    {
        damageIndicatorText.text = attackDamage.ToString(); 
        damageIndicatorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damageIndicatorText.gameObject.SetActive(false);
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
            if(spawnPoint != null)
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
