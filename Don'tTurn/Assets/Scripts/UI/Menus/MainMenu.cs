using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button loadGameButton;
    // Start is called before the first frame update
    void Start()
    {
       if (!DataPersistenceManager.instance.HasGameData()) 
       {
            loadGameButton.interactable = false;
       }
    }

    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void onNewGameClicked()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Game");
    }

    public void onLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync("Game");
    }

}
