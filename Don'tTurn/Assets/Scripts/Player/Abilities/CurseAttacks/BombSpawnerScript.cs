using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerScript : MonoBehaviour
{
    PlayerMovement playerMovement; 

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.facingright)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        else if (!playerMovement.facingright)
        {
            transform.Rotate(0f, 180f, 0f); 
        }
    }
}
