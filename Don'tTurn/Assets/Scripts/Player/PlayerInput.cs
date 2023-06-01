using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Movement Input
    public bool moveLeft {get; private set;}
    public bool moveRight {get; private set;}
    public bool jumpKey {get; private set;}
    public bool jumpKeyReleased {get; private set;}

    //Movement Ability
    public bool moveAbilityInput {get; private set;}
    public bool moveAbilityInputHeld {get; private set;}
    public bool moveAbilityInputRelease {get; private set;}
    public bool swapmoveAbilityInput {get; private set;}
    public bool grappleSelected {get; set;}
    public bool dashSelected {get; set;}

    //Combat Abilities
    public bool meleeInput {get; private set;}
    public bool punchAbilityInput {get; private set;}
    public bool eatAbilityInput {get; private set;}
    public bool bombAbilityInput {get; private set;}

    //General UI Inputs
    public bool consumeItem1Input {get; private set;}
    public bool consumeItem2Input {get; private set;}
    public bool interactInput {get; private set;}
    public bool toggleMapInput {get; private set;}
    public bool inventoryInput {get; private set;}

    public ToggleAbilityImage toggleAbil;

    public PrototypeDash shotgun;

    public GrappleAbility grapple;

    private void Start() 
    {
        dashSelected = true;
    }

    private void Update() {
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);

        jumpKey = Input.GetKeyDown(KeyCode.Space);
        jumpKeyReleased = Input.GetKeyUp(KeyCode.Space);

        meleeInput = Input.GetMouseButtonDown(0);
        moveAbilityInput = Input.GetMouseButtonDown(1);
        moveAbilityInputHeld = Input.GetMouseButton(1);
        moveAbilityInputRelease = Input.GetMouseButtonUp(1);
        swapmoveAbilityInput = Input.GetKeyDown(KeyCode.C);

        punchAbilityInput = Input.GetKeyDown(KeyCode.LeftShift);
        /* eatAbilityInput = Input.GetKeyDown(KeyCode.Q);
        bombAbilityInput = Input.GetKeyDown(KeyCode.E); */

        consumeItem1Input = Input.GetKeyDown(KeyCode.Alpha1);
        consumeItem2Input = Input.GetKeyDown(KeyCode.Alpha2);
        interactInput = Input.GetKeyDown(KeyCode.F);
        toggleMapInput = Input.GetKeyDown(KeyCode.M);
        inventoryInput = Input.GetKeyDown(KeyCode.I);

        NextAbilityScroll();
    }

    private void NextAbilityScroll()
    {
        if (swapmoveAbilityInput && grapple.isUnlocked && shotgun.isUnlocked)
        {
            grappleSelected = !grappleSelected;
            dashSelected = !dashSelected;
            if (grappleSelected)
            {
                toggleAbil.SwitchToGrapple();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.SwitchToGrapple, this.transform.position);
            }
            else
            {
                toggleAbil.SwitchToShotgun();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.SwitchToShotgun, this.transform.position);
            }
        }
    }
}
