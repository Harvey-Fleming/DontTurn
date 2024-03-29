using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockAbility : MonoBehaviour
{
    [SerializeField] private string NPCAbility;
    [SerializeField] private GameObject playerObj;
    public ToggleAbilityImage toggleAbil;

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
                    if(playerObj.GetComponent<PrototypeDash>().isUnlocked == false)
                    {
                        playerObj.GetComponent<PlayerStats>().AddAbility();
                        playerObj.GetComponent<PrototypeDash>().isUnlocked = true;
                    }
                playerObj.GetComponent<PlayerInput>().dashSelected = true;
                playerObj.GetComponent<PlayerInput>().grappleSelected = false;
                toggleAbil.SwitchToShotgun();
                break;
                case("Double Jump"):
                    playerObj.GetComponent<PlayerMovement>().isDoubleJumpUnlocked = true;
                break;
                case("Grapple"):
                    if(playerObj.GetComponent<GrappleAbility>().isUnlocked == false)
                    {
                        playerObj.GetComponent<PlayerStats>().AddAbility();
                        playerObj.GetComponent<GrappleAbility>().isUnlocked = true;
                    }
                playerObj.GetComponent<PlayerInput>().dashSelected = false;
                playerObj.GetComponent<PlayerInput>().grappleSelected = true;
                    toggleAbil.SwitchToGrapple();
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

