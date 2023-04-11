using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MapPanelScript : MonoBehaviour
{
    public Image[] images; 
    public MapScript MapScript;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            images[MapScript.checkpointNumber].gameObject.SetActive(false);

       

    }
}
