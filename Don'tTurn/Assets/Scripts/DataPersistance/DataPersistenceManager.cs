using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistenceManager instance {get; private set;}

    private FileDataHandler dataHandler;

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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }



    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

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
    }

    public void SaveGame()
    {
        //pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPesistenceObj in dataPersistenceObjects)
        {
            dataPesistenceObj.SaveData(gameData);
        }
        
        //save that data to a file using the data handler
        dataHandler.Save(gameData);
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
