using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockAbility : MonoBehaviour
{
    [SerializeField] private string NPCAbility;
    [SerializeField] private GameObject playerObj;

    private void Awake() 
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    public void OnAbilityUnlock() 
    {
        if (playerObj != null)
        {
            switch(NPCAbility)
            {
                case("Dash"):
                playerObj.GetComponent<PrototypeDash>().isUnlocked = true;
                break;
                case("Double Jump"):
                playerObj.GetComponent<PlayerMovement>().maxAerialJumpCount = 1;
                break;
                case("GrappleHook"):
                playerObj.GetComponent<GrappleAbility>().isUnlocked = true;
                break;
                default:
                break;
            }
        }
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindWithTag("Player");
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        playerObj = null;
    }

}

