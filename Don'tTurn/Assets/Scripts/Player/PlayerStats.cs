using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static PlayerStats instance {get; private set;}
    private float maxHealth = 100f;
    public float health = 100f;
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

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("You Died!");
            transform.position = spawnPointTransform.position; 
            health = maxHealth;
        }    
    }


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
}
