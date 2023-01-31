using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicKit : MonoBehaviour
{
    public bool isActive;
    public string type; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Press E!"); 
            isActive = true;
            //get player's health script or curse script here 
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false; 
    }

    private void Update()
    {
        if(isActive && Input.GetKeyDown(KeyCode.E))
        {
            switch (type)
            {
                case "Medic":
                    Debug.Log("Took Med Kit!"); 
                    //add health or minus curse 
                    Destroy(gameObject);
                    break;
                case "Mushroom":
                    Debug.Log("Took Mushroom!");
                    Destroy(gameObject);
                    break; 

            }
            
        }
    }
}
