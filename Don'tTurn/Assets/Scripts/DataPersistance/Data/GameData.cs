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
    public bool isGrappleUnlocked, isShotgunUnlocked, isDoubleJumpUnlocked, isBombUnlocked, isCursePunchUnlocked, isEatUnlocked;

    //These will be the default values for when a new game is initialised
    public GameData()
    {
        this.playerHealth = 100f;
        this.spawnPoint = Vector3.zero;
        isEnemyDead = new SerializableDictionary<string, bool>();
        hasCollectedUpgrade = new SerializableDictionary<string, bool>();

        //Abilities
        isGrappleUnlocked = false;
        isShotgunUnlocked = false;
        isDoubleJumpUnlocked = false;
        isBombUnlocked = false;
        isEatUnlocked = false;
        isCursePunchUnlocked = false;
    }


}
