using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicKit : MonoBehaviour
{
    public bool isActive;
    public string type;
    GameObject player; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isActive = true;
            player = collision.gameObject; 
            //get player's health script or curse script here 
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false; 
    }

    private void Update()
    {
        if(isActive)
        {
            switch (type)
            {
                case "Medic":
                    player.GetComponent<Inventory>().medicAmount++; 
                    Destroy(gameObject);
                    break;
                case "Mushroom":
                    player.GetComponent<Inventory>().mushroomAmount++;
                    Destroy(gameObject);
                    break; 

            }
            
        }
    }
}
