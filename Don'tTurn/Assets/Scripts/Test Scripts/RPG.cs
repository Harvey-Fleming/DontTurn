using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RPG : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDisable()
    {
        RPGCharacter draco = new RPGCharacter("Draco", 10, 15, 6);
        Debug.Log(draco.GetCharacterStats());
        RPGCharacter randomNPC = RPGCharacter.GetRandomCharacter("Jailroom Guard");
        Debug.Log(randomNPC.GetCharacterStats());

        Spell Iceball = new Spell("Iceball");
        Spell Fireball = new Spell("Fireball", 100);
        Spell EarthShake = new Spell("EarthShake", 10, 100);
        Debug.Log(Iceball.GetSpellStats());
        Debug.Log(Fireball.GetSpellStats());
        Debug.Log(EarthShake.GetSpellStats());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
