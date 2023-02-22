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
    public float maxHealth = 100f;
    [Range(0, 100)] public float health = 100f;
    private float cursePoints;
    
    private Vector3 playerPosition;
    public Transform spawnPointTransform; 

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
    }

    private void Start() 
    {
        this.gameObject.transform.position = spawnPointTransform.position;
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void Update()
    {
        if(health <= 0)
        {
            OnDeath();
        }
    }

    public void OnHit(int attackDamage)
    {
        health -= attackDamage;
    }

    public void OnDeath()
    {
        Debug.Log("You Died!");
        transform.position = spawnPointTransform.position; 
        health = maxHealth;
        corruptionScript.time = 0f;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPointTransform == null)
        {
            spawnPointTransform = GameObject.FindWithTag("SpawnPoint")?.GetComponent<Transform>();
            if(spawnPointTransform != null)
            {
                this.gameObject.transform.position = spawnPointTransform.position;      
            }  
        }
        else if (spawnPointTransform != null)
        {
        this.gameObject.transform.position = spawnPointTransform.position;    
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        spawnPointTransform = null;        
    }


    /*--------------------------------------------------------------------------------------------*/
    //Save and Loading Data
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.health = data.playerHealth;
        this.spawnPointTransform = data.spawnPointTransform;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerHealth = this.health;
        data.spawnPointTransform = this.spawnPointTransform;
    }

}
