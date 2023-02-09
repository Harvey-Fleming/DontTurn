using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    private float maxHealth = 100f;
    public float health = 100f;
    private Vector3 playerPosition;
    public Vector2 lastcheckpointPosition; 

    private void Start() 
    {
        if (lastcheckpointPosition != null)
        {
            lastcheckpointPosition = this.transform.position;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("You Died!");
            transform.position = lastcheckpointPosition; 
            health = maxHealth;
        }    
    }


    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.health = data.playerHealth;
        this.lastcheckpointPosition = data.lastcheckpointPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerHealth = this.health;
        data.lastcheckpointPosition = this.lastcheckpointPosition;
    }
}
