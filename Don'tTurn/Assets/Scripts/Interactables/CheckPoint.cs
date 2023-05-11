using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using FMODUnity;

public class CheckPoint : MonoBehaviour, IDataPersistence
{
    [Header("Component References")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private MapPanelScript mapPanelScript;
    [SerializeField] private Transform restPointTrans;

    [SerializeField] public int checkpointNumber;
    [SerializeField] public bool hasVisited;

    private StudioEventEmitter emitter;

    private void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.CheckpointLight, this.gameObject);
        emitter.Play();

    }
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

    #region SaveRegion
    public void SaveData(GameData data)
    {
        if(data.hasUnlockedMap.ContainsKey(checkpointNumber))
        {
            data.hasUnlockedMap.Remove(checkpointNumber);
        }
        data.hasUnlockedMap.Add(checkpointNumber, hasVisited);
    }

    public void LoadData(GameData data)
    {
        data.hasUnlockedMap.TryGetValue(checkpointNumber, out hasVisited);
        StartCoroutine(UpdateMap());
    }

    IEnumerator UpdateMap()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if(hasVisited)
        {
            mapPanelScript.ShowMap(checkpointNumber);
        }
        yield break;
    }

    #endregion

}
