using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RPGCharacter
{
    public string Name { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Dexterity { get; set; }

    public RPGCharacter(string name, int strength, int intelligence, int dexterity)
    {
        this.Name = name;
        this.Strength = strength;
        this.Intelligence = intelligence;
        this.Dexterity = dexterity;
    }

    private RPGCharacter(string name)
    {
        this.Name = name;
        this.Strength = UnityEngine.Random.Range(0, 18);
        this.Intelligence = UnityEngine.Random.Range(0, 18);
        this.Dexterity = UnityEngine.Random.Range(0, 18);
    }

    public static RPGCharacter GetRandomCharacter(string name)
    {
        return new RPGCharacter(name);
    }

    public string GetCharacterStats()
    {
        string characterstats = "";
        characterstats += "Name: " + this.Name;
        characterstats += "Strength: " + this.Strength;
        characterstats += "Intelligence: " + this.Intelligence;
        characterstats += "Dexterity: " + this.Dexterity;
        return characterstats;

    }
}
