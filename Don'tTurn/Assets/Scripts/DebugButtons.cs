using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtons : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    private Rigidbody2D playerRB2D;
    private BoxCollider2D playerBoxCollider2D;

    private float gravityScale;

    public bool isGodEnabled = false;
    
    public void GodMode()
    {
        if(isGodEnabled)
        {
            gravityScale = playerRB2D.gravityScale;
            playerRB2D.gravityScale = 0;

            playerBoxCollider2D.enabled = false;
            
        }
        else
        {
            playerRB2D.gravityScale = gravityScale; 

            playerBoxCollider2D.enabled = true;
        }


    }

    public void UnlockAllAbilities()
    {

    }
    

}
