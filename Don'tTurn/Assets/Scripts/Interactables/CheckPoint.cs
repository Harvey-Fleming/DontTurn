using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCollision playerCollision;
    
    private void Awake() 
    {
        playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        playerCollision = GameObject.FindWithTag("Player")?.GetComponent<PlayerCollision>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") == true)
        {
            playerStats.spawnPoint = this.gameObject.transform.position;
            DataPersistenceManager.instance.OnCheckPointReached();
            playerCollision.OnEnterCheckpoint();
        }
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerStats == null)
        {
            playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        }
        else if (playerCollision == null)
        {
            playerCollision = GameObject.FindWithTag("Player")?.GetComponent<PlayerCollision>();
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        playerStats = null;
        playerCollision = null;
    }

}
