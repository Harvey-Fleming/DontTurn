using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject panel;
    public int timesPressed;
    public GameObject checkpoint;
    public int checkpointNumber;
    public bool isCheckpoint;
    public MapManager mapManager;
    private GameObject pMenu; 
    [SerializeField] private GameObject pMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        pMenu = pMenuCanvas.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            switch (timesPressed)
            {
                case 0:
                    ToggleMap(timesPressed);
                    timesPressed++;
                    Time.timeScale = 0;
                    pMenu.SetActive(false);
                    mapManager.isOpen = true;
                    break;
                case 1:
                    ToggleMap(timesPressed);
                    timesPressed--;
                    Time.timeScale = 1;
                    pMenuCanvas.GetComponent<PauseMenu>().resume();
                    mapManager.isOpen = false;
                    break; 
            }
        }
        
    }

    public void ToggleMap(int pressed)
    {
        switch (pressed)
        {
            case 0:
                panel.SetActive(true);
                break; 
            case 1:
                panel.SetActive(false);
                break; 

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CheckPoint>())
        {
            checkpoint = collision.gameObject;
            isCheckpoint = true; 
            checkpointNumber = checkpoint.GetComponent<CheckPoint>().checkpointNumber;
            checkpoint = null;
            isCheckpoint = false; 
        }
        if(collision.GetComponent<MapTriggerScript>())
        {
            checkpoint = collision.gameObject;
            isCheckpoint = true;
            checkpointNumber = checkpoint.GetComponent<MapTriggerScript>().checkpointNumber;
            checkpoint = null;
            isCheckpoint = false;
        }

       
    }
}
