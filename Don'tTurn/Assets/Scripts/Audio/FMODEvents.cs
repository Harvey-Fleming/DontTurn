using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class FMODEvents : MonoBehaviour
{
    [field: Header("Menu Transition SFX")]
    [field: SerializeField] public EventReference menuTransition { get; private set; }
    
    [field: SerializeField] public EventReference menuStartClick { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference LandingConcrete { get; private set; }
    [field: SerializeField] public EventReference LandingGrass { get; private set; }
    [field: SerializeField] public EventReference shotgunFire { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference Melee1 { get; private set; }
    [field: SerializeField] public EventReference Melee2 { get; private set; }
    [field: SerializeField] public EventReference Melee3 { get; private set; }

    [field: Header("Ambience SFX")]
    [field: SerializeField] public EventReference cityAmbience { get; private set; }
    [field: SerializeField] public EventReference sewerAmbience { get; private set; }
    [field: SerializeField] public EventReference mushForestAmbience { get; private set; }

    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference duoSkellyVoice { get; private set; }
    [field: SerializeField] public EventReference duoSkellyAggro { get; private set; }
    [field: SerializeField] public EventReference duoSkellyDmg { get; private set; }

    public static FMODEvents instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene");
        }
        instance = this;
    }
}
