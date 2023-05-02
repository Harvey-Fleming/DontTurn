using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private Transform restPointTrans;


    [SerializeField] public int checkpointNumber; 
    
    private void Awake() 
    {
        playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        playerCollision = GameObject.FindWithTag("Player")?.GetComponent<PlayerCollision>();
        restPointTrans = gameObject.transform.GetChild(1);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") == true)
        {
            playerStats.spawnPoint = this.gameObject.transform.position;
            playerCollision.OnEnterCheckpoint(restPointTrans, this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) => playerCollision.OnLeaveCheckpoint();


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

    public void RespawnAllEnemies()
    {
        object[] allEnemies = Resources.FindObjectsOfTypeAll(typeof(EnemyStats));
        Debug.Log("allEnemies array has " + allEnemies.Length + "Items");

        foreach (EnemyStats enemyStats in allEnemies)
        {
            enemyStats.gameObject.SetActive(true);
            enemyStats.isDead = false;
            enemyStats.Respawn();
        }
    }

}
