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
                playerObj.GetComponent<PlayerMovement>().isDoubleJumpUnlocked = true;
                break;
                case("Grapple"):
                playerObj.GetComponent<GrappleAbility>().isUnlocked = true;
                break;
                case("Bomb"):
                playerObj.GetComponent<CurseAttacks>().isBombUnlocked = true;
                break;
                case("CursePunch"):
                playerObj.GetComponent<CurseAttacks>().isCursePunchUnlocked = true;
                break;
                case("Eat"):
                playerObj.GetComponent<CurseAttacks>().isEatUnlocked = true;
                break;
                default:
                Debug.LogError("No ability: " + NPCAbility + " Found, Please check spelling");
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

