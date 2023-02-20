using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                Debug.Log("Hit enemy");
                break;
            case "Ground":
                gameObject.SetActive(false); 
                break; 

        }
    }
}
