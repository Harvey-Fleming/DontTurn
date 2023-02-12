using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStatsScript;
    private bool isTriggerOn = false; 
    

    private void Awake() 
    {
        playerStatsScript = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerOn == true)
        {
            playerStatsScript.spawnPointTransform = this.gameObject.transform;
            DataPersistenceManager.instance.OnCheckPointReached();
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Collision");
            isTriggerOn = true;
        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerOn = false; 
    }

}
