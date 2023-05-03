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
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    public void ShowMap(int mapNum)
    {
        images[mapNum - 1].gameObject.SetActive(true);
    }
}
