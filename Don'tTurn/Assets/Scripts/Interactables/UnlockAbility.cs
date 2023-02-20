using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    [SerializeField] private string NPCAbility;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            switch(NPCAbility)
            {
                case("Dash"):
                other.gameObject.GetComponent<PrototypeDash>().isUnlocked = true;
                break;
                case("Double Jump"):
                other.gameObject.GetComponent<PlayerMovement>().maxAerialJumpCount = 1;
                break;
            }
        }
        
    }
}

