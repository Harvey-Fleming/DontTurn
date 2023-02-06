using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameObject player;
    bool isTriggerOn = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerOn == true)
        {
            player.GetComponent<HealthBarScript>().checkpoint = gameObject.transform;
            player.GetComponent<HealthBarScript>().checkpoint.position = gameObject.transform.position;
            

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Collision");
            player = collision.gameObject;
            isTriggerOn = true;
        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerOn = false; 
    }
}
