using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public Vector3 spawnPoint;
    public SerializableDictionary<string, bool> isEnemyDead;

    //These will be the default values for when a new game is initialised
    public GameData()
    {
        this.playerHealth = 100f;
        this.spawnPoint = Vector3.zero;
        isEnemyDead = new SerializableDictionary<string, bool>();
    }


}
