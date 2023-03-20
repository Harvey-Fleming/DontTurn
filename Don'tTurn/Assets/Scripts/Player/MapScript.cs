using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject panel;
    int timesPressed; 
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
                    break;
                case 1:
                    ToggleMap(timesPressed);
                    timesPressed--;
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
}
