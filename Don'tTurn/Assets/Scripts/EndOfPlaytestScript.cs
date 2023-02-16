using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfPlaytestScript : MonoBehaviour
{
    [SerializeField] GameObject EndScreenCanvas;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(EndScreenCanvas);
        }
        
    }

    public void ExitToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
        Destroy(EndScreenCanvas);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
