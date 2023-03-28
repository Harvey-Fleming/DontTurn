using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDataPersistence, IsKillable
{
    //Component References
    public static PlayerStats instance {get; private set;}
    [SerializeField] private CorruptionScript corruptionScript;

    //Stats
    [Range(0, 100)] public float maxHealth = 100f, health = 100f;
    private float cursePoints;
    
    private Vector3 playerPosition;
    public Vector3 spawnPoint; 

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

    public void OnHit(int attackDamage, GameObject attacker)
    {
        health -= attackDamage;

    }

    public void OnDeath()
    {
        Debug.Log("You Died!");
        transform.position = spawnPoint; 
        health = maxHealth;
        corruptionScript.time = 0f;
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
