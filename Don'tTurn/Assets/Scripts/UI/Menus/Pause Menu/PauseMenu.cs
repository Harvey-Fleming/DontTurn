using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pMenu;


    [SerializeField] private bool isPaused = false;

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
            }
            else
            {
                openPauseMenu();   
            }
        }
        
    }

    public void resume()
    {
        Time.timeScale = 1;
        pMenu.SetActive(false);
        isPaused = false;
    }

    void openPauseMenu()
    {
        Time.timeScale = 0;
        pMenu.SetActive(true);
        isPaused = true;
    }

    private void Update()
    {
        Pause();
    }
}
