using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaveTest : MonoBehaviour, IDataPersistence
{
    private Vector3 playerPosition;

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}
