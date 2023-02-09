using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStatsScript;
    private bool isTriggerOn = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerOn == true)
        {
            playerStatsScript.lastcheckpointPosition = gameObject.transform.position;
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
