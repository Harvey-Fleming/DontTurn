using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button loadGameButton;
    private IntroSeenManager introSeenManager;
    private bool hasSeenIntro = false;
    public GameObject saveData;

    // Start is called before the first frame update
    void Start()
    {
       if (!DataPersistenceManager.instance.HasGameData()) 
       {
            loadGameButton.interactable = false;
       }
    }

    private void Awake() 
    {
        introSeenManager = GameObject.FindObjectOfType<IntroSeenManager>();
    }

    public void ExitApplication()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void onNewGameClicked()
    {
        if (DataPersistenceManager.instance.HasGameData())
        {
            saveData.SetActive(true);
        }
        else if(!DataPersistenceManager.instance.HasGameData())
        {
            DataPersistenceManager.instance.NewGame();
            if (!introSeenManager.hasSeenIntro)
            {
                introSeenManager.hasSeenIntro = true;
                SceneManager.LoadSceneAsync("Intro");
            }
            else
            {
                SceneManager.LoadSceneAsync("Game");
            }
        }
         
        //play audio One-Shot
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuStartClick, this.transform.position);
    }

    public void onLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync("Game");
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuStartClick, this.transform.position);
    }

    public void YesToSaveData()
    {
        DataPersistenceManager.instance.NewGame();
        if (!introSeenManager.hasSeenIntro)
        {
            introSeenManager.hasSeenIntro = true;
            SceneManager.LoadSceneAsync("Intro");
        }
        else
        {
            SceneManager.LoadSceneAsync("Game");
        }

        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuStartClick, this.transform.position);
    }
    public void NoToSaveData()
    {
        saveData.SetActive(false);

        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuStartClick, this.transform.position);
    }

}
