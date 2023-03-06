using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCollision playercollision;
    
    private void Awake() 
    {
        playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        playercollision = GameObject.FindWithTag("Player")?.GetComponent<PlayerCollision>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            playerStats.spawnPoint = this.gameObject.transform.position;
            DataPersistenceManager.instance.OnCheckPointReached();
            playercollision.OnEnterCheckpoint();
        }
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerStats == null)
        {
            playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        }
        else if (playercollision == null)
        {
            playercollision = GameObject.FindWithTag("Player")?.GetComponent<PlayerCollision>();
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        playerStats = null;
        playercollision = null;
    }

}
