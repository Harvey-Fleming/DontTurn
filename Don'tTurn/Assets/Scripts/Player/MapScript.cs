using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject panel;
    int timesPressed;
    public GameObject checkpoint;
    public int checkpointNumber;
    public bool isCheckpoint; 
    // Start is called before the first frame update
    void Start()
    {
        
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
                    break;
                case 1:
                    ToggleMap(timesPressed);
                    timesPressed--;
                    Time.timeScale = 1;
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
        else if(collision.GetComponent<MapTriggerScript>())
        {
            checkpointNumber = collision.GetComponent<MapTriggerScript>().mapNumber; 
        }

       
    }
}
