using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public Transform spawnPointTransform;
    public SerializableDictionary<string, bool> isEnemyDead;

    //These will be the default values for when a new game is initialised
    public GameData()
    {
        this.playerHealth = 100f;
        this.spawnPointTransform = null;
        isEnemyDead = new SerializableDictionary<string, bool>();
    }


}
