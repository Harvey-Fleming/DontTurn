using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pMenu;
    [SerializeField] private GameObject pOptionsMenu;
    [SerializeField] private GameObject mapCanvas;
    [SerializeField] private MapScript mapScript;
    private GameObject mapMenu;

    [SerializeField] public bool isPaused = false;

    private void Start() {
        mapMenu = mapCanvas.transform.GetChild(0).gameObject;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void returnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }


    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
            }
            else
            {
                openPauseMenu();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
            }
        }
        
    }

    public void resume()
    {
        Time.timeScale = 1;
        pMenu.SetActive(false);
        pOptionsMenu.SetActive(false);
        isPaused = false;
        Cursor.visible = false;
    }

    void openPauseMenu()
    {
        Time.timeScale = 0;
        mapMenu.SetActive(false);
        mapCanvas.GetComponent<MapManager>().isOpen = false;
        mapScript.timesPressed = 0;
        pMenu.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
    }

    private void Update()
    {
        Pause();
    }
}
