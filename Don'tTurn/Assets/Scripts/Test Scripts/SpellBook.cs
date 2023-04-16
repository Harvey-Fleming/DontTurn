using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Spell
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public float AreaOfEffect { get; set; }

    public Spell(string name)
    {
        this.Name = name;
        this.Damage = 10;
        this.AreaOfEffect = 1;
    }

    public Spell(string name, int damage)
    {
        this.Name = name;
        this.Damage = damage;
        this.AreaOfEffect = 1;
    }

    public Spell(string name, int damage, float AreaOfEffect)
    {
        this.Name = name;
        this.Damage = damage;
        this.AreaOfEffect = AreaOfEffect;
    }

    public string GetSpellStats()
    {
        string spellStats = "";
        spellStats += "Spell Name: " + this.Name;
        spellStats += "Damage: " + this.Damage;
        spellStats += "Area of Effect: " + this.AreaOfEffect;
        return spellStats;
    }
}
