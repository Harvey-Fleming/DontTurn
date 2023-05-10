using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public Vector3 spawnPoint;
    public SerializableDictionary<string, bool> isEnemyDead;
    public SerializableDictionary<string, bool> hasCollectedUpgrade;
    public SerializableDictionary<int, bool> hasCollectedNote;
    public SerializableDictionary<int, bool> hasCollectedCure;
    public SerializableDictionary<int, bool> hasUnlockedMap;
    public bool isGrappleUnlocked, isShotgunUnlocked, isDoubleJumpUnlocked, isBombUnlocked, isCursePunchUnlocked, isEatUnlocked;

    //These will be the default values for when a new game is initialised
    public GameData()
    {
        this.playerHealth = 100f;
        this.spawnPoint = new Vector3(58,-1,0.5f);
        
        hasCollectedUpgrade = new SerializableDictionary<string, bool>();
        hasCollectedNote = new SerializableDictionary<int, bool>();
        hasCollectedCure = new SerializableDictionary<int, bool>();

        //Enemy Save Variables
        isEnemyDead = new SerializableDictionary<string, bool>();

        //Abilities
        isGrappleUnlocked = false;
        isShotgunUnlocked = false;
        isDoubleJumpUnlocked = false;
        isBombUnlocked = false;
        isEatUnlocked = false;
        isCursePunchUnlocked = false;
    }


}
