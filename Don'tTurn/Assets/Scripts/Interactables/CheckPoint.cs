using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour, IDataPersistence
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
            playerStatsScript.checkpointTransform = gameObject.transform;
            playerStatsScript.checkpointTransform.position = gameObject.transform.position;
            
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

    public void SaveData(GameData gameData)
    {

    }

    public void LoadData(GameData gameData)
    {

    }
}
