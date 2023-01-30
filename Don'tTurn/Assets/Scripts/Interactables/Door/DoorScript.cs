using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Interactables interactables; 

    // Start is called before the first frame update
    void Start()
    {
        interactables = GetComponentInChildren<Interactables>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if(interactables.isPressedOnce == true)
        {
            GetComponent<BoxCollider2D>().enabled = false; 
        }
       else if(interactables.isPressedOnce == false)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
