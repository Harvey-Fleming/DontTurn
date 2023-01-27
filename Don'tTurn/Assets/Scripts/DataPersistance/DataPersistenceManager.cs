using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistenceManager instance {get; private set;}

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start() 
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }



    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO - Load any saved data from a file using the data handler

        // if no data can be loaded, initialise to new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }
        

        //push loaded data to other scripts that need it
        foreach (IDataPersistence dataPesistenceObj in dataPersistenceObjects)
        {
            dataPesistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded button clicks: " + gameData.buttonclicked);
    }

    public void SaveGame()
    {
        //pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPesistenceObj in dataPersistenceObjects)
        {
            dataPesistenceObj.SaveData(gameData);
        }
        Debug.Log("Saved button clicks: " + gameData.buttonclicked);
        
        //TODO - save that data to a file using the data handler
    }

    private void OnApplicationQuit() 
    {
        SaveGame();
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects); 
    }
}
