using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCollision playercollision;

    private bool isTriggerOn = false; 
    

    private void Awake() 
    {
        playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        playercollision = GameObject.FindWithTag("Player")?.GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerOn == true)
        {
            playerStats.spawnPointTransform = this.gameObject.transform;
            DataPersistenceManager.instance.OnCheckPointReached();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("CheckPoint Reached");
            isTriggerOn = true;
            playercollision.OnEnterCheckpoint();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerOn = false; 
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
