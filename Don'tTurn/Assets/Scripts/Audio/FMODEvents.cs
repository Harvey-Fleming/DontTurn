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

    [field: Header("Ambiance SFX")]
    [field: SerializeField] public EventReference cityAmbiance { get; private set; }
    [field: SerializeField] public EventReference mushForestAmbiance { get; private set; }

    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference duoSkellyVoice { get; private set; }

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
