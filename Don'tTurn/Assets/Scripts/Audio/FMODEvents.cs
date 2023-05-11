using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class FMODEvents : MonoBehaviour
{
    [field: Header("Menu SFX")]
    [field: SerializeField] public EventReference menuTransition { get; private set; }
    [field: SerializeField] public EventReference menuStartClick { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerLanding { get; private set; }
    [field: SerializeField] public EventReference shotgunFire { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference Melee1 { get; private set; }
    [field: SerializeField] public EventReference Melee2 { get; private set; }
    [field: SerializeField] public EventReference Melee3 { get; private set; }
    [field: SerializeField] public EventReference curseWhispers { get; private set; }
    [field: SerializeField] public EventReference fallDmg { get; private set; }
    [field: SerializeField] public EventReference playerDmg { get; private set; }
    [field: SerializeField] public EventReference cursePunch { get; private set; }
    [field: SerializeField] public EventReference grappleHook { get; private set; }
    [field: SerializeField] public EventReference transformation { get; private set; }

    [field: Header("Ambience SFX")]
    [field: SerializeField] public EventReference levelAmbience { get; private set; }
    [field: SerializeField] public EventReference CheckpointLight { get; private set; }
    [field: SerializeField] public EventReference EndingSFX { get; private set; }

    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference duoSkellySounds { get; private set; }
    [field: SerializeField] public EventReference duoSkellyDmg { get; private set; }
    [field: SerializeField] public EventReference mushEnemy { get; private set; }

    [field: Header("Interactables")]
    [field: SerializeField] public EventReference ItemPickup { get; private set; }
    [field: SerializeField] public EventReference NotePickup { get; private set; }
    [field: SerializeField] public EventReference MedkitHeal { get; private set; }
    [field: SerializeField] public EventReference MushroomHeal { get; private set; }
    [field: SerializeField] public EventReference ChamberRepair { get; private set; }

    [field: Header("UI SFX")]

    public static FMODEvents instance { get; private set; }
    [field: SerializeField] public EventReference notebookOpen { get; private set; }
    [field: SerializeField] public EventReference notebookTurn { get; private set; }
    [field: SerializeField] public EventReference notebookClose { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene");
        }
        instance = this;
    }
}
