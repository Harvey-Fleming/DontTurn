using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int buttonclicked;
    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> isEnemyDead;

    //These will be the default values for when a new game is initialised
    public GameData()
    {
        this.buttonclicked = 0;
        this.playerPosition = Vector3.zero;
        isEnemyDead = new SerializableDictionary<string, bool>();
    }


}
