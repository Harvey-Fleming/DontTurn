using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]

    [SerializeField] private bool initializeDataIfNull;


    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistenceManager instance {get; private set;}

    private FileDataHandler dataHandler;

    private void Awake() 
    {
        DontDestroyOnLoad(this);
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        Debug.Log(this.gameData.playerHealth);
        SaveGame();
        Debug.Log(this.gameData.playerHealth);
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        if (this.gameData == null && this.initializeDataIfNull)
        {
            NewGame();
        }

        // if no data can be loaded, don't allow loading
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A new Game must be started before data can be loaded");
            return;
        }
        

        //push loaded data to other scripts that need it
        foreach (IDataPersistence dataPesistenceObj in dataPersistenceObjects)
        {
            dataPesistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {

        if (this.gameData ==  null)
        {
            Debug.LogWarning("No data was found, Please Start a new game");
        }
        //pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPesistenceObj in dataPersistenceObjects)
        {
            dataPesistenceObj.SaveData(gameData);
        }
        
        //save that data to a file using the data handler
        dataHandler.Save(gameData);
       Debug.Log("Saved Game");
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects); 
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
